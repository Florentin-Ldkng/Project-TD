using UnityEngine;

public class Ballista_Projectile : MonoBehaviour
{
    public Projectile projectile = new Projectile();
    public StatusEffects EffectsOnProjectiles;
    
    bool alreadyTriggered = false;

    private void OnEnable()
    {
        alreadyTriggered = false;
    }
    void FixedUpdate()
    {
        this.transform.Translate(Vector3.forward * projectile.speed);

        if (Mathf.Abs(this.transform.position.x) > 30 || Mathf.Abs(this.transform.position.z) > 30)
        {
            ProjectilePool.ReturnObjToPool(this.gameObject);
            alreadyTriggered = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!alreadyTriggered)
            if (other.gameObject == projectile.target || projectile.target == null)
                other.gameObject.SendMessage("RegisterProjectile",projectile);
                ProjectilePool.ReturnObjToPool(this.gameObject);
                alreadyTriggered = true;

        if (other.CompareTag("Placeable") || other.CompareTag("Path"))
        {
            ProjectilePool.ReturnObjToPool(this.gameObject);
            alreadyTriggered = true;
        }
    }
}
