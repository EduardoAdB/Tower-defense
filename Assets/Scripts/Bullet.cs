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
    [SerializeField] private int bulletDamage = 1;

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

    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Tenta obter o componente 'healt' do objeto colidido
        healt healthComponent = collider.gameObject.GetComponent<healt>();

        // Verifica se o objeto tem o componente 'healt'
        if (healthComponent != null)
        {
            // Se tem, aplica o dano
            healthComponent.TakeDamage(bulletDamage);
            Debug.Log("Aplicou dano e destruiu a bala");
            Destroy(gameObject);
        }               
    }
    private void Update()
    {
        BalaPerdida();
    }
    private void BalaPerdida()
    {
        // Converte a posição do objeto para coordenadas da viewport
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);

        // Verifica se o objeto está fora da área visível da câmera
        if (viewportPosition.x < 0 || viewportPosition.x > 1 ||
            viewportPosition.y < 0 || viewportPosition.y > 1)
        {
            Destroy(gameObject); // Destrói o objeto
        }
    }

}
