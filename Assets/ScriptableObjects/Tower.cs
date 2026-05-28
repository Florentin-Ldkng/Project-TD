using UnityEngine;

[CreateAssetMenu(fileName = "Tower", menuName = "Scriptable Objects/Tower")]
public class Tower : ScriptableObject
{
    public bool DebugMode = false;

    public int range;
    public int damage;
    public float sDelay;
    public bool multitarget;
    
    
}
