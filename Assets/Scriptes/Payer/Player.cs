using System;
using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
[RequireComponent(typeof(UniversalContactsDetector))]
public class Player : MonoBehaviour
{
    private UniversalContactsDetector _contactsDetector;

    public event Action Disappeared;

    private void Awake()
    {
        _contactsDetector = GetComponent<UniversalContactsDetector>();
    }

    private void OnEnable()
    {
        _contactsDetector.IsAttacted += Disappear;
    }

    private void OnDisable()
    {
        _contactsDetector.IsAttacted -= Disappear;
    }

    private void Disappear()
    {
        Destroy(gameObject);

        Disappeared?.Invoke();
    }
}