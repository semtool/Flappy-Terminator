using System;
using UnityEngine;

public class PlayerMissileSpawner : AmmunitionSpawner
{
    [SerializeField] private PlayerMissilePool _missilePool;
    [SerializeField] private Player _player;
    [SerializeField] Barrier _barrier;

    private InputReader _input;

    public event Action EnemyIsDestoyed;

    private void Awake()
    {
        _input = _player.GetComponent<InputReader>();
    }

    private void OnEnable()
    {
        _input.IsFired += Launch;
        _missilePool.ObjectIsInPool += Unsubscribe;
    }

    private void Launch()
    {
        PlayerMissile missile = _missilePool.GetObjectFromPool();

        missile.transform.position = _player.transform.position;

        missile.GetComponent<UniversalMover>().Fly();

        missile.GetComponent<MissileDetector>().IsStricked += PutToWeaponPool;
    }

    private void Unsubscribe(PlayerMissile missile)
    {
        missile.GetComponent<MissileDetector>().IsStricked -= PutToWeaponPool;
    }

    private void PutToWeaponPool(GameObject gameObject)
    {
        if (gameObject.TryGetComponent(out PlayerMissile missile))
        {
            _missilePool.PutObjectToPool(missile);
        }
    }
}
