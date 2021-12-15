using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Movement), typeof(Health))]
public class MoveAnimation : MonoBehaviour
{
    private Animator _animator;
    private Movement _movement;
    private Health _health;

    private const string Speed = "Speed";
    private const string Jump = "Jump";
    private const string Landing = "Landing";
    private const string Falling = "Falling";

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _movement = GetComponent<Movement>();
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _movement.Jumped += OnJumped;
        _movement.Landed += OnLanded;
        _movement.StartFalling += OnStartFalling;
        _movement.Moves += OnMoves;
    }

    private void OnDisable()
    {
        _movement.Jumped -= OnJumped;
        _movement.Landed -= OnLanded;
        _movement.StartFalling -= OnStartFalling;
        _movement.Moves -= OnMoves;
    }

    private void OnJumped()
    {
        _animator.SetTrigger(Jump);
    }

    private void OnLanded()
    {
        if (_health.IsAlive)
            _animator.SetTrigger(Landing);
    }

    private void OnStartFalling()
    {
        if (_health.IsAlive)
            _animator.SetTrigger(Falling);
    }

    private void OnMoves(float speed)
    {
        _animator.SetFloat(Speed, Mathf.Abs(speed));
    }
}