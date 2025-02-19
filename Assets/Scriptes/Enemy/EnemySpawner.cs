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

    private WaitForSeconds _delay;
    private int _minLayerNumber = 1;
    private int _maxLayerNumber = 5;
    private Coroutine _corutineForSignals;

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

        enemy.SendSignalToMissale(CoordinatsHasReceived);

        enemy.GetComponent<UniversalMover>().Fly();

        enemy.GetComponent<UniversalContactsDetector>().IsTouched += PutEnemyToPoolFromBarrier;

        enemy.GetComponent<UniversalContactsDetector>().IsDestroyed += PutEnemyToPoolFromMissile;

        enemy.GetComponent<SpriteRenderer>().sortingOrder = GetRandomOffset(_minLayerNumber, _maxLayerNumber);
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
        }
    }

    private void PutEnemyToPoolFromMissile(GameObject gameObject)
    {
        if (gameObject.TryGetComponent(out Enemy enemy))
        {
            _enemyPool.PutObjectToPool(enemy);

            IsDestroyed?.Invoke();

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