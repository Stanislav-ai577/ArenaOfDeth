using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _followTarget;
    private Vector2 _startPosition;
    private float _startZ;

    private Vector2 _cameraMoveSinceStart => (Vector2)_camera.transform.position - _startPosition;
    private float _zDistanceFromTarget => transform.position.z - _followTarget.transform.position.z;
    private float _clippingPlane => (_camera.transform.position.z + (_zDistanceFromTarget > 0 ? _camera.farClipPlane : _camera.nearClipPlane));
    private float _parallaxFactor => Mathf.Abs(_zDistanceFromTarget) / _clippingPlane;
    
    private void Start()
    {
        _startPosition = transform.position;
        _startZ = transform.position.z;
    }

    private void Update()
    {
        Vector2 position = _startPosition + _cameraMoveSinceStart * _parallaxFactor;
        transform.position = new Vector3(position.x, position.y, _startZ);
    }
    
}
