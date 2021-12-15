using UnityEngine;

public class StateCompletedTransition : Transition
{
    [SerializeField] private State _previousState;

    private void OnEnable()
    {
        _previousState.Completed += OnStateCompleted;
    }

    private void OnDisable()
    {
        NeedTransit = false;
        _previousState.Completed -= OnStateCompleted;
    }

    private void OnStateCompleted()
    {
        NeedTransit = true;
    }
}