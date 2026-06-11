using System;
using System.Collections.Generic;
using UnityEngine;

public class BaseSkeleton : MonoBehaviour
{
    public List<GameObject> Path;

    public Animator animator;

    public int index = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        this.transform.LookAt(Path[index].transform.position + (Vector3.up * .5f));
        this.transform.Translate((Vector3.forward * 0.6f) * Time.deltaTime);
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
