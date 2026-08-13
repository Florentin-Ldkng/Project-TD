using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private OutlineHelper OutlineHelper;
    public Camera _camera;
    public GameManager _gameManager;
    public MapController _mapController;

    public Material material;
    private Ray ray;
    public GameObject currGameobject;
    public GameObject prevGameobject;

    public List<GameObject> TowerPrefabs;

    private Vector2 lastPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        OutlineHelper = new OutlineHelper();
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

        Vector2 ContextValue = context.ReadValue<Vector2>();

        if (Vector2.Distance(lastPosition, ContextValue) < 50f)
        {
            return;
        }

        lastPosition = ContextValue;

        if (_camera != null)
        {
            ray = _camera.ScreenPointToRay(ContextValue);
        }

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100, OutlineHelper.layerMask))
        {
            if (hit.collider.gameObject.CompareTag("Placeable") || hit.collider.gameObject.CompareTag("Tower"))
            {
                if (hit.collider.gameObject != currGameobject)
                {
                    currGameobject = hit.collider.gameObject;

                    OutlineHelper.SetIndex(currGameobject, true);


                    if (prevGameobject != null)
                    {
                        OutlineHelper.SetIndex(prevGameobject, false);
                    }

                    prevGameobject = currGameobject;
                }

            }
            else
            {
                if (prevGameobject != null)
                {
                    OutlineHelper.SetIndex(prevGameobject, false);
                    currGameobject = null;
                    prevGameobject = null;
                }
            }

        }
        else
        {
            if (prevGameobject != null)
            {
                OutlineHelper.SetIndex(prevGameobject, false);
                currGameobject = null;
                prevGameobject = null;
            }
        }
    }
    public void MouseClick(InputAction.CallbackContext context)
    {
        if (currGameobject != null && _gameManager.Gold >= 10)
        {
            if (context.ReadValueAsButton() && currGameobject.CompareTag("Placeable"))
            {
                var tempBuffer = Instantiate(TowerPrefabs[0], currGameobject.transform.position, Quaternion.identity);

                tempBuffer.transform.SetParent(currGameobject.transform.parent, true);

                currGameobject.transform.SetParent(tempBuffer.transform, true);

                OutlineHelper.SetIndex(currGameobject, false);

                currGameobject.tag = "Tower";

                OutlineHelper.SetIndex(currGameobject, true);

                prevGameobject = currGameobject;

                _gameManager.Gold -= 10;

                //_mapController.towers.Add(tempBuffer);
            }
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
