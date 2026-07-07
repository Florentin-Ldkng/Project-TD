using System.Collections.Generic;
using UnityEngine;

public class BallistaAttackPoints : MonoBehaviour
{
    public List<GameObject> Attackpoints = new List<GameObject>();

    public List<GameObject>Shootpoints = new List<GameObject>();
    public event System.Action OnRotationLimitHit;

    public GameObject errorPoint;
    public void OnTriggerEnter(Collider other)
    {
        errorPoint = other.gameObject;
        OnRotationLimitHit?.Invoke();
    }

}
