using UnityEngine;

[RequireComponent(typeof(InputReader))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speedX;
    [SerializeField] private float _topLimit;
    [SerializeField] private float _botomLimit;

    private InputReader _inputReader;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move(_inputReader.VerticalalDirection);

        transform.position = new Vector2(transform.position.x, Mathf.Clamp(transform.position.y, _botomLimit, _topLimit));
    }

    private void Move(float verticalDirection)
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _speedX * verticalDirection * Time.fixedDeltaTime);
    }
}