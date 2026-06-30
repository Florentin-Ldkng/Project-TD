using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class BallistaAttackPoints : MonoBehaviour
{
    public List<GameObject> Attackpoints = new List<GameObject>();
    public event System.Action OnRotationLimitHit;

    public GameObject errorPoint;
    public void OnTriggerEnter(Collider other)
    {
        errorPoint = other.gameObject;
        OnRotationLimitHit?.Invoke();
    }

}
