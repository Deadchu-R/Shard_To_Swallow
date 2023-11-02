using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public Transform player;
    public float followSpeed = 5.0f;
    public Animator anim;
    public SpriteRenderer sR;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        sR = gameObject.GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            anim.SetBool("GhostIdle", false);
            anim.SetBool("GhostWalkFront", true);
        }
        if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            anim.SetBool("GhostWalkFront", false);
            anim.SetBool("GhostIdle", true);
        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A))
        {
            anim.SetBool("GhostIdle", false);
            anim.SetBool("GhostWalkBack", true);

        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A))
        {
            anim.SetBool("GhostWalkBack", false);
            anim.SetBool("GhostIdle", true);
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S))
        {
            sR.flipX = true;
        }
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.D))
        {
            sR.flipX = false;
        }




        if (player != null)
        {
            Vector3 targetPosition = player.position;
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }
    
}
