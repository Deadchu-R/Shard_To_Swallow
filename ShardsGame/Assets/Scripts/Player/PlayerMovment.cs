using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourDirectionalMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float moveSpeedOriginal = 5.0f;
    public float kickForce = 10.0f;
    public float sphereCastRadius = 1.0f;

    private void Start()
    {
        moveSpeedOriginal = moveSpeed;
    }
    private void Update()
    {
        //Movment
        Walk();

        //Controlls
        
        //Kick
        if (Input.GetKeyDown(KeyCode.K))
        {
            Kick();
        }
    }

    private void Walk()
    {
        float horizontalInput = Input.GetAxis("Vertical");
        float verticalInput = Input.GetAxis("Horizontal");

        Vector3 inputDirection = new Vector3(horizontalInput, 0, verticalInput * -1).normalized;

        if (inputDirection.magnitude > 0)
        {
            if (Mathf.Abs(inputDirection.x) > Mathf.Abs(inputDirection.z))
            {
                inputDirection.z = 0;
            }
            else
            {
                inputDirection.x = 0;
            }
            
            transform.Translate(inputDirection * moveSpeed * Time.deltaTime);
            
            if(moveSpeed > 0)
             //Play sound
            if(!gameObject.GetComponent<AudioSource>().isPlaying)
             {
            AudioManager audioManager = GameObject.Find("/GameManager").GetComponent<AudioManager>();
            Sound snd = audioManager.sounds[Random.Range(0,audioManager.sounds.Length)];
            audioManager.Play(gameObject.GetComponent<AudioSource>(), snd);
             }

        }
    }

    void Kick()
    {
        Vector3 castDirection = transform.forward;
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, sphereCastRadius, castDirection, 0.0f);

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.CompareTag("Shard"))
            {
                Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddForce(castDirection * kickForce, ForceMode.Impulse);
                }
            }
        }
    }
}
