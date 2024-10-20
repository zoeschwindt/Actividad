using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectPrefab;

    public float bulletFoce;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
          GameObject bulletClone = Instantiate(objectPrefab, transform.position, transform.rotation);

            Rigidbody bulletRigidBody = bulletClone.GetComponent<Rigidbody>();

            bulletRigidBody.velocity = transform.up * bulletFoce;

            Destroy(bulletClone, 2f);
            
        }

        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            Cursor.lockState = CursorLockMode.None;
        }
        
    }
}
