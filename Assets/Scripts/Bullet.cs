using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 5f;

    private Transform target;

    public void SetTarget(Transform _target)
    {
        target = _target;   
    }
    private void FixedUpdate()
    {
        if (!target) return;
        transform.up= (target.position - transform.position);

        rb.velocity = transform.up * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //take health
        Destroy(gameObject);
    }
}
