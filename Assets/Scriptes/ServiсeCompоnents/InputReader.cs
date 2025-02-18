using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    private const string HorizontalAxis = "Horizontal";
    private const string VerticalAxis = "Vertical";

    [SerializeField] private GameStarter _gameStarter;

    private KeyCode Shoot = KeyCode.Space;
    private bool _isUnblocked = false;

    public event Action IsFired;

    public float HorizontalDirection { get; private set; }
    public float VerticalalDirection { get; private set; }

    private void OnEnable()
    {
        _gameStarter.WeaponInUnbloked += AllowSoot;
    }

    private void Update()
    {
        HorizontalDirection = Input.GetAxis(HorizontalAxis);
        VerticalalDirection = Input.GetAxis(VerticalAxis);


        if (Input.GetKeyDown(Shoot))
        {
            if (_isUnblocked)
            {
                IsFired?.Invoke();
            }
        }
    }

    private void AllowSoot()
    {
        _isUnblocked = true;
    }

    private void OnDisable()
    {
        _gameStarter.WeaponInUnbloked -= AllowSoot;
    }
}