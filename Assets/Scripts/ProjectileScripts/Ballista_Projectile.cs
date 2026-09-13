using UnityEngine;

public class Ballista_Projectile : MonoBehaviour
{
    public Projectile projectile = new Projectile();
    public StatusEffects EffectsOnProjectiles;
    
    bool alreadyTriggered = false;
    
    void FixedUpdate()
    {
        this.transform.Translate(Vector3.forward * projectile.speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!alreadyTriggered && other.gameObject == projectile.target)
        other.gameObject.SendMessage("RegisterProjectile",projectile);
        Destroy(this.gameObject);
        alreadyTriggered = true;
    }
}
