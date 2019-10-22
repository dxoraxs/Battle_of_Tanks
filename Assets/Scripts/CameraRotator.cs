using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotator : MonoBehaviour
{
    [SerializeField] private Transform _transformRotate;
    [SerializeField] private float _upBorder;
    [SerializeField] private float _downBorder;
    [Range(0.1f, 5f)]
    [SerializeField] private float _speedRotation;
    private Vector3 _nexrPositionCamera;
    private Vector3 _startPosition;
    private float _maxYPosition;
    private float _minYPosition;

    private void Start()
    {
        _startPosition = _nexrPositionCamera = transform.position;

        _maxYPosition = Mathf.Cos(_downBorder) * _startPosition.z;
        _minYPosition = Mathf.Cos(_upBorder) * _startPosition.z; 
    }

    private void FixedUpdate()
    {
        _nexrPositionCamera = GetAxisY(Input.GetAxis("Mouse X"), transform.up);
        float yAxis = -Input.GetAxis("Mouse Y");
        if (transform.position.y < _maxYPosition && transform.position.y > _minYPosition)
        {
            _nexrPositionCamera = GetAxisY(yAxis, transform.right);
        }
        else
        {
            if (yAxis < 0 && transform.position.y > _maxYPosition)
            {
                _nexrPositionCamera = GetAxisY(yAxis, transform.right);
            }
            else if (yAxis > 0 && transform.position.y < _minYPosition)
            {
                _nexrPositionCamera = GetAxisY(yAxis, transform.right);
            }
        }
        transform.position = Vector3.Slerp(transform.position, _nexrPositionCamera, 0.2f);
        transform.LookAt(_transformRotate);
    }

    private Vector3 GetAxisY(float yAxis, Vector3 axis)
    {
        return Quaternion.AngleAxis(yAxis * _speedRotation, axis) * _nexrPositionCamera;
    }
}
