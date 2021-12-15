using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemySpawner : ObjectPool
{
    [SerializeField] private Player _target;
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private GameObject _enemy;
    [SerializeField] private float _minDelay;
    [SerializeField] private float _maxDelay;
    [SerializeField] private float _disableObject;
    [SerializeField] private ContactFilter2D _filter;

    private Transform[] _points;
    private float _delay;
    private bool _isActive;

    private void Start()
    {
        _points = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
            _points[i] = transform.GetChild(i);

        Initialize(_enemy);
        InvokeRepeating(nameof(DisableObjectAbroadScreen), 0f, _disableObject);
    }

    private void Update()
    {
        if (_isActive == false)
            return;

        if (_delay <= 0f)
        {
            Spawn();
            _delay = Random.Range(_minDelay, _maxDelay);
        }

        _delay -= Time.deltaTime;
    }

    public void Play(bool value)
    {
        _isActive = value;
    }

    private void Spawn()
    {
        if (TryGetObject(out GameObject spawn))
        {
            Transform point = _points[Random.Range(0, _points.Length)];
            Vector2 positionOnGround = GetPositionOnGround(point.position);

            if (positionOnGround == Vector2.zero)
                return;

            spawn.transform.position = positionOnGround + Vector2.up;
            float direction = Mathf.Cos(point.eulerAngles.y * Mathf.Deg2Rad);

            spawn.GetComponent<Enemy>().Initialize(_target, _tilemap, direction);
            spawn.SetActive(true);
        }
    }

    private Vector2 GetPositionOnGround(Vector3 position)
    {
        RaycastHit2D[] raycastHit2D = new RaycastHit2D[1];
        int result = Physics2D.Raycast(position, Vector2.down, _filter, raycastHit2D);

        if (result > 0)
            return raycastHit2D[0].point;

        return Vector2.zero;
    }
}