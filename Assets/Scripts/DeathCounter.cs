using UnityEngine;

public class DeathCounter : MonoBehaviour
{
    [SerializeField] private GameUI _gameUI;
    private int _deathCounter = 0;

    public void OnTankDeath()
    {
        _gameUI.SetTextNumberDeaths(++_deathCounter);
    }
}
