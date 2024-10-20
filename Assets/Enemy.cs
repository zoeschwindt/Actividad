using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Enemy : MonoBehaviour
{
    public int health;
    public TextMeshProUGUI enemyHealthUi;
    GameController gameController;

    private void Start()
    {
        enemyHealthUi.text = health.ToString();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            int receivedDamage = collision.gameObject.GetComponent<Bullet>().bulletDamage;

            Destroy(collision.gameObject);

            health -= receivedDamage;

            enemyHealthUi.text = health.ToString();

            if (health <= 0)
            {
                Debug.Log("Muere");

                gameController.AddKill();
                Destroy(gameObject);
            }

        }

        
    }

    

    

}
