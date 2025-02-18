using UnityEngine;

public class EnemyMissileDetector : MissileDetector
{
    public override  void DetectEnemy(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            IsStricked?.Invoke(gameObject);
        }
    }
}