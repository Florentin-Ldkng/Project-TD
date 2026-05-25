using UnityEngine;

public class EnemyTestScript : MonoBehaviour
{
    Vector3 m_Movement = Vector3.forward;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(m_Movement * Time.deltaTime);
        //this.gameObject.transform.position = Vector3.SmoothDamp(this.gameObject.transform.position, new Vector3(0, 1, 0), this.gameObject.transform.position, 2f);
    }
}
