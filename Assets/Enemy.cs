using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public int health;
    public TextMeshProUGUI enemyHealthUi;
    GameController gameController;
    public NavMeshAgent Agent;
    public Transform player;
    public float initialdelay;
    public float interval;
    [SerializeField] private float distancePlayerEnemy;

    private void Start()
    {
        enemyHealthUi.text = health.ToString();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        Agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        InvokeRepeating("SetDestination", initialdelay, interval);

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
        else if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("El enemigo tocó al jugador");
            gameController.TimerFinished();
        }

        
    }

    public void SetDestination()
    {
        float distancePlayer = Vector3.Distance(transform.position, player.position);
        Vector3 directionPlayer = (player.position - transform.position).normalized;

        Ray ray = new Ray(transform.position, directionPlayer);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, distancePlayerEnemy))
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                Agent.destination = player.position;
            }
        }


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(gameObject.transform.position, distancePlayerEnemy);

    }

}
