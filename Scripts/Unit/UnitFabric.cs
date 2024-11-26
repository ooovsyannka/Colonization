using UnityEngine;

public class UnitFabric : MonoBehaviour
{
    [SerializeField] private Unit _unitPrefab;

    public Unit SpawnUnit(Vector3 spawnPosition)
    {
        Unit unit = Instantiate(_unitPrefab);
        unit.gameObject.SetActive(false);
        unit.transform.position = spawnPosition;
        unit.gameObject.SetActive(true);

        return unit;
    }
}