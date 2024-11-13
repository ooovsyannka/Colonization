using System;
using UnityEngine;

public class TowerScaner : MonoBehaviour
{
    [SerializeField] private ResurceSpawner _resurceSpawner;

    public event Action<Resurce> ResurceDetected;

    private void OnEnable()
    {
        _resurceSpawner.ResurceSpawned += ResurceDetect;
    }

    private void OnDisable()
    {
        _resurceSpawner.ResurceSpawned -= ResurceDetect;
    }

    private void ResurceDetect(Resurce resurce)
    {
        ResurceDetected?.Invoke(resurce);
    }
}