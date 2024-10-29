using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; // Para desenhar gizmos na cena
using UnityEngine.UIElements; // Para a interface do usu�rio (n�o est� sendo usada aqui)
using Unity.Mathematics; // Para trabalhar com matem�tica (n�o est� sendo usado aqui)

public class Turret : MonoBehaviour
{
    #region singleton
    public static Turret main; // Singleton para acessar a torreta de qualquer lugar
    #endregion

    [Header("References")]
    [SerializeField] private Transform turretRotationPoint; // Ponto de rota��o da torreta
    [SerializeField] private LayerMask enemyMask; // M�scara para identificar inimigos
    [SerializeField] public GameObject bulletPrefab; // Prefab da bala que ser� disparada
    [SerializeField] private Transform firingPoint; // Ponto de onde a bala ser� disparada

    [Header("Attributes")]
    [SerializeField] private float targetingRange = 5f; // Alcance da torreta
    [SerializeField] private float bps = 1f; // Disparos por segundo
    [SerializeField] float rotationSpeed = 10.0f; // Velocidade de rota��o da torreta

    private Transform target; // Alvo atual da torreta
    private float timeUntilFire; // Contador para disparar

    private void Awake()
    {
        main = this; // Inicializa o singleton
    }

    private void Update()
    {
        if (target == null)
        {
            FindTarget(); // Se n�o h� alvo, procura um
            return;
        }

        // Verifica se o alvo ainda est� dentro do alcance
        if (!CheckTargetIsInRange())
        {
            target = null; // Se o alvo saiu do alcance, redefine o alvo
            return;
        }

        // Rotaciona a torreta para o alvo
        RotateTowardsTarget();

        // Atira no alvo, se o tempo de disparo for alcan�ado
        timeUntilFire += Time.deltaTime; // Atualiza o contador de disparo
        if (timeUntilFire >= bps) // Verifica se o tempo de disparo foi alcan�ado
        {
            Debug.Log("disparou");
            Shoot(); // Dispara
            timeUntilFire = 0f; // Reseta o contador
        }
    }

    public void Shoot()
    {
        // Instancia a bala no ponto de disparo
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        Bullet bulletScript = bulletObj.GetComponent<Bullet>(); // Obt�m o script da bala

        if (bulletScript != null)
        {
            bulletScript.SetTarget(target); // Define o alvo da bala
        }
        else
        {
            Debug.LogWarning("O prefab da bala n�o possui o script 'Bullet'.");
        }
    }

    private void FindTarget()
    {
        // Realiza um c�rculo para encontrar inimigos
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);

        if (hits.Length > 0)
        {
            target = hits[0].transform; // Define o primeiro inimigo encontrado como alvo
        }
    }

    private bool CheckTargetIsInRange()
    {
        // Verifica se o alvo est� dentro do alcance
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    private void RotateTowardsTarget()
    {
        // Dire��o para o alvo
        Vector3 directionToTarget = target.position - turretRotationPoint.position;

        // Calcula o �ngulo desejado em rela��o ao eixo Z
        float targetAngle = Mathf.Atan2(-directionToTarget.x, directionToTarget.y) * Mathf.Rad2Deg;

        // Interpola suavemente o �ngulo atual para o �ngulo desejado
        float currentAngle = Mathf.LerpAngle(turretRotationPoint.eulerAngles.z, targetAngle, rotationSpeed * Time.deltaTime);

        // Aplica a rota��o interpolada � torreta
        turretRotationPoint.rotation = Quaternion.Euler(0, 0, currentAngle);
    }

    private void OnDrawGizmosSelected()
    {
        // Desenha um gizmo no editor para visualizar o alcance da torreta
        Handles.color = Color.blue;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }
}
