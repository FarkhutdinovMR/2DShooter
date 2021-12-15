using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private float _reloadDuration;
    [SerializeField] private UnityEvent _shooted;

    private Coroutine _reload;

    private const string Shoot = "Shoot";

    public virtual void ToShoot()
    {
        if (_reload != null)
            return;

        Instantiate(_bullet, _shootPoint.position, transform.rotation);
        _reload = StartCoroutine(Reload());
        _animator.SetTrigger(Shoot);
        _shooted.Invoke();
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(_reloadDuration);
        _reload = null;
    }
}