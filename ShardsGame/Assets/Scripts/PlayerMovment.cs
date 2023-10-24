using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    public float moveSpeed = 5f;

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 inputDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;

        if (inputDirection != Vector3.zero)
        {
            Vector3 moveDirection = Camera.main.transform.TransformDirection(inputDirection);
            moveDirection.y = 0;

            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }
    }
}
