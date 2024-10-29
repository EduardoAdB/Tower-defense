using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using Unity.Mathematics;

// Classe Turret representa uma torre que procura alvos dentro de um alcance, mira e atira neles
public class Turret : MonoBehaviour
{
    // Singleton para permitir acesso único à instância da classe
    #region singleton
    public static Turret main;
    #endregion

    // Referências aos objetos usados na funcionalidade da torreta
    [Header("References")]
    [SerializeField] private Transform turretRotationPoint; // Ponto de rotação da torreta
    [SerializeField] private LayerMask enemyMask;           // Máscara para detectar inimigos
    [SerializeField] public GameObject bulletPrefab;        // Prefab da bala
    [SerializeField] private Transform firingPoint;         // Ponto de disparo das balas

    // Atributos configuráveis da torreta
    [Header("Attributes")]
    [SerializeField] private float targetingRange = 5f; // Alcance da torreta para encontrar alvos
    [SerializeField] private float bps = 1f;            // Taxa de disparo (balas por segundo)
    [SerializeField] float rotationSpeed = 10.0f;      // Velocidade de rotação da torreta

    private Transform target;     // Alvo atual
    private float timeUntilFire;  // Contador para controlar o tempo até o próximo disparo

    private void Awake()
    {
        // Define a instância singleton
        main = this;
    }

    private void Update()
    {
        // Se não houver alvo, busca um novo alvo
        if (target == null)
        {
            FindTarget();
            return;
        }

        // Verifica se o alvo ainda está no alcance da torreta
        if (!CheckTargetIsInRange())
        {
            // Se o alvo saiu do alcance, para de atirar e redefine o alvo
            target = null;
            return;
        }

        // Rotaciona a torreta para mirar no alvo
        RotateTowardsTarget();

        // Atualiza o contador de disparo e atira se o intervalo de tempo foi atingido
        timeUntilFire += Time.deltaTime;
        if (timeUntilFire >= bps)
        {
            Debug.Log("disparou");
            Shoot();           // Dispara no alvo
            timeUntilFire = 0f; // Reseta o contador de tempo para o próximo disparo
        }
    }

    // Método que dispara uma bala na direção do alvo atual
    public void Shoot()
    {
        // Instancia uma bala no ponto de disparo
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();

        if (bulletScript != null)
        {
            bulletScript.SetTarget(target); // Define o alvo da bala
        }
        else
        {
            Debug.LogWarning("O prefab da bala não possui o script 'Bullet'.");
        }
    }

    // Método para encontrar o alvo mais próximo dentro do alcance
    private void FindTarget()
    {
        // Cria uma área circular para detectar inimigos no alcance
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);

        // Se houver inimigos detectados, define o primeiro inimigo como alvo
        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }

    // Verifica se o alvo atual ainda está dentro do alcance
    private bool CheckTargetIsInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    // Método para rotacionar a torreta na direção do alvo
    private void RotateTowardsTarget()
    {
        // Calcula a direção em relação ao alvo
        Vector3 directionToTarget = target.position - turretRotationPoint.position;

        // Calcula o ângulo desejado para mirar no alvo
        float targetAngle = Mathf.Atan2(-directionToTarget.x, directionToTarget.y) * Mathf.Rad2Deg;

        // Interpola suavemente a rotação atual para a rotação desejada, com base na velocidade de rotação
        float currentAngle = Mathf.LerpAngle(turretRotationPoint.eulerAngles.z, targetAngle, rotationSpeed * Time.deltaTime);

        // Aplica a rotação interpolada
        turretRotationPoint.rotation = Quaternion.Euler(0, 0, currentAngle);
    }

    // Método para desenhar o alcance de ataque da torreta no editor
    /*private void OnDrawGizmosSelected()
    {
        Handles.color = Color.blue;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange); // Exibe o alcance da torreta
    }*/
}
