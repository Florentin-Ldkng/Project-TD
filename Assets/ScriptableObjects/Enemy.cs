using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Scriptable Objects/Enemy")]
public class Enemy : ScriptableObject
{
    [Header("Stats")]
    public int Hp;
    public int Armor;
    public float Speed;
    public int XPGiven;
    public bool stealth;
}
