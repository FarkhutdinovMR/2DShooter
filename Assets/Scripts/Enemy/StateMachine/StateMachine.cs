using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class StateMachine : MonoBehaviour
{
    [SerializeField] private State _startState;

    private Enemy _enemy;
    private Player _target;
    private State _currentState;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    private void OnEnable()
    {
        _target = _enemy.Target;
        Transit(_startState);
    }

    private void Update()
    {
        if (_currentState == null)
            return;

        if (_currentState.CheckTransition(out State state))
            Transit(state);
    }

    private void Transit(State nextState)
    {
        if (_currentState != null)
            _currentState.Exit();

        _currentState = nextState;

        if (_currentState != null)
            _currentState.Enter(_target);
    }
}