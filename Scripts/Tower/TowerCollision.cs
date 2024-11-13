using System;
using UnityEngine;

public class TowerCollision : MonoBehaviour
{
    public event Action ResurceDetected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Resurce resurce)) 
        {
            ResurceDetected?.Invoke();
        }
    }
}