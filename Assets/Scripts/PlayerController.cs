using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Camera _camera;

    public Material material;
    private Ray ray;
    public GameObject currGameobject;
    public GameObject prevGameobject;

    public List<GameObject> TowerPrefabs;
    int layerMask;
    uint lightmaskYellow,lightmaskWhite,lightmaskDefault;
    uint noLine, yellowLine, whiteLine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        layerMask = LayerMask.GetMask("FloorCheck");
        lightmaskWhite = RenderingLayerMask.GetMask("Light Layer 1");
        lightmaskYellow = RenderingLayerMask.GetMask("Light Layer 2");
        lightmaskDefault= RenderingLayerMask.GetMask("Default");

        noLine = lightmaskDefault;
        whiteLine = lightmaskDefault | lightmaskWhite;
        yellowLine = lightmaskDefault | lightmaskYellow;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }
    public void MouseMove(InputAction.CallbackContext context)
    {
        // read the value for the "move" action each event call
        //Debug.Log(context.ReadValue<Vector2>());+
        
        if (_camera!=null)
        {
            ray = _camera.ScreenPointToRay(context.ReadValue<Vector2>());
        }
        

        
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit,100,layerMask))
        {
            if (hit.collider.gameObject.CompareTag("Placeable") || hit.collider.gameObject.CompareTag("Tower"))
            {
                if (hit.collider.gameObject != currGameobject)
                {
                    currGameobject = hit.collider.gameObject;

                    SetIndex(currGameobject, true);
                    

                    if (prevGameobject != null)
                    {
                        SetIndex(prevGameobject, false);
                    }                   

                    prevGameobject = currGameobject;
                }
                
            }
            else
            {
                if (prevGameobject != null)
                {
                    SetIndex(prevGameobject, false);
                    currGameobject = null;
                    prevGameobject= null;
                }
            }

        }
        else
        {
            if (prevGameobject != null)
            {
                SetIndex(prevGameobject, false);
                currGameobject = null;
                prevGameobject = null;
            }
        }
    }
    public void MouseClick(InputAction.CallbackContext context)
    {
        if (currGameobject != null)
        {
            if (context.ReadValueAsButton() && currGameobject.CompareTag("Placeable"))
            {
                var tempBuffer = Instantiate(TowerPrefabs[0], currGameobject.transform.position, Quaternion.identity);

                tempBuffer.transform.SetParent(currGameobject.transform.parent, true);

                currGameobject.transform.SetParent(tempBuffer.transform, true);

                SetIndex(currGameobject, false);

                currGameobject.tag = "Tower";            

                SetIndex(currGameobject, true);

                prevGameobject = currGameobject;
            }
        }
    }

    private void SetIndex(GameObject hit, bool Line)
    {
        Renderer tempRenderer = null;
        uint tempIndex = 0;

        switch (hit.tag)
        {
            case "Tower":
                tempRenderer = hit.transform.GetComponent<Renderer>();
                tempIndex = yellowLine;
                break;
            case "Placeable":
                tempRenderer = hit.GetComponent<Renderer>();
                tempIndex = whiteLine;
                break;
        }

        if (Line)
        {
            tempRenderer.renderingLayerMask = tempIndex;
        }
        else
        {
            tempRenderer.renderingLayerMask = noLine;
        }

    }

    public void RightMouseClick(InputAction.CallbackContext context)
    {
        if (currGameobject != null)
        {
            if (context.ReadValueAsButton() && currGameobject.CompareTag("Tower"))
            {
                currGameobject.transform.parent.Rotate(Vector3.up * 90);
            }
        }
    }   

}
