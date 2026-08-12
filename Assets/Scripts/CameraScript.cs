using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class CameraScript : MonoBehaviour
{
    public GameManager GameManager;
    
    public Vector2 expected = Vector2.zero;   
    
    public Vector3 actual = Vector3.zero;
    public Vector3 current = Vector3.zero;
    public Vector3 moveVector = Vector3.zero;

    public Vector3 scrollVector = Vector3.zero;
    public Vector3 scrollActual = Vector3.zero;

    float lastDirection = 0;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.position = new Vector3((GameManager.MapSize * -4) / 2, GameManager.MapSize * 5, GameManager.MapSize * 0.5f);
        scrollActual = Vector3.down * GameManager.MapSize;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        Vector3 test = Vector3.zero;

        actual = Vector3.SmoothDamp(current, moveVector + scrollVector, ref test , .07f);

        this.transform.Translate(actual, Space.World);
        this.transform.position = new Vector3(this.transform.position.x,Mathf.Clamp(this.transform.position.y, 10, GameManager.MapSize * 10),this.transform.position.z);
                
        current = actual;

        if (scrollVector != Vector3.zero)
        {
            scrollVector = Vector3.Lerp(scrollVector, Vector3.zero, 1f);   
        }

    }
    public void CameraMove(InputAction.CallbackContext context)
    {
        current = moveVector;
        expected = context.ReadValue<Vector2>() * ((this.transform.position.y / GameManager.MapSize * 10) / 100);
        moveVector = new Vector3(-expected.x, 0, -expected.y);
    }

    public void Scroll(InputAction.CallbackContext context)
    {
        lastDirection = context.ReadValue<Vector2>().y;
        scrollVector += scrollActual * lastDirection;
    }

}
