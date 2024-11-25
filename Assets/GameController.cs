using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update

    private int Kills;
    private int totalEnemies;
    [SerializeField] private GameObject panelfinjuego;
    public static bool muerto = false;
    [SerializeField] private TextMeshProUGUI KillsText;
    [SerializeField] private TextMeshProUGUI winText;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        panelfinjuego.SetActive(false);
        winText.enabled = false;
        totalEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        UpdateUI();
        currentTime = startTime;
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
        isTimerFinished = true;
        //Time.timeScale = 0;
        muerto = true;
        Cursor.lockState = CursorLockMode.Confined;
        panelfinjuego.SetActive(true);
        winText.text = "YOU WIN";
        winText.enabled = true;


    }

    // Tiempo inicial en segundos
    [SerializeField] private float startTime = 60f;
    private float currentTime; // Tiempo actual del contador

    // Referencia al componente Text de la UI
    [SerializeField] private TextMeshProUGUI countdownText;
    

    // Variable para saber si el temporizador ha terminado
    private bool isTimerFinished = false;

    

    void Update()
    {
            if (!isTimerFinished)
        {
            // Decrementa el tiempo
            currentTime -= Time.deltaTime;

            // Si el tiempo llega a cero o menos, ejecuta la acción
            if (currentTime <= 0)
            {
                currentTime = 0;
                TimerFinished();
            }

            

            TimeSpan formatedTime = TimeSpan.FromSeconds(currentTime);
            countdownText.text = formatedTime.ToString("mm':'ss");
        }
    }

    // Método para cuando el temporizador llega a cero
    public void TimerFinished()
    {
        //Time.timeScale = 0;
        muerto = true;
        Cursor.lockState = CursorLockMode.Confined;
        isTimerFinished = true;
        Debug.Log("¡El tiempo ha terminado!");
        panelfinjuego.SetActive(true);
        winText.text = "YOU LOSE";
        winText.enabled = true;

    }
    public void MainMenu()
    {
        //Time.timeScale = 1;
        muerto = false;
        SceneManager.LoadScene(0);
        
    }
}

