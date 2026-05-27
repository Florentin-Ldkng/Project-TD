using UnityEngine;

public class EnemyClass
{
    public GameObject enemy {  get; set; }
    public LineRenderer line {  get; set; }

    public EnemyClass(GameObject enemy, LineRenderer line)
    { 
        this.enemy = enemy;
        this.line = line;
    }
}
