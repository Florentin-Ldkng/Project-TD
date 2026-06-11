using System.Linq;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public MapController mapController;
    public GameObject enemy;
    public bool spawn1 = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (spawn1)
        {
            spawn1 = false;

            var spawn = Instantiate(enemy,mapController.path.First().transform.position + (Vector3.up * 0.5f) ,Quaternion.identity,this.transform);
            spawn.GetComponent<BaseSkeleton>().Path = mapController.path;
        }
    }


}
