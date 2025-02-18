using System;
using UnityEngine;

public class UniversalContactsDetector : MonoBehaviour
{
    public event Action<GameObject> IsTouched;
    public event Action<GameObject> IsDestroyed;
    public event Action IsAttacted;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            IsTouched?.Invoke(collision.gameObject);
        }

        if (collision.gameObject.TryGetComponent(out Barrier barrier))
        {
            IsTouched?.Invoke(gameObject);
        }

        if (collision.gameObject.TryGetComponent(out Missile missile))
        {
            if (missile is EnemyMissile)
            {
                IsAttacted?.Invoke();
            }

            if (missile is PlayerMissile)
            {
                IsDestroyed?.Invoke(gameObject);
            }
        }
    }
}