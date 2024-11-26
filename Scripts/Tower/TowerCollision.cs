using System;
using UnityEngine;

public class TowerCollision : MonoBehaviour
{
    public event Action<Resource> ResurceDetected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Resource resurce))
        {
            ResurceDetected?.Invoke(resurce);
            resurce.Die();  
        }
    }
}