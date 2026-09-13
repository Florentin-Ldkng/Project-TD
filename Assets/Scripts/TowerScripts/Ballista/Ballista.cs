using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class Ballista : MonoBehaviour
{
    public BallistaAttackPoints[] ballistaAttackPoints = new BallistaAttackPoints[5];

    public TowerGeneric tG;
    public List<GameObject> Targets = new List<GameObject>();
    public List<GameObject> AttackPoints = new List<GameObject>();
    
    public GameObject ProjectileBig;
    public GameObject ProjectileSmall;

    public GameObject ProjectilesEmpty;

    bool isShooting = false;

    private bool selectorIsRunning = false;

    private int LAttach = 1, RAttach = 1;


    void Start()
    {
        BallistaLevelUp();
        tG.EnableTower();
        tG.UpdateRange();
    }

    private void FixedUpdate()
    {

        foreach (var SideTurrets in AttackPoints)
        {
            if (SideTurrets.CompareTag("Attachment"))
            {
                SideTurrets.transform.Rotate(new Vector3(0, LAttach, 0));
            }
        }

        if (tG.towerDetection.enemyList.Count > 0)
        {
            AttackPoints[0].transform.LookAt(tG.towerDetection.enemyList.First().enemy.transform.position + Vector3.up * 1.5f);
        }

    }

    private void OnEnable()
    {
        tG.towerDetection.OnDetection += EnemyHandler;
        tG.OnLevelUp += BallistaLevelUp;
    }

    private void OnDisable()
    {
        tG.towerDetection.OnDetection -= EnemyHandler;
        tG.OnLevelUp -= BallistaLevelUp;
        ballistaAttackPoints[tG.CurrentLevel].OnRotationLimitHit -= TowerReset;
    }
    private void BallistaLevelUp()
    {

        if (tG.Range != 0)
        {
            ballistaAttackPoints[tG.CurrentLevel].OnRotationLimitHit -= TowerReset;
        }

        tG.LoadStats();

        AttackPoints.Clear();
        AttackPoints.AddRange(ballistaAttackPoints[tG.CurrentLevel].Attackpoints);

        Targets.Clear();
        Targets.Capacity = tG.MaxTargets;

        ballistaAttackPoints[tG.CurrentLevel].OnRotationLimitHit += TowerReset;
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
       

    private void TowerReset()
    {
        SetCorrectRotation(ballistaAttackPoints[tG.CurrentLevel].errorPoint.name);
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


    IEnumerator Shooting()
    {
        isShooting = true;
        yield return new WaitForSeconds(tG.ShootingDelay);
        do
        {
            var a = Instantiate(ProjectileBig, ballistaAttackPoints[tG.CurrentLevel].Shootpoints[0].transform.position, ballistaAttackPoints[tG.CurrentLevel].Shootpoints[0].transform.rotation, ProjectilesEmpty.transform);
            a.GetComponent<Ballista_Projectile>().projectile.setValues(tG.Damage, .4f,StatusEffects.None,this.gameObject, tG.towerDetection.enemyList.First().enemy.gameObject);

            yield return new WaitForSeconds(tG.ShootingDelay);
        } while (tG.towerDetection.enemyList.Count > 0);

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
