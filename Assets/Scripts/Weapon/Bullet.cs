using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _speed;
    [SerializeField] private float _lifeTime;
    [SerializeField] private float _pushForce;
    [SerializeField] private ParticleSystem _hitParticle;

    private void Start()
    {
        Destroy(gameObject, _lifeTime);
    }

    private void Update()
    {
        transform.Translate(transform.right * _speed * Time.deltaTime, Space.World);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Health health))
            SendDamage(health, collision);

        Destroy(gameObject);
    }

    private void SendDamage(Health target, Collision2D collision)
    {
        target.TakeDamage(_damage);

        Vector2 direction = (collision.transform.position - transform.position).normalized;
        Rigidbody2D _rigidbody2D = collision.gameObject.GetComponent<Rigidbody2D>();
        _rigidbody2D.AddForce(direction *_pushForce, ForceMode2D.Impulse);

        Instantiate(_hitParticle, collision.GetContact(0).point, Quaternion.identity);
    }
}