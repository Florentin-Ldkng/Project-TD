using UnityEngine;

public class EnemyClass
{
    public GameObject enemy {  get; set; }
    public GameObject target { get; set; }
    public EnemyClass(GameObject enemy)
    { 
        this.enemy = enemy;
    }
}
