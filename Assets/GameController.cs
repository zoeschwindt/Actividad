using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update

    private int Kills;
    private int totalEnemies;

    [SerializeField] private TextMeshProUGUI KillsText;
    [SerializeField] private TextMeshProUGUI winText;
    void Start()
    {
        winText.enabled = false;
        totalEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        UpdateUI();
    }

    
    public void AddKill()
    {
        Kills++;
        UpdateUI();

        if(Kills == totalEnemies)
        {
            Win();
        }
    }
    private void UpdateUI()
    {
        KillsText.text = Kills.ToString() + " / " + totalEnemies.ToString();
    }

    private void Win()
    {
        winText.enabled = true;
    }
}
