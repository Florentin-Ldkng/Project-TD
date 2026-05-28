using System.Collections.Generic;
using UnityEngine;

public class TowerDetection : MonoBehaviour
{
    public List<EnemyClass> enemyList;
    EnemyClass Target = null;

    public delegate void Detection();
    public static event Detection OnDetection;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyList = new List<EnemyClass>();        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyList.Count > 0)
        {
            foreach (EnemyClass enemy in enemyList)
            {
                if (enemy.line == null)
                {
                    enemy.line = SpawnLine();
                }
                enemy.line.SetPosition(0, this.gameObject.transform.position);
                enemy.line.SetPosition(1, enemy.enemy.transform.position);
            }
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            var temp = new EnemyClass(other.gameObject, SpawnLine());

            if (Target == null)
            { 
                Target = temp;
                Target.line.material.color = Color.hotPink;
            }
            
            enemyList.Add(temp);

            if (OnDetection != null)
                OnDetection();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            var temp = enemyList.Find(x => x.enemy == other.gameObject);
            Destroy(temp.line.gameObject);
            enemyList.Remove(temp);
        }
    }

    protected LineRenderer SpawnLine()
    {
        var line = new GameObject("Line").AddComponent<LineRenderer>();
        line.material.color = Color.grey;
        line.startWidth = 0.1f;
        line.endWidth = 0.1f;
        line.positionCount = 2;
        line.useWorldSpace = true;

        return line;
        
    }
    
}
