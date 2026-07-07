using System.Collections.Generic;
using UnityEngine;

public class TowerDetection : MonoBehaviour
{
    public List<EnemyClass> enemyList;

    public event System.Action OnDetection;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyList = new List<EnemyClass>();        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            var temp = new EnemyClass(other.gameObject);
            
            enemyList.Add(temp);

            OnDetection?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            var temp = enemyList.Find(x => x.enemy == other.gameObject);
            enemyList.Remove(temp);
        }
    }

    public void RemoveEnemy(GameObject enemy)
    {
        var temp = enemyList.Find(x => x.enemy == enemy.gameObject);
        enemyList.Remove(temp);
    }
}
