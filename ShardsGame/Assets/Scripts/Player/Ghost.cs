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
        if (player != null)
        {
            Vector3 targetPosition = player.position;
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }
    
    
}
