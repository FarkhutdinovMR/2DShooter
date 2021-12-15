using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Movement), typeof(Enemy))]
public class RunOverObstaclesState : State
{
    [SerializeField] private float _distanceToObstacle;
    [SerializeField] private int _holeDepth;

    private Movement _movement;
    private Enemy _enemy;
    private Tilemap _tilemap;
    private float _moveDirection;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _movement = GetComponent<Movement>();
    }

    private void OnEnable()
    {
        _tilemap = _enemy.Tilemap;
        _moveDirection = _enemy.Direction;
    }

    private void Update()
    {
        _movement.Move(_moveDirection);

        CheckObstacles();
    }

    private void CheckObstacles()
    {
        if (_movement.CurrentGrounded == false)
            return;

        if (CheckWall() || CheckHole())
            _movement.TryJump();
    }

    private bool CheckWall()
    {
        Vector3Int groundPosition = Vector3Int.FloorToInt(transform.position + transform.right * _distanceToObstacle);

        return (_tilemap.HasTile(groundPosition));
    }

    private bool CheckHole()
    {
        Vector3Int groundPosition;

        for (int i = 1; i < _holeDepth + 1; i++)
        {
            groundPosition = Vector3Int.FloorToInt(transform.position + Vector3.down * i);
            if (_tilemap.HasTile(groundPosition))
                return false;
        }

        return true;
    }
}