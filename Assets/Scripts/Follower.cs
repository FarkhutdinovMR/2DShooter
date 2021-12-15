using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _offset;

    private void Update()
    {
        Vector3 position = new Vector3(Mathf.Max(_target.position.x, transform.position.x), 0f, 0f);
        transform.position = position + _offset;
    }
}