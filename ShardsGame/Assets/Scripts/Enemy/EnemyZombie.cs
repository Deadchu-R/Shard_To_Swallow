using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyZombie : MonoBehaviour
{
    public float detectionRadius = 10f;
    public float movementSpeed = 5f;
    public Transform player;
    public Sprite movingSprite;
    private bool isChasing = false;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!isChasing)
        {
            if (Vector3.Distance(transform.position, player.position) <= detectionRadius)
            {
                isChasing = true;
                spriteRenderer.sprite = movingSprite;
            }
        }
        else
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * movementSpeed * Time.deltaTime);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
