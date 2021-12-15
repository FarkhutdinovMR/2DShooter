using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _groundCheckDistance;
    [SerializeField] private UnityEvent _jumped;
    [SerializeField] private UnityEvent _landed;
    [SerializeField] private ContactFilter2D _filter;

    private Rigidbody2D _rigidbody2D;
    private bool _rightDirection = true;
    private bool _previousGrounded = true;
    private bool _currentGrounded;

    public bool CurrentGrounded => _currentGrounded;

    public event UnityAction Jumped
    {
        add => _jumped.AddListener(value);
        remove => _jumped.RemoveListener(value);
    }
    public event UnityAction Landed
    {
        add => _landed.AddListener(value);
        remove => _landed.RemoveListener(value);
    }
    public event UnityAction StartFalling;
    public event UnityAction<float> Moves;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _currentGrounded = CheckGround();

        if (_previousGrounded && _currentGrounded == false)
        {
            _previousGrounded = false;
            StartFalling?.Invoke();
        }
        else if (_previousGrounded == false && _currentGrounded)
        {
            _previousGrounded = true;
            _landed?.Invoke();
        }
    }

    public void Move(float direction)
    {
        SetDirection(direction);
        transform.Translate(Vector2.right * direction * _speed * Time.deltaTime, Space.World);
        Moves?.Invoke(direction);
    }

    public bool TryJump()
    {
        if (_currentGrounded == false)
            return false;

        _rigidbody2D.velocity = Vector3.zero;
        _rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        _jumped?.Invoke();
        return true;
    }

    public void SetDirection(float direction)
    {
        if ((direction < 0f && _rightDirection) ||
            (direction > 0f && _rightDirection == false))
            Flip();
    }

    public bool CheckGround()
    {
        RaycastHit2D[] raycastHit2D = new RaycastHit2D[1];

        int results = _rigidbody2D.Cast(Vector2.down, _filter, raycastHit2D, _groundCheckDistance);

        return results > 0;
    }

    private void Flip()
    {
        _rightDirection = !_rightDirection;
        transform.eulerAngles = _rightDirection ? new Vector2(0f, 0f) : new Vector2(0f, 180f);
    }
}