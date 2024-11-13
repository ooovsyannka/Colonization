using System;
using System.Collections.Generic;
using UnityEngine;

public class TowerUnitHolder : MonoBehaviour
{
    [SerializeField] private UnitSpawner _unitSpawner;
    [SerializeField] private List<Transform> _unitPlaces;

    private Queue<Unit> _activeUnits;

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
            unit.GetTowerUnitHolder(this);

            _activeUnits.Enqueue(unit);
        }
    }

    public bool TrySendActiveUnit(out Unit unit)
    {
        unit = null;

        if (_activeUnits.Count > 0)
        {
            unit = _activeUnits.Dequeue();
            return true;
        }

        return false;
    }

    public void ReturnUnit(Unit unit)
    {
        _activeUnits.Enqueue(unit);
        UnitReturned?.Invoke();
    }
}
