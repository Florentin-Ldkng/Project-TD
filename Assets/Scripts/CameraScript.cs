using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public MapController controller;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = new Vector3((controller.mapSize * -4) / 2, controller.mapSize * 5, controller.mapSize * 0.5f);
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
