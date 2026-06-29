using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public GameObject Projectile;

    public ParticleSystem Levelup;

    public int CurrentLevel = 0;
    public int XP = 0;


    public int XPThreshhold;

    private int Range;
    private int Damage;
    private float ShootingDelay;
    private bool Multitarget;
    private bool UsesRange;
    private int MaxTargets;
    private int DamageAllocation;
    private bool selectorIsRunning = false;

    private int minClamp, maxClamp;
    

    void Start()
    {
        LoadStats();
        EnableTower();
        UpdateRange();
    }

    private void FixedUpdate()
    {
        XP++;
        
        if(XP >= XPThreshhold && CurrentLevel < 2)
        {
            XP = 0;
            CurrentLevel++;
            LevelUp();
        }

        if (CurrentLevel == 2 && TowerDetection.enemyList.Count > 0)
        {
            foreach (var AttackPoint in AttackPoints)
            {
                //var Enemy = TowerDetection.enemyList.FirstOrDefault(x => x.target = AttackPoint);
                var Enemy = TowerDetection.enemyList.First();
                Vector3 enemyPosition = Enemy.enemy.transform.position + Vector3.up;       
                  
                if (AttackPoint.CompareTag("Attachment"))
                {
                    //SetCorrectRotationClamp(AttackPoint.name);
                    //
                    //Vector3 directionToEnemy = enemyPosition - AttackPoint.transform.position;
                    //Quaternion targetRotationWorld = Quaternion.LookRotation(directionToEnemy);
                    //
                    //if (AttackPoint.name == "L")
                    //{ Debug.Log(AttackPoint.transform.eulerAngles.y); }
                    //
                    //if (AttackPoint.transform.parent != null)
                    //{
                    //    Quaternion localTargetRotation = Quaternion.Inverse(AttackPoint.transform.parent.rotation) * targetRotationWorld;      
                    //    
                    //    if (localTargetRotation.eulerAngles.y >= maxClamp || localTargetRotation.eulerAngles.y <= minClamp)
                    //    {
                    //        return;
                    //    }
                    //
                    //    AttackPoint.transform.localRotation = localTargetRotation;
                    //}
                    //else
                    //{
                    //    AttackPoint.transform.rotation = targetRotationWorld;
                    //}

                    AttackPoint.transform.LookAt(enemyPosition);

                    if (AttackPoint.name == "L")
                    {
                        Debug.Log(AttackPoint.transform.localRotation.y);
                    }
                }
                else
                {
                    AttackPoint.transform.LookAt(enemyPosition);
                }

            }            

          }

    }

    private void OnEnable()
    {
        TowerDetection.OnDetection += EnemyHandler;
    }

    private void OnDisable()
    {
        TowerDetection.OnDetection -= EnemyHandler;
    }
    private void LoadStats()
    {
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
        //StartCoroutine(Shooting(AttackPoints[0]));

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

    private void SetCorrectRotationClamp(string name)
    {        
        switch (name)
        {
            case "R":
                minClamp = 10;
                maxClamp = 170;
                break;
            case "L":
                minClamp = 10;
                maxClamp = 170;
                break;
            default:
                Debug.LogError($"SetCorrectRotationClamp - Object has wrong name - {name}");
                break;
        }
    }
    IEnumerator Shooting(GameObject Attackpoint)
    {
        do
        {
            Debug.Log("Shooting");            
            yield return new WaitForSeconds(ShootingDelay);
        } while (TowerDetection.enemyList.Count > 0);
        yield return null;
    }

    IEnumerator TargetSelector()
    {
        do
        {
            foreach (var enemy in TowerDetection.enemyList)
            {
                enemy.target = AttackPoints[0].gameObject;
            }
            yield return new WaitForSeconds(1f);
        } while (TowerDetection.enemyList.Count > 0);
        yield return null;
        selectorIsRunning = false;
    }
}
