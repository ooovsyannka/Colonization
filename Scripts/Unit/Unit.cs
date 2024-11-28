using System;
using System.Collections;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

[RequireComponent(typeof(UnitMover))]
public class Unit : MonoBehaviour
{
    [SerializeField] private UnitTrailer _trailer;
    [SerializeField] private UnitAnimation _animation;

    private UnitMover _mover;

    public event Action<Unit> ResourceDelivered;
    public event Action<Resource, Unit> ResourceUnloaded;

    private void Awake()
    {
        _mover = GetComponent<UnitMover>();
    }

    private void Update()
    {
        _animation.PlayAnimation(_mover.IsMove);
    }

    public void StartDeliveryResource(Resource resource)
    {
        StartCoroutine(DeliveryResource(resource));
    }

    private IEnumerator DeliveryResource(Resource resource)
    {
        yield return _mover.MoveToResurce(resource);
        
        _animation.PlayProcessing();

        yield return _trailer.UploadResurce(resource);

        yield return _mover.MoveToBase();

        _animation.PlayProcessing();
        
        yield return _trailer.UnloadResurce(ResourceUnloaded, this);

        resource.Die(); 
        ResourceDelivered?.Invoke(this);
    }
}
