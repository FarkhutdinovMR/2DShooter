using UnityEngine;
using UnityEngine.Tilemaps;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Player _target;

    private Tilemap _tilemap;
    private float _direction;

    public Player Target => _target;
    public Tilemap Tilemap => _tilemap;
    public float Direction => _direction;

    public void Initialize(Player target, Tilemap tilemap, float direction)
    {
        _target = target;
        _tilemap = tilemap;
        _direction = direction;
    }
}