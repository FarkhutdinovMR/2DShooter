using UnityEngine;

[RequireComponent (typeof(Health))]
public class DeathTransition : Transition
{
    private Health _health;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _health.Died += OnDied;
    }

    private void OnDisable()
    {
        _health.Died -= OnDied;
        NeedTransit = false;
    }

    private void OnDied()
    {
        NeedTransit = true;
    }
}