using System;
using System.Collections;
using UnityEngine;

public class EnemyWeaponController : MonoBehaviour
{
    [SerializeField] private int _maxDistance;
    [SerializeField] private LayerMask _layerMask;

    private Coroutine _coroutineForMonitoring;
    private Coroutine _coroutineForSignals;
    private Coroutine _coroutineForTimer;
    private int _numberOfLaunches = 2;
    private int _timeForRecharge = 3;
    private int _secondsCounter = 0;
    private WaitForSeconds _wait;
    private float _intervalToNextLaunch = 0.5f;

    public event Action<Vector2> AimIsDetected;

    private void Awake()
    {
        _wait = new WaitForSeconds(_intervalToNextLaunch);
    }

    public void MonitorSpace()
    {
        if (_coroutineForMonitoring != null)
        {
            _coroutineForMonitoring = null;
        }

        _coroutineForMonitoring = StartCoroutine(LookingAhead());
    }

    public void RechargeWeaponStstem()
    {
        _coroutineForSignals = null;
        _coroutineForTimer = null;
        _secondsCounter = 0;
    }

    private IEnumerator LookingAhead()
    {
        while (enabled)
        {
            Detect();

            yield return null;
        }
    }

    private void Detect()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(-1, 0), _maxDistance, _layerMask);

        if (hit.collider != null)
        {
            if (_coroutineForSignals == null)
            {
                _coroutineForSignals = StartCoroutine(SendSignal());
            }
        }

        if (_secondsCounter == _timeForRecharge)
        {
            RechargeWeaponStstem();
        }
    }

    private void StartTimer()
    {
        if (_coroutineForTimer == null)
        {
            _coroutineForTimer = StartCoroutine(Timer());
        }
    }

    private IEnumerator SendSignal()
    {
        int _launchCounter = 0;

        while (_launchCounter < _numberOfLaunches)
        {
            AimIsDetected?.Invoke(transform.position);

            _launchCounter++;

            yield return _wait;
        }

        StartTimer();
    }

    private IEnumerator Timer()
    {
        while (_secondsCounter < _timeForRecharge)
        {
            var wait = new WaitForSeconds(1);

            yield return wait;

            _secondsCounter++;
        }
    }
}