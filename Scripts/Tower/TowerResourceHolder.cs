using System.Collections.Generic;
using UnityEngine;

public class TowerResourceHolder : MonoBehaviour
{
    [SerializeField] private TowerCollision _collision;

    private Dictionary<Resource, bool> _freeResurces;

    private void Awake()
    {
        _freeResurces = new Dictionary<Resource, bool>();
    }

    private void OnEnable()
    {
        _collision.ResurceDetected += RemoveResource;
    }

    public bool CanAddResurce(Resource detectedResource)
    {
        if (_freeResurces.Count == 0)
            return true;

        if (_freeResurces.ContainsKey(detectedResource))
            return false;

        return true;
    }

    public void AddResurce(Resource detectedResource)
    {
        _freeResurces.Add(detectedResource, false);
    }

    public bool TryGetFreeResurce(out Resource desiredResource)
    {
        desiredResource = null;

        if (_freeResurces.Count > 0)
        {
            foreach (KeyValuePair<Resource, bool> resource in _freeResurces)
            {
                if (resource.Value == false)
                {
                    desiredResource = resource.Key;
                    _freeResurces[desiredResource] = true;

                    return true;
                }
            }
        }

        return false;
    }

    private void RemoveResource(Resource resource)
    {
        _freeResurces.Remove(resource);
    }
}