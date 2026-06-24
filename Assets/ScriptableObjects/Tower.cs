using UnityEngine;

[CreateAssetMenu(fileName = "Tower", menuName = "Scriptable Objects/Tower")]
public class Tower : ScriptableObject
{
    [Header("Debug Options")]
    public bool DebugMode = false;

    [Header("Variables")]
    public int BaseRange;
    public int BaseDamage;
    [Tooltip("Delay between shots in seconds")]
    public float BaseShootingDelay;
    public bool Multitarget;
    public bool UsesRange;
    public int MaxTargets;
    [Tooltip("% of the normal Damage dealt via side turrets")]
    [Range(0,100)]
    public int DamageAllocation;
    public int XPThreshhold;
}
