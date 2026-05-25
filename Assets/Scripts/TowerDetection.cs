using UnityEngine;
using static UnityEditor.PlayerSettings;

public class TowerDetection : MonoBehaviour
{
    LineRenderer lineRenderer;
    bool enemy = false;
    Collider enemyCollider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy)
        {
            lineRenderer.SetPosition(0, this.gameObject.transform.position);
            lineRenderer.SetPosition(1, enemyCollider.gameObject.transform.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy entered");
            enemy = true;
            enemyCollider = other;
            SpawnLine();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy exit");
            enemy= false;
            Destroy(lineRenderer.gameObject);
        }
    }

    protected void SpawnLine()
    {
        lineRenderer = new GameObject("Line").AddComponent<LineRenderer>();
        lineRenderer.material.color = Color.greenYellow;
        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.positionCount = 2;
        lineRenderer.useWorldSpace = true;
        
    }
    
}
