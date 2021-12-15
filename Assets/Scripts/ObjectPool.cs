using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private int _capacity;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _screenBorderLeft = -0.2f;
    [SerializeField] private float _screenBorderRight = 1.5f;

    private List<GameObject> _pool = new List<GameObject>();

    protected void Initialize(GameObject prefab)
    {
        for (int i = 0; i < _capacity; i++)
        {
            GameObject spawned = Instantiate(prefab, _container);
            spawned.SetActive(false);
            _pool.Add(spawned);
        }
    }

    protected bool TryGetObject(out GameObject result)
    {
        result = _pool.FirstOrDefault(p => p.activeSelf == false);

        return result != null;
    }

    protected void DisableObjectAbroadScreen()
    {
        Vector3 leftDisablePoint = _camera.ViewportToWorldPoint(new Vector2(_screenBorderLeft, 0f));
        Vector3 rightDisablePoint = _camera.ViewportToWorldPoint(new Vector2(_screenBorderRight, 0f));

        foreach (GameObject item in _pool)
        {
            if (item.activeSelf)
            {
                if ((item.transform.position.x < leftDisablePoint.x) ||
                    (item.transform.position.x > rightDisablePoint.x) ||
                    (item.transform.position.y < leftDisablePoint.y))
                    item.SetActive(false);
            }
        }
    }
}