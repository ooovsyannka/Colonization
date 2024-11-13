using System;
using UnityEngine;

[RequireComponent (typeof(Collider), typeof(Rigidbody))]

public class Resurce : MonoBehaviour 
{
    public event Action<Resurce> Died;

    private void OnDisable()
    {
        Died?.Invoke(this);
    }
}
