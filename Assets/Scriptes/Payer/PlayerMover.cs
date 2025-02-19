using UnityEngine;

[RequireComponent(typeof(InputReader))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _topLimit;
    [SerializeField] private float _botomLimit;
    [SerializeField] private float _leftLimit;
    [SerializeField] private float _rightLimit;

    private InputReader _inputReader;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _inputReader = GetComponent<InputReader>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move(_inputReader.HorizontalDirection,_inputReader.VerticalalDirection);

        transform.position = new Vector2(Mathf.Clamp(transform.position.x, _leftLimit, _rightLimit), Mathf.Clamp(transform.position.y, _botomLimit, _topLimit));
    }

    private void Move(float horisontalDirection,float verticalDirection)
    {
        _rigidbody.velocity = new Vector2(_speed * horisontalDirection * Time.fixedDeltaTime, _speed * verticalDirection * Time.fixedDeltaTime);
    }
}