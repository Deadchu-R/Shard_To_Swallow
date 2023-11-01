using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    public Transform player;
    public GameObject projectilePrefab;
    public float shootingCooldown = 2.0f;
    public float projectileSpeed = 10.0f;
    public float detectionRange;

    private float lastShootTime;

    void Start()
    {
        lastShootTime = Time.time;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) < detectionRange)
        {
            if (Time.time - lastShootTime >= shootingCooldown)
            {
                Shoot();
                lastShootTime = Time.time;
            }
        }
    }

    void Shoot()
    {
        GameObject newProjectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Vector3 shootDirection = (player.position - transform.position).normalized;
        Rigidbody projectileRigidbody = newProjectile.GetComponent<Rigidbody>();
        projectileRigidbody.velocity = shootDirection * projectileSpeed;

    }
}
