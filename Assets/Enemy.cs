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
    private Animator animator;
    [SerializeField] private GameObject Sangre;
    public float defaultVelocity;
    public float slowVelocity;





    private void Start()
    {
        animator = GetComponent<Animator>();
        enemyHealthUi.text = health.ToString();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

        Agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        InvokeRepeating("SetDestination", initialdelay, interval);

    }
    void Update()
    {
        if (GameController.muerto == false) 
        { 
            animator.SetFloat("Speed",Agent.velocity.magnitude);
        }
        else
        {
            animator.SetFloat("Speed", 0f);
            Agent.isStopped = true;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            animator.SetTrigger("Stunned");

            GameObject sangreclone = Instantiate(Sangre,collision.transform.position, transform.rotation);

            Destroy(sangreclone, 2f);

            Agent.speed = slowVelocity;

            StartCoroutine(VelocidadNormal());





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
            animator.SetTrigger("Grab");
            gameController.TimerFinished();

        }

        
    }

    public void SetDestination()
    {
        if (GameController.muerto == false)
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
                
                //activar animacion de corer



            }
        }
        else
        {
            //activar animacion de idle
        }

         }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(gameObject.transform.position, distancePlayerEnemy);

    }

    private IEnumerator VelocidadNormal()
    {
        yield return new WaitForSeconds(2);

       Agent.speed = defaultVelocity;


    }


}
