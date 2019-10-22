using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private Text _numberDeaths;
        
    public void SetTextNumberDeaths(int number)
    {
        _numberDeaths.text = number.ToString();
    }
}
