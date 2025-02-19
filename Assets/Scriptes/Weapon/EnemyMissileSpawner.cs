using UnityEngine;
using Vector2 = UnityEngine.Vector2;

[RequireComponent(typeof(MissileDetector))]
public class EnemyMissileSpawner : AmmunitionSpawner
{
    [SerializeField] EnemySpawner _enemySpawner;
    [SerializeField] EnemyMissilePool _missilePool;

    private void OnEnable()
    {
        _enemySpawner.CoordinatsHasReceived += Launch;
        _missilePool.ObjectIsInPool += Unsubscribe;
    }

    public void Launch(Vector2 vector)
    {
        EnemyMissile missile = _missilePool.GetObjectFromPool();

        missile.transform.position = vector;

        missile.GetComponent<UniversalMover>().Fly();

        missile.GetComponent<MissileDetector>().IsStricked += PutToWeaponPool;
    }

    private void PutToWeaponPool(GameObject gameObject)
    {
        if (gameObject.TryGetComponent(out EnemyMissile missile))
        {
            _missilePool.PutObjectToPool(missile);
        }
    }

    private void Unsubscribe(EnemyMissile missile)
    {
        missile.GetComponent<MissileDetector>().IsStricked -= PutToWeaponPool;
    }

    private void OnDisable()
    {
        _enemySpawner.CoordinatsHasReceived -= Launch;
        _missilePool.ObjectIsInPool -= Unsubscribe;
    }
}