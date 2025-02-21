using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private Scaner _scaner;
    [SerializeField] private TowerUnitHolder _unitHolder;
    [SerializeField] private TowerResourceHolder _resourceHolder;

    public event Action<Resource> ResourceReceived;

    private void OnEnable()
    {
        _scaner.ResourceDetected += TryAddResurce;
        _unitHolder.UnitReturned += TrySendUnitForResurce;
    }

    private void OnDisable()
    {
        _scaner.ResourceDetected -= TryAddResurce;
        _unitHolder.UnitReturned -= TrySendUnitForResurce;
    }

    private void TryAddResurce(Resource detectedResource)
    {
        if (_resourceHolder.CanAddResurce(detectedResource))
        {
            _resourceHolder.AddResurce(detectedResource);
            TrySendUnitForResurce();
        }
    }

    private void TrySendUnitForResurce()
    {
        if (_unitHolder.HasActiveUnits)
        {
            if (_resourceHolder.TryGetFreeResurce(out Resource resource))
            {
                if (_unitHolder.TrySendActiveUnit(out Unit unit))
                {
                    unit.StartDeliveryResource(resource);
                    unit.ResourceUnloaded += GetResource;
                }
            }
        }
    }

    private void GetResource(Resource resource, Unit unit)
    {
        ResourceReceived?.Invoke(resource);
        unit.ResourceUnloaded -= GetResource;
    }
}
