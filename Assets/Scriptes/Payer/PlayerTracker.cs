using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    [SerializeField] private PlayerMover _player;

    private void Update()
    {
        var position = transform.position;

        transform.position = position;
    }
}