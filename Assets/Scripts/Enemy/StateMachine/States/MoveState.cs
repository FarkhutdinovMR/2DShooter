using UnityEngine;

[RequireComponent(typeof(Movement))] 
public class MoveState : State
{
    private Movement _movement;

    private void OnDisable()
    {
        _movement.Move(0f);
    }

    private void Start()
    {
        _movement = GetComponent<Movement>();
    }

    private void Update()
    {
        float direction = (Target.transform.position - transform.position).normalized.x;

        _movement.Move(direction);
    }
}