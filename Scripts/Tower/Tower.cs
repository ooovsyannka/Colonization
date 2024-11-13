using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private TowerScaner _scaner;
    [SerializeField] private TowerUnitHolder _unitHolder;
    [SerializeField] private TowerCounter _counter;

    private Queue<Resurce> _resurcePositions;

    private void Awake()
    {
        _resurcePositions = new Queue<Resurce>();
    }

    private void OnEnable()
    {
        _scaner.ResurceDetected += AddResurcePosition;
        _unitHolder.UnitReturned += TrySendUnitForResurce;
        _unitHolder.UnitReturned += _counter.CountUpdate;
    }

    private void OnDisable()
    {
        _scaner.ResurceDetected -= AddResurcePosition;
        _unitHolder.UnitReturned -= TrySendUnitForResurce;
        _unitHolder.UnitReturned -= _counter.CountUpdate;
    }

    private void AddResurcePosition(Resurce resurce)
    {
        _resurcePositions.Enqueue(resurce);

        TrySendUnitForResurce();
    }

    private void TrySendUnitForResurce()
    {
        if (_resurcePositions.Count > 0)
        { 
            if (_unitHolder.TrySendActiveUnit(out Unit unit))
            {
                unit.GetComponent<UnitMover>().StartMoveToResurce(_resurcePositions.Dequeue());
            }
        }
    }
}
