using System.Collections;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] EnemyPool _enemyPool;
    [SerializeField] Barrier _barrier;
    [SerializeField] private float _gorizontalStartPosition;
    [SerializeField] private float _minOffsetOfPosition;
    [SerializeField] private float _maxOffsetOfPosition;
    [SerializeField] private int _spawnTime;
    [SerializeField] private int _launchInterval;

    private WaitForSeconds _wait;
    private WaitForSeconds _delay;
    private int _minLayerNumber = 1;
    private int _maxLayerNumber = 5;
    private Coroutine _corutineForSignals;

    public event Action<Vector2> CoordinatsHasReceived;
    public event Action IsDestroy;

    private void Awake()
    {
        _wait = new WaitForSeconds(_spawnTime);
        _delay = new WaitForSeconds(_launchInterval);
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

            yield return _wait;
        }
    }

    private void SpawnItem()
    {
        Enemy enemy = _enemyPool.GetObjectFromPool();

        SetCoordinateOfAppearance(enemy);

        SendSignal(enemy);

        enemy.GetComponent<UniversalMover>().Fly();

        enemy.GetComponent<UniversalContactsDetector>().IsTouched += PutEnemyToPoolFromBarrier;

        enemy.GetComponent<UniversalContactsDetector>().IsDestroyed += PutEnemyToPoolFromMissile;

        enemy.GetComponent<SpriteRenderer>().sortingOrder = GetRandomOffset(_minLayerNumber, _maxLayerNumber);
    }

    private void SendSignal(Enemy enemy)
    {
        _corutineForSignals = StartCoroutine(Inform(enemy));
    }

    private IEnumerator Inform(Enemy enemy)
    {
        while (enabled)
        {
            CoordinatsHasReceived?.Invoke(enemy.transform.position);

            yield return _delay;
        }
    }

    private void SetCoordinateOfAppearance(Enemy enemy)
    {
        enemy.transform.position =
        new Vector2(_gorizontalStartPosition, transform.position.y + enemy.GetRandomOffset(_minOffsetOfPosition, _maxOffsetOfPosition));
    }

    private void PutEnemyToPoolFromBarrier(GameObject gameObject)
    {
        if (gameObject.TryGetComponent(out Enemy enemy))
        {
            _enemyPool.PutObjectToPool(enemy);

            if (_corutineForSignals != null)
            {
                StopCoroutine(_corutineForSignals);
            }
        }
    }

    private void PutEnemyToPoolFromMissile(GameObject gameObject)
    {
        if (gameObject.TryGetComponent(out Enemy enemy))
        {
            _enemyPool.PutObjectToPool(enemy);

            IsDestroy?.Invoke();

            if (_corutineForSignals != null)
            {
                StopCoroutine(_corutineForSignals);
            }
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
    }

    private void OnDisable()
    {
        _enemyPool.ObjectIsInPool -= Unsubscribe;
    }
}