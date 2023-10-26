using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerMovement : MonoBehaviour
{
    public float horizontalMoveSpeed = 5f;
    public float verticalMoveSpeed = 3f;
    public float kickForce = 0.1f;
    public float kickDragRadius = 1.0f;

    private void Update()
    {
        MovePlayer();
        if (Input.GetKeyDown(KeyCode.X))
        {
            Kick();
        }
    }

    void MovePlayer()
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

    void Kick()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, kickDragRadius);

        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                Rigidbody rb = collider.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Vector3 kickDirection = (collider.transform.position - transform.position).normalized;

                    rb.AddForce(kickDirection * kickForce, ForceMode.Impulse);
                }
            }
        }
    }
}
