using System.Collections;
using UnityEngine;

public class UniversalMover : MonoBehaviour
{
    [SerializeField] private float _speed;

    public void Fly()
    {
        StartCoroutine(Move());
    }

    private IEnumerator Move()
    {
        while (enabled)
        {
            transform.position = new Vector2(transform.position.x - _speed * Time.deltaTime, transform.position.y);

            yield return null;
        }
    }
}