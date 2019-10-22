using UnityEngine;

public class RotationObject
{
    public Quaternion GetRotationToTarget(Vector3 _target, Transform transform, float speed, Vector3 offset = new Vector3())
    {
        Vector3 direction = _target + offset - transform.position;
        Quaternion rotation = Quaternion.LookRotation(direction);
        return Quaternion.Lerp(transform.rotation, rotation, speed * Time.deltaTime);
    }
}