using System;
using System.Collections.Generic;
using UnityEngine;

public class TowerUnitHolder : MonoBehaviour
{
    [SerializeField] private UnitFabric _unitSpawner;
    [SerializeField] private List<Transform> _unitPlaces;

    private Queue<Unit> _activeUnits;

    public bool HasActiveUnits { get { return _activeUnits.Count != 0; } }

    public event Action UnitReturned;

    private void Awake()
    {
        _activeUnits = new Queue<Unit>();
    }

    private void Start()
    {
        for (int i = 0; i < _unitPlaces.Count; i++)
        {
            Unit unit = _unitSpawner.SpawnUnit(_unitPlaces[i].position);

            _activeUnits.Enqueue(unit);
        }
    }

    public bool TrySendActiveUnit(out Unit unit)
    {
        unit = null;

        if (TryGetActiveUnit(out unit))
        {
            unit.ResourceDelivered += ReturnUnit;

            return true;
        }

        return false;
    }

    public void ReturnUnit(Unit unit)
    {
        _activeUnits.Enqueue(unit);
        UnitReturned?.Invoke();
        unit.ResourceDelivered -= ReturnUnit;
    }

    private bool TryGetActiveUnit(out Unit desiredUnit)
    {
        desiredUnit = null;

        if (_activeUnits.Count > 0)
        {
            desiredUnit = _activeUnits.Dequeue();
            return true;
        }

        return false;
    }
}
