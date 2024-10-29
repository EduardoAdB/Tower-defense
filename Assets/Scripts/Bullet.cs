using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Classe Bullet: gerencia o comportamento do proj�til, incluindo movimento, dano e destrui��o
public class Bullet : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb; // Refer�ncia ao Rigidbody2D para controlar a f�sica da bala

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 5f; // Velocidade da bala
    [SerializeField] private int bulletDamage = 1; // Dano que a bala causa ao atingir um inimigo

    private Transform target; // Alvo da bala

    // Define o alvo da bala
    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    // Atualiza a dire��o e velocidade da bala a cada quadro fixo
    private void FixedUpdate()
    {
        if (!target) return; // Se n�o h� alvo, sai da fun��o
        transform.up = (target.position - transform.position); // Direciona a bala para o alvo

        rb.velocity = transform.up * bulletSpeed; // Move a bala na dire��o do alvo com a velocidade definida
    }

    // Detecta colis�es com inimigos para aplicar dano
    private void OnTriggerEnter2D(Collider2D collider)
    {
        // Tenta obter o componente EnemyMovement do objeto colidido
        EnemyMovement healthComponent = collider.gameObject.GetComponent<EnemyMovement>();

        // Se o objeto colidido � um inimigo, aplica dano
        if (healthComponent != null)
        {
            healthComponent.TakeDamage(bulletDamage); // Aplica o dano ao inimigo
            Debug.Log("Aplicou dano e destruiu a bala");
            Destroy(gameObject); // Destroi a bala ap�s aplicar o dano
        }
    }

    // Verifica se a bala saiu da �rea vis�vel da c�mera
    private void Update()
    {
        BalaPerdida();
    }

    private void BalaPerdida()
    {
        // Converte a posi��o da bala para coordenadas da viewport
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);

        // Se a bala est� fora da �rea vis�vel, ela � destru�da
        if (viewportPosition.x < 0 || viewportPosition.x > 1 ||
            viewportPosition.y < 0 || viewportPosition.y > 1)
        {
            Destroy(gameObject);
        }
    }
}
