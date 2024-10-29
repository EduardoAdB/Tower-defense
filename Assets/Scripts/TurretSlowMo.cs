using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

// Classe TurretSlowMo que congela inimigos dentro de um alcance espec�fico
public class TurretSlowMo : MonoBehaviour
{
    // Camada de detec��o para identificar objetos como "inimigos"
    [Header("References")]
    [SerializeField] private LayerMask enemyMask;

    // Atributos da torre de congelamento
    [Header("Attributes")]
    [SerializeField] private float targetingRange = 5f; // Alcance da �rea de efeito
    [SerializeField] private float aps = 4f;           // Taxa de disparo (ataques por segundo)
    [SerializeField] private float freezeTime = 1f;    // Tempo de congelamento dos inimigos

    private float timeUntilFire; // Contador para controlar o intervalo de disparo

    private void Update()
    {
        // Incrementa o tempo at� o pr�ximo disparo
        timeUntilFire += Time.deltaTime;

        // Verifica se � hora de disparar com base no intervalo definido (aps)
        if (timeUntilFire >= aps)
        {
            Debug.Log("congelou");  // Mensagem para indicar o efeito de congelamento
            FreezeEnemies();        // Chama o m�todo para congelar inimigos
            timeUntilFire = 0f;     // Reseta o contador de tempo
        }
    }

    // M�todo FreezeEnemies que aplica o efeito de congelamento a todos os inimigos no alcance
    private void FreezeEnemies()
    {
        // Cria uma �rea circular para detectar inimigos dentro do alcance especificado
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);

        // Se houver inimigos no alcance
        if (hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];  // Obt�m cada inimigo detectado

                EnemyMovement em = hit.transform.GetComponent<EnemyMovement>(); // Pega o script EnemyMovement do inimigo
                em.UpdateSpeed(0.1f);        // Reduz a velocidade do inimigo para 0.1

                StartCoroutine(ResetEnemySpeed(em)); // Inicia a rotina para restaurar a velocidade ap�s o tempo de congelamento
            }
        }
    }

    // Coroutine que aguarda o tempo de congelamento e, em seguida, restaura a velocidade do inimigo
    private IEnumerator ResetEnemySpeed(EnemyMovement em)
    {
        yield return new WaitForSeconds(freezeTime); // Espera o tempo de congelamento

        em.ResetSpeed(); // Restaura a velocidade original do inimigo
    }

    // M�todo para desenhar o alcance de efeito no Editor, facilitando ajustes
    /*private void OnDrawGizmosSelected()
    {
        Handles.color = Color.blue; // Define a cor do Gizmo
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange); // Desenha um c�rculo para mostrar o alcance
    }*/
}
