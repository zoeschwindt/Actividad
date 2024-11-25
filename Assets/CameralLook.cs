using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameralLook : MonoBehaviour
{
    Vector2 smoothV;

    public Vector2 mouseInput;

    public float sensibility = 5f;
    public float smooth = 2f;

    public GameObject player;

    private float rotationX = 0f;
    private float rotationY = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        player = this.transform.parent.gameObject;
    }

   
    void Update()
    {
        if (GameController.muerto == false)
        {

        
            mouseInput = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        mouseInput = Vector2.Scale(mouseInput, new Vector2(sensibility * smooth, sensibility * smooth));

        smoothV.x = Mathf.Lerp(smoothV.x, mouseInput.x, 1f / smooth);
        smoothV.y = Mathf.Lerp(smoothV.y, mouseInput.y, 1f / smooth);

        rotationX -= smoothV.y;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        rotationY += smoothV.x;

        transform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);

        player.transform.localRotation = Quaternion.Euler(0f, rotationY, 0f);


             }
    }
}
