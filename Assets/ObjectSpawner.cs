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
        Ray ray = new Ray(weaponTransform.position, weaponTransform.forward);
        RaycastHit hit;

        Vector3 targetPoint;

        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.origin + ray.direction * raycastDistance;
        }

        Vector3 screenPoint = mainCamera.WorldToScreenPoint(targetPoint);

        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), screenPoint, mainCamera, out Vector2 canvasPoint);

        crosshairUI.anchoredPosition = canvasPoint;


    }

}

