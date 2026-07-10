using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public MapController mapController;
    public List<GameObject> enemy;
    public List<int> enemyValue;

    public List<GameObject> activeEnemies;

    public int Budget;
    public event System.Action OnEmptyList;

    public void Start()
    {
    }

    public void StartSpawning(int EnemyBudget)
    {
        Budget = EnemyBudget;
        StartCoroutine(SpawnEnemies());    
    }
    public IEnumerator SpawnEnemies()
    {
        do
        {
            yield return new WaitForSeconds(1);
            var spawn = Instantiate(enemy.First(), mapController.path.First().transform.position + (Vector3.up * 0.5f), Quaternion.identity, this.transform);
            spawn.GetComponent<BaseSkeleton>().Path = mapController.path;

            activeEnemies.Add(spawn);
            Budget -= enemyValue.First();

        } while (Budget > 0);


        yield return null;
    }

    public void RemoveEnemy(GameObject enemy)
    {
        
        mapController.BroadcastMessage("RemoveEnemy", enemy);

        activeEnemies.Remove(enemy);

        if (Budget == 0 && activeEnemies.Count == 0)
        {
            OnEmptyList?.Invoke();
            activeEnemies.Clear();
        }
    }

}
