using System.Collections.Generic;
using UnityEngine;

public class GenericEnemy : MonoBehaviour
{
    public List<GameObject> Path;
    public Enemy EnemyStats;
    public Animator animator;
    public int index = 0;
    private Projectile lastHit;
    public AnimationClip walkClip;

    private int Hp;
    private int Armor;
    private float Speed;
    private int XPGiven;
    private bool stealth;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Hp = EnemyStats.Hp;
        Armor = EnemyStats.Armor;
        Speed = EnemyStats.Speed;
        XPGiven = EnemyStats.XPGiven;
        stealth = EnemyStats.stealth;
    }

    private void FixedUpdate()
    {
        this.transform.LookAt(Path[index].transform.position + (Vector3.up * .5f));

        Debug.Log(animator.GetCurrentAnimatorStateInfo(0).IsName(walkClip.name));

        if (animator.GetCurrentAnimatorStateInfo(0).IsName(walkClip.name))
        {
            this.transform.Translate((Vector3.forward * Speed) * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("WalkNode"))
        {
            if (index < Path.Count - 1)
            {
                index++;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }

    public void RegisterProjectile(Projectile projectile)
    {
        lastHit = projectile;
        Hp -= projectile.damage;

        if (Hp <= 0)
            Destroy(this.gameObject);
    }

    private void OnDisable()
    {
        GameObject.Find("Enemies").BroadcastMessage("RemoveEnemy", this.gameObject);
        if (lastHit != null)
            lastHit.originTower.SendMessage("EarnXP", XPGiven);
    }
}
