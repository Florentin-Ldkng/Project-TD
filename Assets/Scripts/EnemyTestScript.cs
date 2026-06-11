using System.Collections.Generic;
using UnityEngine;

public class EnemyTestScript : MonoBehaviour
{
    public List<GameObject> Path;

    public int index = 0;
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
}
