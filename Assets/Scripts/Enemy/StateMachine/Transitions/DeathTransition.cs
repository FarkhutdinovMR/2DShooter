using UnityEngine;


public class DeathTransition : Transition
{
    private Health _health;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _health.Dies += OnDies;
    }

    private void OnDisable()
    {
        _health.Dies -= OnDies;
        NeedTransit = false;
    }

    private void OnDies()
    {
        NeedTransit = true;
    }
}