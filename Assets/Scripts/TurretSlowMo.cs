using System.Collections;
using System.Collections.Generic;
using UnityEditor; // Para desenhar gizmos na cena
using UnityEngine;

public class TurretSlowMo : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask enemyMask; // Máscara para identificar inimigos

    [Header("Attributes")]
    [SerializeField] private float targetingRange = 5f; // Alcance da torreta
    [SerializeField] private float aps = 4f; // Ataques por segundo
    [SerializeField] private float freezeTime = 1f; // Tempo que os inimigos ficam congelados

    private float timeUntilFire; // Contador para disparos

    private void Update()
    {
        // Incrementa o contador de tempo até o próximo ataque
        timeUntilFire += Time.deltaTime;

        // Verifica se o tempo até o ataque é maior ou igual ao intervalo definido
        if (timeUntilFire >= aps)
        {
            Debug.Log("congelou");
            FreezeEnemies(); // Congele os inimigos dentro do alcance
            timeUntilFire = 0f; // Reseta o contador
        }
    }

    private void FreezeEnemies()
    {
        // Realiza um círculo para detectar inimigos dentro do alcance
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);

        // Se houver inimigos no alcance
        if (hits.Length > 0)
        {
            // Para cada inimigo encontrado
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];
                EnemyMovement em = hit.transform.GetComponent<EnemyMovement>(); // Obtém o componente EnemyMovement

                if (em != null) // Verifica se o inimigo possui o script EnemyMovement
                {
                    em.UpdateSpeed(0.1f); // Reduz a velocidade do inimigo
                    StartCoroutine(ResetEnemySpeed(em)); // Inicia a corrotina para resetar a velocidade
                }
            }
        }
    }

    private IEnumerator ResetEnemySpeed(EnemyMovement em)
    {
        yield return new WaitForSeconds(freezeTime); // Espera o tempo de congelamento
        em.ResetSpeed(); // Reseta a velocidade do inimigo
    }

    private void OnDrawGizmosSelected()
    {
        // Desenha um gizmo no editor para visualizar o alcance da torreta
        Handles.color = Color.blue;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }
}
