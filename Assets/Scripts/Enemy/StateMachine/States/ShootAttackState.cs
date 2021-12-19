using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Movement))] 
public class ShootAttackState : State
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _delayBeforeShoot;
    [SerializeField] private float _delayAfterShoot;

    private Animator _animator;
    private Movement _movement;
    private Coroutine _coroutine;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _movement = GetComponent<Movement>();
    }

    private void OnEnable()
    {
        _coroutine = StartCoroutine(ToShoot());
    }

    private void OnDisable()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private IEnumerator ToShoot()
    {        
        float direction = (Target.transform.position - transform.position).normalized.x;
        _movement.SetDirection(direction);
        _animator.SetTrigger(AnimatorCharacterController.States.Shoot);
        yield return new WaitForSeconds(_delayBeforeShoot);

        Instantiate(_bullet, _shootPoint.position, transform.rotation);
        yield return new WaitForSeconds(_delayAfterShoot);

        OnCompleted();
    }
}