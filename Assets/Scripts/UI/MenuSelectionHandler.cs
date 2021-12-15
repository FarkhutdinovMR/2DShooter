using UnityEngine;
using UnityEngine.EventSystems;

public class MenuSelectionHandler : MonoBehaviour
{
    [SerializeField] private GameObject _selectedObject;

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(_selectedObject);
    }
}