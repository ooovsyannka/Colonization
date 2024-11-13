using System;
using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class UnitMover : MonoBehaviour
{
    [SerializeField] private float _distance;
    [SerializeField] private float _speed;
    [SerializeField] private float _speedRotation;

    private Coroutine _moveTo;
    private Vector3 _basePositon;

    public bool IsMove { get; private set; }

    public event Action<Resurce> ArriveAtResurce;
    public event Action ArriveAtBase;

    private void OnEnable()
    {
        _basePositon = transform.position;
    }

    public void StartMoveToBase()
    {
        if (_moveTo != null)
            StopCoroutine(_moveTo);

        _moveTo = StartCoroutine(MoveToBase());
    }

    public void StartMoveToResurce(Resurce resurce)
    {
        if (_moveTo != null)
            StopCoroutine(_moveTo);

        _moveTo = StartCoroutine(MoveToResurce(resurce));
    }

    private IEnumerator MoveToResurce(Resurce resurce)
    {  
        Vector3 resurcePosition = new Vector3(resurce.transform.position.x, resurce.transform.position.y, resurce.transform.position.z);

        while (IsArrived(resurcePosition) == false)
        {
            Move(resurcePosition);

            yield return null;
        }

        IsMove = false;
        ArriveAtResurce?.Invoke(resurce);
    }

    private IEnumerator MoveToBase()
    {
        while (IsArrived(_basePositon) == false)
        {
            Move(_basePositon);

            yield return null;
        }

        IsMove = false;
        ArriveAtBase?.Invoke();
    }

    private void Move(Vector3 targetPosition)
    {
        IsMove = true;
        transform.Translate(Vector3.forward * _speed * Time.deltaTime);

        LookAtTarget(targetPosition);
    }

    private bool IsArrived(Vector3 targetPostion)
    {
        return transform.position.IsEnoughClose(targetPostion, _distance);
    }

    private void LookAtTarget(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _speedRotation);
    }
}