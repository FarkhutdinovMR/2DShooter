using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AttackState : State
{
    [SerializeField] private float _damage;
    [SerializeField] private float _distance;
    [SerializeField] private float _delayBeforeTakeDamage;
    [SerializeField] private float _delayAfterAttack;

    private Animator _animator;
    private Coroutine _coroutine;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        StartCoroutine(Attack());
    }

    private void OnDisable()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private IEnumerator Attack()
    {
        _animator.SetTrigger("Attack");
        yield return new WaitForSeconds(_delayBeforeTakeDamage);

        if ((Vector3.Distance(Target.transform.position, transform.position) <= _distance) && 
            (Target.TryGetComponent(out Health health)))
            health.TakeDamage(_damage);

        yield return new WaitForSeconds(_delayAfterAttack);
        OnCompleted();
    }
}