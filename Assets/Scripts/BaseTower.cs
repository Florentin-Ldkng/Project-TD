using System.Collections;
using UnityEngine;

public class BaseTower : MonoBehaviour
{
    public GameObject Range;
    public Tower TowerStats;
    public TowerDetection TowerDetection;

    private bool routine = false;

    private void Awake()
    {
        if (Range.transform.localScale.x != TowerStats.range)
        {
            Range.transform.localScale = new Vector3(TowerStats.range, TowerStats.range, TowerStats.range);
        }
    }

    private void OnEnable()
    {
        TowerDetection.OnDetection += ShootingStart;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

     void ShootingStart()
    {
        if (TowerDetection.enemyList.Count > 0 && !routine)
        {
            StartCoroutine(Shooting());
            routine = true;
        }
    }


    
    IEnumerator Shooting()
    {
        do
        {
            Debug.Log("Shooting");
            yield return new WaitForSeconds(TowerStats.sDelay);
        } while (TowerDetection.enemyList.Count > 0);
        routine = false;
        yield return null;
    }
}
