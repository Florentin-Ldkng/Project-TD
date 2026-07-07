using UnityEngine;

public class Projectile
{
    public int damage;
    public float speed;
    public StatusEffects effects;
    public GameObject originTower;
    
    public void setValues(int damage,float speed, StatusEffects effects, GameObject originTower)
    {
        this.damage = damage;
        this.speed = speed;
        this.effects = effects;
        this.originTower = originTower;
    }
}
