using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Movement))]
public class Input : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Weapon _weapon;

    private Movement _movement;
    private InputAction _moveAction;

    private const string Move = "Move";

    private void Start()
    {
        _movement = GetComponent<Movement>();
        _moveAction = _playerInput.actions[Move];
    }

    private void Update()
    {
        _movement.Move(_moveAction.ReadValue<Vector2>().x);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started)
            _movement.TryJump();
    }
    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.started)
            _weapon.ToShoot();
    }
}