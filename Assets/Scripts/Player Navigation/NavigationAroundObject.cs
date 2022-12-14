using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NavigationAroundObject : MonoBehaviour
{
    [Header("ROTATION")]

    [SerializeField] private float _rotationSpeed = 0.5f;
    [SerializeField] private float _direction = -1;
    [SerializeField] private float _inertionTime;

    [SerializeField] private AnimationCurve _inertionCurve;

    private Vector2 _currentSpeed;

    private bool _isFingerOnTheScreen;
    private bool _needRotate;

    private float _timePassed;
    private float _pitch = 0.0f;
    private float _yaw = 0.0f;


    void Start()
    {
        Application.targetFrameRate = 60;
    }
    void Update()
    {
        TouchHandler();
    }

    private void TouchHandler()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                _isFingerOnTheScreen = true;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            _isFingerOnTheScreen = false;
        }

        if (Input.GetMouseButton(0))
        {
            if (_isFingerOnTheScreen)
            {
                RotationHandler();
            }
        }
        else
        {
            InertionRotation();
        }
    }
    private void RotationHandler()
    {
        Vector2 deltaPosition = Input.GetTouch(0).deltaPosition;

        CameraRotate(deltaPosition);

        _needRotate = true;

        _currentSpeed = deltaPosition;

        _timePassed = 0;
    }
    private void InertionRotation()
    {
        if (!_needRotate)
        {
            return;
        }

        if (_timePassed >= _inertionTime)
        {
            _needRotate = false;
            return;
        }

        float normalizedTime = _timePassed / _inertionTime;
        Vector2 currentSpeed = _inertionCurve.Evaluate(normalizedTime) * _currentSpeed;

        CameraRotate(currentSpeed);

        _timePassed += Time.deltaTime;
    }

    private void CameraRotate(Vector2 rotationValue)
    {
        _pitch -= rotationValue.y * _direction * _rotationSpeed * Time.deltaTime;
        _yaw += rotationValue.x * _direction * _rotationSpeed * Time.deltaTime;

        _pitch = Mathf.Clamp(_pitch, -27, 70);

        transform.eulerAngles = new Vector3(_pitch, _yaw, 0.0f);
    }
}
