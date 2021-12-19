using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private Vector3 _offset;
    [SerializeField] private Vector2 _size;
    [SerializeField] private ContactFilter2D _filter;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + _offset, _size);
    }

    public bool Check()
    {
        Collider2D[] collider2D = new Collider2D[1];
        int results = Physics2D.OverlapBox(transform.position + _offset, _size, 0, _filter, collider2D);

        return results > 0;
    }
}