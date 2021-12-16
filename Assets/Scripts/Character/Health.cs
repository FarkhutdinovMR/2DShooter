using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Health : MonoBehaviour
{
    [SerializeField] private float _maxValue;
    [SerializeField] private float _delayAfterDeath;
    [SerializeField] private UnityEvent<float> _changed;

    private Animator _animator;
    private float _value;
    private int _previousLayer;
    private int _deadLayer = 8;

    public bool IsAlive => _value > 0f;
    public float MaxHealth => _maxValue;
    public float Value => _value;

    public event UnityAction Died;
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
        _value = _maxValue;
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
            Die();
        else
            _animator.SetTrigger(Hit);

        _changed.Invoke(_value);
    }

    public void Kill()
    {
        _value = 0f;
        Die();
        _changed.Invoke(_value);
    }

    private void Initialize()
    {
        gameObject.layer = _previousLayer;
        _value = _maxValue;
    }

    private void Die()
    {
        gameObject.layer = _deadLayer;
        _animator.SetTrigger(Death);
        Died?.Invoke();
        StartCoroutine(Disable());
    }

    private IEnumerator Disable()
    {
        yield return new WaitForSeconds(_delayAfterDeath);
        gameObject.SetActive(false);
    }
}