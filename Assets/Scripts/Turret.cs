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

    private List<Transform> enemiesInRange = new List<Transform>(); // Lista para armazenar inimigos dentro do alcance

    private void Awake()
    {
        // Define a instância singleton
        main = this;
    }

    private void Update()
    {
        // Atualiza a lista de inimigos dentro do alcance
        UpdateEnemiesInRange();

        // Se não houver alvo, busca um novo alvo
        if (target == null)
        {
            return;
        }

        // Verifica se o alvo ainda está no alcance da torreta
        if (!CheckTargetIsInRange())
        {
            // Se o alvo saiu do alcance, redefine o alvo
            target = null;
            return;
        }

        // Rotaciona a torreta para mirar no alvo
        RotateTowardsTarget();

        // Atualiza o contador de disparo e atira se o intervalo de tempo foi atingido
        timeUntilFire += Time.deltaTime;
        if (timeUntilFire >= bps)
        {
            Debug.Log("Disparou!");
            Shoot();           // Dispara no alvo
            timeUntilFire = 0f; // Reseta o contador de tempo para o próximo disparo
        }
    }

    // Método para atualizar a lista de inimigos dentro do alcance da torreta
    private void UpdateEnemiesInRange()
    {
        // Cria uma área circular para detectar inimigos no alcance
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, Vector2.zero, 0f, enemyMask);

        // Para cada inimigo detectado, se ele não estiver na lista, adiciona-o
        foreach (var hit in hits)
        {
            if (!enemiesInRange.Contains(hit.transform))
            {
                enemiesInRange.Add(hit.transform);
            }
        }

        // Verifica se algum inimigo saiu do alcance ou morreu, e remove da lista
        for (int i = enemiesInRange.Count - 1; i >= 0; i--)
        {
            // Se o inimigo saiu do alcance ou foi destruído, remove da lista
            if (enemiesInRange[i] == null || Vector2.Distance(enemiesInRange[i].position, transform.position) > targetingRange)
            {
                enemiesInRange.RemoveAt(i);
            }
        }

        // Se não houver mais inimigos na lista, define target como null
        if (enemiesInRange.Count == 0)
        {
            target = null;
        }
        else
        {
            // Se houver inimigos, define target como o próximo inimigo na lista
            target = GetLowestHealthEnemy();
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

    // Método para verificar se o alvo está dentro do alcance
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

    // Método para retornar o inimigo com a menor vida da lista
    private Transform GetLowestHealthEnemy()
    {
        Transform lowestHealthEnemy = enemiesInRange[0];
        float lowestHealth = lowestHealthEnemy.GetComponent<EnemyMovement>().hitPoints; // Supondo que o inimigo tenha um script 'Enemy' com hitPoints

        foreach (Transform enemy in enemiesInRange)
        {
            float currentHealth = enemy.GetComponent<EnemyMovement>().hitPoints;
            if (currentHealth < lowestHealth)
            {
                lowestHealthEnemy = enemy;
                lowestHealth = currentHealth;
            }
        }

        return lowestHealthEnemy;
    }
}
