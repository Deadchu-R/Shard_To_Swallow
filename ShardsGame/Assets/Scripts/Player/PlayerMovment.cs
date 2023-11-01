using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourDirectionalMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float kickForce = 10.0f;
    public float sphereCastRadius = 1.0f;
    public float interactionRange = 2f;
    public float grabSpeed = 5f;
    private Vector3 offsetFromPlayer = new Vector3(0f, 0f, 0f);
    public float GrabOffsetFromPlayer;
    private GameObject grabbedObject;

    private void Update()
    {

        if (grabbedObject != null)
        {
            UpdateGrabPosition();
        }

        //Movment
        Walk();

        //Controlls
        
        //Kick
        if (Input.GetKeyDown(KeyCode.K))
        {
            Kick();
        }

        //Grab
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (grabbedObject == null)
            {
                TryGrabObject();
            }
            else
            {
                ReleaseObject();
            }
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

    void TryGrabObject()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, interactionRange);

        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Shard"))
            {
                grabbedObject = collider.gameObject;
                break;
            }
        }
    }

    void ReleaseObject()
    {
        if (grabbedObject != null)
        {
            grabbedObject = null;
        }
    }

    void UpdateGrabPosition()
    {
        if (offsetFromPlayer.x < grabbedObject.transform.position.x && offsetFromPlayer.z < grabbedObject.transform.position.z)
        {
            offsetFromPlayer = new Vector3(GrabOffsetFromPlayer, 0, 0);
        }
        if (offsetFromPlayer.x < grabbedObject.transform.position.x && offsetFromPlayer.z > grabbedObject.transform.position.z)
        {
            offsetFromPlayer = new Vector3(0, 0, GrabOffsetFromPlayer);
        }
        if (offsetFromPlayer.x > grabbedObject.transform.position.x && offsetFromPlayer.z > grabbedObject.transform.position.z)
        {
            offsetFromPlayer = new Vector3(-GrabOffsetFromPlayer, 0, 0);
        }
        if (offsetFromPlayer.x > grabbedObject.transform.position.x && offsetFromPlayer.z < grabbedObject.transform.position.z)
        {
            offsetFromPlayer = new Vector3(0, 0, -GrabOffsetFromPlayer);
        }
        Vector3 targetPosition = transform.position + offsetFromPlayer;
        grabbedObject.transform.position = Vector3.Lerp(grabbedObject.transform.position, targetPosition, grabSpeed * Time.deltaTime);
    }    

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}
