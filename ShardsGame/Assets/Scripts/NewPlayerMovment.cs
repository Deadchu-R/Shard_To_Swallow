using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricCharacterMovement : MonoBehaviour
{
    public float horizontalMoveSpeed = 5f;
    public float verticalMoveSpeed = 3f;

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 inputDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;

        if (inputDirection != Vector3.zero)
        {
            if (Mathf.Abs(inputDirection.x) > Mathf.Abs(inputDirection.z))
            {
                inputDirection.z = 0f;
            }
            else
            {
                inputDirection.x = 0f;
            }

            Vector3 moveDirection = Camera.main.transform.TransformDirection(inputDirection);
            moveDirection.y = 0; // Ensure the character stays on the ground (assuming your game is ground-based).

            float speed = inputDirection.x != 0 ? horizontalMoveSpeed : verticalMoveSpeed;

            transform.Translate(moveDirection * speed * Time.deltaTime);
        }
    }
}
