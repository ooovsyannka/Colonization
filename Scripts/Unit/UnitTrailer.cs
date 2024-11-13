using System;
using System.Collections;
using UnityEngine;

public class UnitTrailer : MonoBehaviour
{
    [SerializeField] private UnitAnimation _animation;
    [SerializeField] private Transform _resurseParent;

    private Coroutine _processingDelay;
    private Coroutine _uploadDelay;
    private Coroutine _unloadDelay;
    private WaitForSeconds _processingWait;
    private WaitForSeconds _transmitWait;
    private Resurce  _resurce;

    public bool IsUpload { get; private set; }

    public event Action ResurceUploaded;
    public event Action ResurceUnloaded;

    private void Awake()
    {
        float divisionDuration = 2;

        _processingWait = new WaitForSeconds(_animation.ProcessingAnimationDuration.length);
        _transmitWait = new WaitForSeconds(_animation.ProcessingAnimationDuration.length / divisionDuration);
    }

    public void UploadResurce(Resurce resurce)
    {
        _resurce = resurce;
        _animation.PlayProcessing();

        StartProcessingCoroutine( ResurceUploaded);

        if (_uploadDelay != null)
            StopCoroutine(_uploadDelay);

        _uploadDelay = StartCoroutine(UploadDelay());
    }

    public void UnloadResurce()
    {
        _animation.PlayProcessing();

        StartProcessingCoroutine( ResurceUnloaded);

        if (_unloadDelay != null)
            StopCoroutine(_unloadDelay);

        _unloadDelay = StartCoroutine(UnloadDelay());
    }

    private void StartProcessingCoroutine( Action processing = null)
    {
        if (_processingDelay != null)
            StopCoroutine(_processingDelay);

        _processingDelay = StartCoroutine(ProcessingAnimationDelay(processing));
    }

    private IEnumerator ProcessingAnimationDelay(Action processing)
    {
        yield return _processingWait;

        processing?.Invoke();
    }

    private IEnumerator UploadDelay()
    {
        yield return _transmitWait;

        _resurce.transform.parent = _resurseParent;
        _resurce.transform.localPosition = Vector3.zero;
    }

    private IEnumerator UnloadDelay()
    {
        yield return _transmitWait;

        _resurce.transform.parent = null;
        _resurce.GetComponent<Rigidbody>().isKinematic = false;
        _resurce.Die();
        _resurce = null;
    }
}