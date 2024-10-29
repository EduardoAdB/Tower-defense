using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Classe Bullet: gerencia o comportamento do projétil, incluindo movimento, dano e destruição
public class Bullet : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb; // Referência ao Rigidbody2D para controlar a física da bala

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 5f; // Velocidade da bala
    [SerializeField] private int bulletDamage = 1; // Dano que a bala causa ao atingir um inimigo

    private Transform target; // Alvo da bala

    // Define o alvo da bala
    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    // Atualiza a direção e velocidade da bala a cada quadro fixo
    private void FixedUpdate()
    {
        if (!target) return; // Se não há alvo, sai da função
        transform.up = (target.position - transform.position); // Direciona a bala para o alvo

        rb.velocity = transform.up * bulletSpeed; // Move a bala na direção do alvo com a velocidade definida
    }

    // Detecta colisões com inimigos para aplicar dano
    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Tenta obter o componente EnemyMovement do objeto colidido
        EnemyMovement healthComponent = collider.gameObject.GetComponent<EnemyMovement>();

        // Se o objeto colidido é um inimigo, aplica dano
        if (healthComponent != null)
        {
            healthComponent.TakeDamage(bulletDamage); // Aplica o dano ao inimigo
            Debug.Log("Aplicou dano e destruiu a bala");
            Destroy(gameObject); // Destroi a bala após aplicar o dano
        }
    }

    // Verifica se a bala saiu da área visível da câmera
    private void Update()
    {
        BalaPerdida();
    }

    private void BalaPerdida()
    {
        // Converte a posição da bala para coordenadas da viewport
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);

        // Se a bala está fora da área visível, ela é destruída
        if (viewportPosition.x < 0 || viewportPosition.x > 1 ||
            viewportPosition.y < 0 || viewportPosition.y > 1)
        {
            Destroy(gameObject);
        }
    }
}
