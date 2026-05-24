using UnityEngine;

public class TestScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {        
        var test = gameObject.GetComponent<AudioSource>().loop = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
