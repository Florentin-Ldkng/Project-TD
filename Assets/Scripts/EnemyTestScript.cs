using System.Collections.Generic;
using UnityEngine;

public class EnemyTestScript : MonoBehaviour
{
    public List<GameObject> Path;

    public int index = 0;

    public int health = 100;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {        
             
    }

    private void FixedUpdate()
    {
        this.transform.LookAt(Path[index].transform.position + (Vector3.up * 1.5f));
        this.transform.Translate(Vector3.forward * 0.5f);      
        
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.gameObject.tag)
        {
            case "WalkNode":
                if (index < Path.Count - 1)
                {
                    index++;
                }
                else
                {
                    Destroy(this.gameObject);
                }
                break;            
            default:
                Debug.Log(other.tag); break;

        }
    }

    public void RegisterProjectile()
    {
        Debug.Log("TookDamage");
    }
}
