using UnityEngine;
using UnityEngine.InputSystem;

public class MouseController : MonoBehaviour
{
    public Camera camera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MouseMove(InputAction.CallbackContext context)
    {
        // read the value for the "move" action each event call
        //Debug.Log(context.ReadValue<Vector2>());
        var ray = camera.ScreenPointToRay(context.ReadValue<Vector2>());

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (!hit.collider.gameObject.CompareTag("Path"))
            {
          
                Destroy(hit.collider.gameObject);
            }
            
        }
    }
}
