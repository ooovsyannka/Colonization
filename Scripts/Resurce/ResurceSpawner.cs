using System;
using System.Collections;
using UnityEngine;

public class ResurceSpawner : MonoBehaviour
{
    [SerializeField] private Resurce _resurcePrefab;
    [SerializeField] private int _maxNumberPosition;
    [SerializeField] private int _minNumberPosition;
    [SerializeField] private float _spawnDelay;

    private Spawner<Resurce> _spawner;
    private WaitForSeconds _spawnWait;
    private float _maxRayCastDistance = 50;

    public event Action<Resurce> ResurceSpawned;

    private void Awake()
    {
        _spawner = new Spawner<Resurce>(_resurcePrefab);
        _spawnWait = new WaitForSeconds(_spawnDelay);
    }

    private void Start()
    {
        StartCoroutine(LoopSpawnResurce());
    }

    private IEnumerator LoopSpawnResurce()
    {
        while (enabled)
        {
            Vector3 newSpawnPosition = new Vector3(GetRandomPosition(), 0, GetRandomPosition());

            GetPositionY(ref newSpawnPosition);

            Resurce resurce = _spawner.Spawn(newSpawnPosition);
            resurce.gameObject.SetActive(true);
            resurce.GetComponent<Rigidbody>().isKinematic = true;

            resurce.Died += ReturnResurceInSpawner;

            ResurceSpawned?.Invoke(resurce);
            yield return _spawnWait;
        }
    }

    private void ReturnResurceInSpawner(Resurce resurce)
    {
        _spawner.ReturnObjectInPool(resurce);
        resurce.Died -= ReturnResurceInSpawner;
    }

    private void GetPositionY(ref Vector3 newSpawnPosition)
    {
        if (Physics.Raycast(newSpawnPosition + Vector3.up * _maxRayCastDistance, Vector3.down, out RaycastHit hit, _maxRayCastDistance))
        {
            newSpawnPosition.y = hit.point.y;
        }
    }

    private float GetRandomPosition()
    {
        return UnityEngine.Random.Range(_minNumberPosition, _maxNumberPosition);
    }
}