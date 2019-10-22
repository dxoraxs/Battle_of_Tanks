using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRotate : MonoBehaviour
{
    [SerializeField] private float _speedRotation;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private float _rangeVisibility;
    [SerializeField] public LayerMask _maskScanEnemy;
    private Transform _target;
    private RotationObject _rotationObject = new RotationObject();
    private Quaternion _lastRotation;
    private TankMovement _tankMovement;
    private bool _isShooting = true;

    private void Start()
    {
        _tankMovement = GetComponentInParent<TankMovement>();
    }

    private void Update()
    {
        if (_target != null)
        {
            _lastRotation = transform.rotation;
            transform.rotation = _rotationObject.GetRotationToTarget(_target.position, transform, _speedRotation, new Vector3(0, transform.localPosition.y));

            float angleRotate = Quaternion.Angle(transform.rotation, _lastRotation);
            if (angleRotate < 2f)
            {
                if (angleRotate < 0.2f && _isShooting)
                StartCoroutine(Shot());
            }
        }
        else
        {
            _tankMovement.enabled = true;
            SelectedTarget();
            transform.rotation = _rotationObject.GetRotationToTarget(transform.parent.localPosition, transform, _speedRotation, transform.parent.forward + new Vector3(0, transform.localPosition.y));
        }
    }
    
    private IEnumerator Shot()
    {
        _isShooting = false;
        yield return new WaitForSeconds(Random.Range(0.1f, 0.7f));
        for (int i =0; i<10;i++)
        {
            var bullet = Instantiate(_bulletPrefab, _spawnPosition.transform.position + GetRandomBulletSpawn(), transform.rotation);
            Destroy(bullet, 1.5f);
        }
        yield return new WaitForSeconds(1f);
        _isShooting = true;
    }

    private Vector3 GetRandomBulletSpawn()
    {
        Vector2 randomDirection = Random.insideUnitCircle * Random.Range(0f, 1f);
        return randomDirection;
    }

    private void SelectedTarget()
    {
        Collider[] _colliders = Physics.OverlapSphere(transform.position, _rangeVisibility, _maskScanEnemy);
        if (_colliders.Length > 1)
        {
            float _minDistance = float.MaxValue;
            foreach (Collider collider in _colliders)
            {
                float distanceToTank = GetDistanceTwoTransform(collider.transform, transform);
                if (distanceToTank < _minDistance && distanceToTank > 0.1f)
                {
                    _tankMovement.enabled = false;
                    _target = collider.transform;
                    _minDistance = Vector3.Distance(_target.position, transform.position);
                }
            }
        }
    }

    private float GetDistanceTwoTransform(Transform first, Transform second)
    {
        return Vector2.Distance(
                    new Vector2(first.position.x, first.position.z),
                    new Vector2(second.position.x, second.position.z));
    }
}
