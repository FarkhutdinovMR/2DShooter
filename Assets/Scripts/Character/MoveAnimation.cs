using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Movement), typeof(Health))]
public class MoveAnimation : MonoBehaviour
{
    private Animator _animator;
    private Movement _movement;
    private Health _health;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _movement = GetComponent<Movement>();
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _movement.Jumped += OnJumped;
        _movement.Landed += OnLanded;
        _movement.StartedFall += OnStartFalling;
        _movement.Moved += OnMoves;
    }

    private void OnDisable()
    {
        _movement.Jumped -= OnJumped;
        _movement.Landed -= OnLanded;
        _movement.StartedFall -= OnStartFalling;
        _movement.Moved -= OnMoves;
    }

    private void OnJumped()
    {
        _animator.SetTrigger(AnimatorCharacterController.States.Jump);
    }

    private void OnLanded()
    {
        if (_health.IsAlive)
            _animator.SetTrigger(AnimatorCharacterController.States.Landing);
    }

    private void OnStartFalling()
    {
        if (_health.IsAlive)
            _animator.SetTrigger(AnimatorCharacterController.States.Falling);
    }

    private void OnMoves(float speed)
    {
        _animator.SetFloat(AnimatorCharacterController.Params.Speed, Mathf.Abs(speed));
    }
}