using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "Scriptable Objects/Wave")]
public class Wave : ScriptableObject
{
    public float waitTimeBasic;
    public float waitTimeRandomBound;

    public List<GameObject> EnemyOrder;
}
