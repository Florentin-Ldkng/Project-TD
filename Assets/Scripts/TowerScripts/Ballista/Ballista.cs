using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Ballista : MonoBehaviour
{
    public Tower[] BallistaBaseStats = new Tower[5];
    public GameObject[] BallistaLevels = new GameObject[5];
    public BallistaAttackPoints[] ballistaAttackPoints = new BallistaAttackPoints[5];

    public List<GameObject> Targets = new List<GameObject>();
    public List<GameObject> AttackPoints = new List<GameObject>();
    public GameObject DetectionRange;
    public TowerDetection TowerDetection;
    public GameObject ProjectileBig;
    public GameObject ProjectileSmall;

    public ParticleSystem Levelup;

    public GameObject ProjectilesEmpty;
    public int CurrentLevel = 0;
    public int XP = 0;


    public int XPThreshhold;

    bool isShooting = false;


    private int Range;
    private int Damage;
    private float ShootingDelay;
    private bool Multitarget;
    private bool UsesRange;
    private int MaxTargets;
    private int DamageAllocation;
    private bool selectorIsRunning = false;

    private int LAttach = 1, RAttach = 1;


    void Start()
    {
        LoadStats();
        EnableTower();
        UpdateRange();
    }

    private void FixedUpdate()
    {
        foreach (var SideTurrets in AttackPoints.FindAll(x => x.gameObject.CompareTag("Attachment")))
        {
            SideTurrets.transform.Rotate(new Vector3(0, LAttach, 0));
        }


        if (TowerDetection.enemyList.Count > 0)
        {
            AttackPoints[0].transform.LookAt(TowerDetection.enemyList.First().enemy.transform.position + Vector3.up * 1.5f);
        }


    }

    private void OnEnable()
    {
        TowerDetection.OnDetection += EnemyHandler;
    }

    private void OnDisable()
    {
        TowerDetection.OnDetection -= EnemyHandler;
        ballistaAttackPoints[CurrentLevel].OnRotationLimitHit -= TowerReset;
    }
    private void LoadStats()
    {

        if (Range != 0)
        {
            ballistaAttackPoints[CurrentLevel].OnRotationLimitHit -= TowerReset;
        }

        Range = BallistaBaseStats[CurrentLevel].BaseRange;
        Damage = BallistaBaseStats[CurrentLevel].BaseDamage;
        ShootingDelay = BallistaBaseStats[CurrentLevel].BaseShootingDelay;
        Multitarget = BallistaBaseStats[CurrentLevel].Multitarget;
        UsesRange = BallistaBaseStats[CurrentLevel].UsesRange;
        MaxTargets = BallistaBaseStats[CurrentLevel].MaxTargets;
        DamageAllocation = BallistaBaseStats[CurrentLevel].DamageAllocation;
        XPThreshhold = BallistaBaseStats[CurrentLevel].XPThreshhold;

        AttackPoints.Clear();
        AttackPoints.AddRange(ballistaAttackPoints[CurrentLevel].Attackpoints);

        Targets.Clear();
        Targets.Capacity = MaxTargets;

        ballistaAttackPoints[CurrentLevel].OnRotationLimitHit += TowerReset;
    }

    private void EnableTower()
    {
        BallistaLevels[Mathf.Clamp(CurrentLevel - 1, 0, 4)].SetActive(false);
        BallistaLevels[CurrentLevel].SetActive(true);
    }

    private void UpdateRange()
    {
        DetectionRange.transform.localScale = new Vector3(Range, Range, Range);
    }

    private void EnemyHandler()
    {
        if (isShooting == false)
        {
            StartCoroutine(Shooting());
        }

        if (selectorIsRunning == false)
        {
            selectorIsRunning = true;
            StartCoroutine(TargetSelector());
        }
    }

    private void LevelUp()
    {
        Levelup.Play();
        LoadStats();
        EnableTower();
        UpdateRange();
    }

    private void TowerReset()
    {
        SetCorrectRotation(ballistaAttackPoints[CurrentLevel].errorPoint.name);
    }

    private void SetCorrectRotation(string name)
    {
        switch (name)
        {
            case "R":
                RAttach *= -1;
                break;
            case "L":
                LAttach *= -1;
                break;
            default:
                Debug.LogError($"SetCorrectRotationClamp - Object has wrong name - {name}");
                break;
        }
    }

    public void EarnXP(int earnedXP)
    {
        XP += earnedXP;
        if (XP >= XPThreshhold && CurrentLevel < 3)
        {
            XP = XPThreshhold - XP;
            CurrentLevel++; 
            LevelUp();
        }
    }

    IEnumerator Shooting()
    {
        isShooting = true;
        yield return new WaitForSeconds(ShootingDelay);
        do
        {
            var a = Instantiate(ProjectileBig, ballistaAttackPoints[CurrentLevel].Shootpoints[0].transform.position, ballistaAttackPoints[CurrentLevel].Shootpoints[0].transform.rotation, ProjectilesEmpty.transform);
            a.GetComponent<Ballista_Projectile>().projectile.setValues(Damage, 1,StatusEffects.None,this.gameObject);

            yield return new WaitForSeconds(ShootingDelay);
        } while (TowerDetection.enemyList.Count > 0);

        isShooting = false;
        yield return null;
    }



    IEnumerator TargetSelector()
    {
        //do
        //{
        //    //TODO idee ist gut nur ich muss es umdrehen. auf den attackpoints müssen die ziele verwaltet werden und der code muss nur ausgeführt werden wenn ich das ziel des attackpoints verliere durch z.b. tot / oder halt den lock
        //    //Das einzige wofür ich keine lösung finde ist wie ich dafür sorge das der attackpoint nicht sofort wieder das gleiche ziel auswählt weil dann wäre die ganze logik fürn arsch (Potentiell non issue weil der gegner dann eh näher an den anderen Attackpoints sein sollte)
        //
        //
        //    //foreach (var attackPoint in AttackPoints)
        //    //{
        //    //    foreach (var enemy in TowerDetection.enemyList.Where(x => x.target == null))
        //    //    {
        //    //
        //    //        float shortest = 1000;
        //    //        GameObject shortestGO = null;
        //    //    
        //    //        float calc = Vector3.Distance(attackPoint.transform.position, enemy.enemy.transform.position);
        //    //        if (calc < shortest)
        //    //        {
        //    //            shortest = calc;
        //    //            shortestGO = attackPoint;
        //    //            
        //    //        }
        //    //
        //    //        enemy.target = shortestGO;
        //    //        Debug.Log($"Enemy is targeted by: {shortestGO?.name}");
        //    //
        //    //    }               
        //    //}
        //    //yield return new WaitForSeconds(1f);
        //} while (TowerDetection.enemyList.Count > 0);
        yield return null;
        selectorIsRunning = false;
    }
}
