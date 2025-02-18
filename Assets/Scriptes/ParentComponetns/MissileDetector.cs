using System;
using UnityEngine;

public abstract class MissileDetector : MonoBehaviour
{
    public  Action<GameObject> IsStricked;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Barrier barrier))
        {
            IsStricked?.Invoke(gameObject);
        }

        DetectEnemy(collision);
    }

    public abstract void DetectEnemy(Collider2D collision);
}