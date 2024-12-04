using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectSpawner : MonoBehaviour
{
    public GameObject objectPrefab;
    public float raycastDistance = 100f;
    public float bulletFoce;

    public GameObject shootingEffect;
    public Transform effectPoint;
    public Camera mainCamera;
    public Transform weaponTransform;
    public RectTransform crosshairUI;
    public Canvas canvas;
    public LayerMask layerMask;




    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }



    // Update is called once per frame
    void Update()
    {
        PositionCrosshair();
        if (GameController.muerto == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                GameObject bulletClone = Instantiate(objectPrefab, transform.position, transform.rotation);

                Rigidbody bulletRigidBody = bulletClone.GetComponent<Rigidbody>();

                bulletRigidBody.velocity = transform.up * bulletFoce;

                Destroy(bulletClone, 1f);

                if (shootingEffect != null && effectPoint != null)
                {
                    Instantiate(shootingEffect, effectPoint);
                }

            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }

    }
    void PositionCrosshair()
    {
        Ray ray = new Ray(weaponTransform.position, weaponTransform.up);
        Vector3 targetPoint;

        if (Physics.Raycast(ray, out RaycastHit hit, raycastDistance, layerMask))
        {
            // Si el raycast golpea algo, usa ese punto
            targetPoint = hit.point;
        }
        else
        {
            // Si no golpea nada, calcula un punto en la distancia m�xima
            targetPoint = ray.GetPoint(raycastDistance);
        }

        // Convierte el punto del mundo a coordenadas de pantalla
        Vector3 screenPoint = mainCamera.WorldToScreenPoint(targetPoint);

        // Actualiza la posici�n de la ret�cula en la UI
        crosshairUI.position = screenPoint;

        // Opcional: desactiva la ret�cula si el objetivo est� fuera de la vista
        crosshairUI.gameObject.SetActive(screenPoint.z > 0);

    }

}

