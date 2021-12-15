using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class Parallax : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private float _speed;

    private RawImage _image;
    private float _positionX;
    private float _previousCameraPositionX;

    private void Start()
    {
        _image = GetComponent<RawImage>();
    }

    private void Update()
    {
        _previousCameraPositionX = _camera.position.x;
    }

    private void LateUpdate()
    {
        float offset = _camera.position.x - _previousCameraPositionX;
        _positionX += offset * _speed;

        if (_positionX > 1f)
            _positionX = 0f;

        _image.uvRect = new Rect(_positionX, _image.uvRect.y, _image.uvRect.width, _image.uvRect.height);
    }
}