using System.Collections;
using UnityEngine;

public class BackgroundMover : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private float _speed;

    private float _minPositionX;
    private Vector2 _restartPosition;

    private void Awake()
    {
        _restartPosition = transform.position;
        _minPositionX = _sprite.bounds.size.x  * 2 - _restartPosition.x;
    }

    public void Start()
    {
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        while (enabled)
        {
            transform.Translate(Vector2.left * _speed * Time.deltaTime);

            if (transform.position.x <= -_minPositionX)
            {
                transform.position = _restartPosition;
            }

            yield return null;
        }
    }
}