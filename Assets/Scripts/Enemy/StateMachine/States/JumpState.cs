using UnityEngine;

[RequireComponent(typeof(Movement))]
public class JumpState : State
{
    private Movement _movement;
    private bool _isJump;

    private void Awake()
    {
        _movement = GetComponent<Movement>();
    }

    private void OnEnable()
    {
        _movement.Landed += OnLanded;
    }

    private void OnDisable()
    {
        _isJump = false;
        _movement.Move(0f);
        _movement.Landed -= OnLanded;
    }

    private void Update()
    {
        float direction = (transform.position - Target.transform.position).normalized.x;
        _movement.Move(direction);

        if (_isJump == false)
            _isJump = _movement.TryJump();
    }

    private void OnLanded()
    {
        OnCompleted();
    }
}