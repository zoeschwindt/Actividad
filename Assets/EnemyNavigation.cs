using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNavigation : MonoBehaviour
{
    // Start is called before the first frame update

    public NavMeshAgent Agent;
    public Transform player;
    public float initialdelay;
    public float interval;
    [SerializeField] private float distancePlayerEnemy;
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        InvokeRepeating("SetDestination", initialdelay, interval);

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
