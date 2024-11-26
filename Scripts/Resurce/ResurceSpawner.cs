using System;
using System.Collections;
using UnityEngine;

public class ResurceSpawner : MonoBehaviour
{
    [SerializeField] private Resource _resurcePrefab;
    [SerializeField] private int _maxNumberPosition;
    [SerializeField] private int _minNumberPosition;
    [SerializeField] private float _spawnDelay;

    private Spawner<Resource> _spawner;
    private WaitForSeconds _spawnWait;
    private float _maxRayCastDistance = 50;

    public event Action<Resource> ResurceSpawned;

    private void Awake()
    {
        _spawner = new Spawner<Resource>(_resurcePrefab);
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
            Resource resource = _spawner.Spawn(NewSpawnPosition());
            resource.gameObject.SetActive(true);
            resource.TryGetComponent(out Rigidbody resurceRigidbody);
            resurceRigidbody.isKinematic = true;

            resource.Died += ReturnResurceInSpawner;

            ResurceSpawned?.Invoke(resource);
            
            yield return _spawnWait;
        }
    }

    private Vector3 NewSpawnPosition()
    {
        Vector3 newSpawnPosition = new Vector3(GetRandomNumber(), 0, GetRandomNumber());

        SetPositionY(ref newSpawnPosition);
        return newSpawnPosition;
    }

    private void ReturnResurceInSpawner(Resource resurce)
    {
        _spawner.ReturnObjectInPool(resurce);
        resurce.Died -= ReturnResurceInSpawner;
    }

    private void SetPositionY(ref Vector3 newSpawnPosition)
    {
        if (Physics.Raycast(newSpawnPosition + Vector3.up * _maxRayCastDistance, Vector3.down, out RaycastHit hit, _maxRayCastDistance))
        {
            if (hit.collider.TryGetComponent<Terrain>(out _))
            {  
                newSpawnPosition.y = hit.point.y;
            }
        }
    }

    private float GetRandomNumber()
    {
        return UnityEngine.Random.Range(_minNumberPosition, _maxNumberPosition);
    }
}
