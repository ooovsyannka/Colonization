using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] private Unit _unitPrefab;

    private Spawner<Unit> _spawner;

    private void Awake()
    {
        _spawner = new Spawner<Unit>(_unitPrefab);
    }

    public Unit SpawnUnit(Vector3 spawnPosition)
    {
        Unit unit = _spawner.Spawn(spawnPosition);
        unit.gameObject.SetActive(true);

        return unit;
    }
}