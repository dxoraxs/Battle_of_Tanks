using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private float _intervalSpawn;
    [SerializeField] private float _border;
    [SerializeField] private UnityEvent _onTankDead;

    public Vector3 GetRandomPoint() => new Vector3(GetRandomPosition(), 0, GetRandomPosition());

    public void OnDeathTank()
    {
        _onTankDead.Invoke();
    }

    private float GetRandomPosition() => Random.Range(-_border, _border);

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(_intervalSpawn);
        var spawnedObject = Instantiate(_prefab, transform);
        spawnedObject.transform.position = GetRandomPoint();
        spawnedObject.GetComponent<TankMovement>().InitializationTank(GetRandomPoint, OnDeathTank);
        StartCoroutine(Start());
    }
}