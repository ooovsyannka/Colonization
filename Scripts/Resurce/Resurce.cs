using System;
using System.Collections;
using UnityEditorInternal;
using UnityEngine;

[RequireComponent (typeof(Collider), typeof(Rigidbody))]

public class Resurce : MonoBehaviour 
{
    [SerializeField] private float _timeToDie;

    private Coroutine _dieDelay;
private WaitForSeconds _dieDelayWait;

    public event Action<Resurce> Died;

    private void Awake()
    {
        _dieDelayWait = new WaitForSeconds(_timeToDie);
    }

    public void Die()
    {
        if(_dieDelay != null)
            StopCoroutine(_dieDelay);

        _dieDelay = StartCoroutine(DieDelay());
    }

    private IEnumerator DieDelay()
    {
        yield return _dieDelay;

        Died?.Invoke(this);
    }
}
