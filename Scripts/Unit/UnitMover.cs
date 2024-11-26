using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

public class UnitMover : MonoBehaviour
{
    [SerializeField] private float _distance;
    [SerializeField] private float _speed;
    [SerializeField] private float _speedRotation;

    private Coroutine _moveTo;
    private Vector3 _basePosition;

    public bool IsMove { get; private set; }

    public event Action ArriveAtResurce;
    public event Action ArriveAtBase;

    private void OnEnable()
    {
        _basePosition = transform.position;
    }

    public Coroutine MoveToResurce(Resource resurce)
    {
        return StartMoveTo(resurce.transform.position, ArriveAtResurce);
    }

    public Coroutine MoveToBase()
    {
        return StartMoveTo(_basePosition, ArriveAtBase);
    }

    private Coroutine StartMoveTo(Vector3 targetPosition, Action arriveAt)
    {
        if (_moveTo != null)
            StopCoroutine(_moveTo);

        return StartCoroutine(MoveTo(targetPosition, arriveAt));
    }

    private IEnumerator MoveTo(Vector3 targetPosition, Action arriveAt)
    {
        while (IsArrived(targetPosition) == false)
        {
            Move(targetPosition);

            yield return null;
        }

        IsMove = false;
        arriveAt?.Invoke();
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