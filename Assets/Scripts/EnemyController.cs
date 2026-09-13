using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public SimpleMap mapController;
    public List<GameObject> enemyList;

    public GameObject TowerEmpty;
    public List<GameObject> activeEnemies;

    public List<Wave> Waves;

    
    public event System.Action OnEmptyList;

    private Wave activeWave;
    private bool spawningDone = false;
    
    public void Start()
    {
        
    }

    public void StartSpawning(int Wave)
    {
        spawningDone = false;
        
        activeWave = Waves[Wave];
        enemyList = activeWave.EnemyOrder;

        StartCoroutine(SpawnEnemies());
    }
    public IEnumerator SpawnEnemies()
    {
        foreach (var item in enemyList)
        {
            yield return new WaitForSeconds(Random.Range(activeWave.waitTimeBasic - activeWave.waitTimeRandomBound, activeWave.waitTimeBasic + activeWave.waitTimeRandomBound));

            var spawn = Instantiate(item, mapController.path.First().transform.position + (Vector3.up * 0.5f), Quaternion.identity, this.transform);
            
            //Needs Rework i dont know the enemy
            spawn.GetComponent<BaseSkeleton>().Path = mapController.path;

            activeEnemies.Add(spawn);
            
        }

        spawningDone = true;

        yield return null;
    }

    public void RemoveEnemy(GameObject enemy)
    {

        TowerEmpty.BroadcastMessage("RemoveEnemy", enemy);

        activeEnemies.Remove(enemy);

        if (activeEnemies.Count == 0 && spawningDone)
        {
            OnEmptyList?.Invoke();
            activeEnemies.Clear();
        }
    }

}
