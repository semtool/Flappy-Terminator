using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(UniversalContactsDetector))]
[RequireComponent(typeof(EnemyWeaponController))]
public class Enemy : MonoBehaviour
{
    private EnemyWeaponController _enemyVision;

    public event Action<Vector2> HasCoordinates;

    private void Awake()
    {
        _enemyVision = GetComponent<EnemyWeaponController>();
    }

    private void OnEnable()
    {
        _enemyVision.AimIsDetected += SendCoordOfStart;
    }

    public float GetRandomOffset(float minOffsetOfPosition, float maxOffsetOfPosition)
    {
        return Random.Range(minOffsetOfPosition, maxOffsetOfPosition);
    }

    private void SendCoordOfStart(Vector2 coordinates)
    {
        HasCoordinates?.Invoke(coordinates);
    }

    private void OnDisable()
    {
        _enemyVision.AimIsDetected -= SendCoordOfStart;
    }
}