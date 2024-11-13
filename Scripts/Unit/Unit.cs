using UnityEngine;

[RequireComponent(typeof(UnitMover))]

public class Unit : MonoBehaviour
{
    [SerializeField] private UnitTrailer _trailer;
    [SerializeField] private UnitAnimation _animation;

    private UnitMover _mover;
    private TowerUnitHolder _towerUnitHolder;
    
    private void Update()
    {
        _animation.PlayAnimation(_mover.IsMove);
    }

    private void Awake()
    {
        _mover = GetComponent<UnitMover>();
    }

    private void OnEnable()
    {
        _mover.ArriveAtResurce += _trailer.UploadResurce;
        _trailer.ResurceUploaded += _mover.StartMoveToBase;
        _mover.ArriveAtBase += _trailer.UnloadResurce;
        _trailer.ResurceUnloaded += ReturnInUnitHolder;
    }

    private void OnDisable()
    {
        _mover.ArriveAtResurce -= _trailer.UploadResurce;
        _trailer.ResurceUploaded -= _mover.StartMoveToBase;
        _mover.ArriveAtBase -= _trailer.UnloadResurce;
        _trailer.ResurceUnloaded -= ReturnInUnitHolder;
    }

    public void GetTowerUnitHolder(TowerUnitHolder towerUnitHolder)
    {
        _towerUnitHolder = towerUnitHolder;
    }

    private void ReturnInUnitHolder()
    {
        _towerUnitHolder.ReturnUnit(this);
    }
}
