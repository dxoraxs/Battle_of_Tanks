using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour
{
    public delegate Vector3 GetPosition();
    public delegate void OnDestroyTank();
    [SerializeField] private float _speed;
    [SerializeField] private float _speedRotation;
    private Vector3 _targetPoint;
    private GetPosition _getPosition;
    private OnDestroyTank _onDestroyTank;
    private RotationObject _rotationObject = new RotationObject();
    private Quaternion _lastRotation;

    public void InitializationTank(GetPosition functionGetPosition, OnDestroyTank functionOnDestroyTank)
    {
        _getPosition = functionGetPosition;
        _onDestroyTank = functionOnDestroyTank;
    }

    private void Start()
    {
        if (_getPosition !=null)
        _targetPoint = _getPosition();
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, _targetPoint) < 0.1f)
        {
            Start();
        }
        else
        {
            _lastRotation = transform.rotation;
            transform.rotation = _rotationObject.GetRotationToTarget(_targetPoint, transform, _speedRotation);
            if (Quaternion.Angle(transform.rotation, _lastRotation) < 0.4f)
                transform.position = Vector3.MoveTowards(transform.position, _targetPoint, _speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }

    private void OnDestroy()
    {
        _onDestroyTank?.Invoke();
        _getPosition = null;
        _onDestroyTank = null;
    }
}
