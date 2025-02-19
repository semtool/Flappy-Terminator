using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(UniversalContactsDetector))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private int _missileStartInterval;
    
    private WaitForSeconds _delay;

    private void Awake()
    {
        _delay = new WaitForSeconds(_missileStartInterval);
    }

    public float GetRandomOffset(float minOffsetOfPosition, float maxOffsetOfPosition)
    {
        return Random.Range(minOffsetOfPosition, maxOffsetOfPosition);
    }

    public void SendSignalToMissale(Action<Vector2> action)
    {
        StartCoroutine(Inform(action));
    }

    private IEnumerator Inform(Action<Vector2> action)
    {
        while (true)
        {
            yield return _delay;

            action?.Invoke(transform.position);
        }
    }
}