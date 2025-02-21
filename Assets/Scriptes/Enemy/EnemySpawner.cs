using System.Collections;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] EnemyPool _enemyPool;
    [SerializeField] private float _minOffsetOfPosition;
    [SerializeField] private float _maxOffsetOfPosition;
    [SerializeField] private int _spawnTime;

    private WaitForSeconds _delay;
    private int _minLayerNumber = 1;
    private int _maxLayerNumber = 5;

    public event Action<Vector2> CoordinatsHasReceived;
    public event Action IsDestroyed;

    private void Awake()
    {
        _delay = new WaitForSeconds(_spawnTime);
    }

    private void OnEnable()
    {
        _enemyPool.ObjectIsInPool += Unsubscribe;
    }

    public void LaunchEnemies()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (enabled)
        {
            SpawnItem();

            yield return _delay;
        }
    }

    private void SpawnItem()
    {
        Enemy enemy = _enemyPool.GetObjectFromPool();

        SetCoordinateOfAppearance(enemy);

        enemy.GetComponent<EnemyWeaponController>().MonitorSpace();

        enemy.GetComponent<EnemyWeaponController>().AimIsDetected += SendCoordinatesOfStart;

        enemy.GetComponent<UniversalMover>().Fly();

        enemy.GetComponent<UniversalContactsDetector>().IsTouched += PutEnemyToPoolFromBarrier;

        enemy.GetComponent<UniversalContactsDetector>().IsDestroyed += PutEnemyToPoolFromMissile;

        enemy.GetComponent<SpriteRenderer>().sortingOrder = GetRandomOffset(_minLayerNumber, _maxLayerNumber);
    }

    private void SendCoordinatesOfStart(Vector2 vector)
    {
        CoordinatsHasReceived?.Invoke(vector);
    }

    private void SetCoordinateOfAppearance(Enemy enemy)
    {
        enemy.transform.position =
        new Vector2(transform.position .x, transform.position.y + enemy.GetRandomOffset(_minOffsetOfPosition, _maxOffsetOfPosition));
    }

    private void PutEnemyToPoolFromBarrier(GameObject gameObject)
    {

        PootToPool(gameObject);
    }

    private void PutEnemyToPoolFromMissile(GameObject gameObject)
    {
        PootToPool(gameObject);

        IsDestroyed?.Invoke();
    }


    private void PootToPool(GameObject gameObject)
    {
        if (gameObject.TryGetComponent(out Enemy enemy))
        {
            _enemyPool.PutObjectToPool(enemy);

            gameObject.GetComponent<EnemyWeaponController>().RechargeWeaponStstem();
        }
    }

    private int GetRandomOffset(int minOffsetOfPosition, int maxOffsetOfPosition)
    {
        return Random.Range(minOffsetOfPosition, maxOffsetOfPosition);
    }

    private void Unsubscribe(Enemy enemy)
    {
        enemy.GetComponent<UniversalContactsDetector>().IsTouched -= PutEnemyToPoolFromBarrier;
        enemy.GetComponent<UniversalContactsDetector>().IsDestroyed -= PutEnemyToPoolFromMissile;
        enemy.GetComponent<EnemyWeaponController>().AimIsDetected -= SendCoordinatesOfStart;
    }

    private void OnDisable()
    {
        _enemyPool.ObjectIsInPool -= Unsubscribe;
    }
}