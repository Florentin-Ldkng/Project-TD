using System.Linq;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public MapController mapController;
    public GameObject enemy;


    float time = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= 5f)
        {
            time = 0;

            var spawn = Instantiate(enemy,mapController.path.First().transform.position + (Vector3.up * 0.5f) ,Quaternion.identity,this.transform);
            spawn.GetComponent<BaseSkeleton>().Path = mapController.path;
        }
    }


}
