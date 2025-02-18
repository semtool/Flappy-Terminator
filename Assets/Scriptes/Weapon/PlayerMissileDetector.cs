using UnityEngine;

public class PlayerMissileDetector : MissileDetector
{
    public override void DetectEnemy(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            IsStricked?.Invoke(gameObject);
        }
    }
}