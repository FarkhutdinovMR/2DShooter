using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _killZoneScreenY;

    private Health _health;
    private float _killZone;

    private const string Menu = "Menu";

    private void Awake()
    {
        _health = GetComponent<Health>();
        _killZone = _camera.ViewportToWorldPoint(new Vector2(0f, _killZoneScreenY)).y;
    }

    private void OnEnable()
    {
        _health.Died += OnDies;
    }

    private void OnDisable()
    {
        _health.Died -= OnDies;
    }

    private void Update()
    {
        if (transform.position.y < _killZone)
            _health.Kill();
    }

    private void OnDies()
    {
        _playerInput.SwitchCurrentActionMap(Menu);
    }
}