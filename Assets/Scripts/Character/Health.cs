using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Health : MonoBehaviour
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _delayAfterDeath;
    [SerializeField] private UnityEvent<float> _changed;

    private Animator _animator;
    private float _value;
    private int _previousLayer;
    private int _deadLayer = 8;

    public bool IsAlive => _value > 0f;
    public float MaxHealth => _maxHealth;
    public float Value => _value;

    public event UnityAction Dies;
    public event UnityAction<float> Changed
    {
        add => _changed.AddListener(value);
        remove => _changed.RemoveListener(value);
    }

    private const string Hit = "Hit";
    private const string Death = "Death";

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _previousLayer = gameObject.layer;
        _value = _maxHealth;
    }

    private void OnEnable()
    {
        Initialize();
    }

    public void TakeDamage(float value)
    {
        if (IsAlive == false)
            return;

        _value -= value;

        if (_value <= 0)
            ToDeath();
        else
            _animator.SetTrigger(Hit);

        _changed.Invoke(_value);
    }

    public void Kill()
    {
        _value = 0f;
        ToDeath();
        _changed.Invoke(_value);
    }

    private void Initialize()
    {
        gameObject.layer = _previousLayer;
        _value = _maxHealth;
    }

    private void ToDeath()
    {
        gameObject.layer = _deadLayer;
        _animator.SetTrigger(Death);
        Dies?.Invoke();
        StartCoroutine(Disable());
    }

    private IEnumerator Disable()
    {
        yield return new WaitForSeconds(_delayAfterDeath);
        gameObject.SetActive(false);
    }
}