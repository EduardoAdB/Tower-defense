using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;  // Referência ao Rigidbody2D do inimigo para aplicar movimento

    [Header("Attributes")]
    [SerializeField] public float moveSpeed = 2f;  // Velocidade de movimento do inimigo

    [Header("LifeAttribute")]
    [SerializeField] public int hitPoints = 2;  // Pontos de vida do inimigo
    [SerializeField] private int currencyWorth = 50;  // Quantidade de moeda dada ao jogador ao derrotar este inimigo

    private bool isDestroyed = false;  // Indica se o inimigo já foi destruído, evitando destruição duplicada
    public int metadeDaVida;  // Armazena metade dos pontos de vida para habilidades ou condições de "meia vida"
    public bool habilidadeAtiva = false;  // Status de habilidade especial do inimigo

    private Transform target;  // Ponto atual do caminho que o inimigo está perseguindo
    private int pathIndex = 0;  // Índice do próximo ponto no caminho

    public float baseSpeed;  // Velocidade inicial do inimigo para poder restaurá-la se necessário

    private void Start()
    {
        baseSpeed = moveSpeed;  // Armazena a velocidade base
        target = LevelManager.main.path[pathIndex];  // Define o primeiro alvo do caminho
        metadeDaVida = hitPoints / 2;  // Calcula a metade dos pontos de vida para uso futuro
    }

    private void Update()
    {
        Updt();  // Atualiza o caminho e alvo a cada frame
    }

    private void FixedUpdate()
    {
        FxdUpdate();  // Move o inimigo a cada atualização fixa de física
    }

    public void Updt()
    {
        // Verifica se o inimigo está próximo o suficiente do ponto de destino atual
        if (Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            pathIndex++;  // Avança para o próximo ponto no caminho

            // Se o inimigo chegou ao fim do caminho
            if (pathIndex == LevelManager.main.path.Length)
            {
                EnemySpawner.onEnemyDestroy.Invoke();  // Dispara o evento de destruição do inimigo
                Destroy(gameObject);  // Destroi o objeto inimigo
                // Método para remover vida da torre pode ser implementado aqui
                return;
            }
            else
            {
                target = LevelManager.main.path[pathIndex];  // Define o próximo alvo no caminho
            }
        }
    }

    public void FxdUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;  // Calcula a direção para o próximo ponto

        rb.velocity = direction * moveSpeed;  // Aplica a velocidade ao Rigidbody2D na direção calculada
    }

    public void UpdateSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;  // Atualiza a velocidade do inimigo com o valor fornecido
    }

    public void ResetSpeed()
    {
        moveSpeed = baseSpeed;  // Restaura a velocidade do inimigo ao valor base
    }

    public void TakeDamage(int dmg)
    {
        hitPoints -= dmg;  // Reduz os pontos de vida do inimigo pelo dano recebido

        if (hitPoints <= 0 && !isDestroyed)  // Se o inimigo foi derrotado e ainda não foi destruído
        {
            EnemySpawner.main.EnemyDestroyed();  // Notifica o sistema que o inimigo foi destruído
            LevelManager.main.IncreaseCurrency(currencyWorth);  // Aumenta a moeda do jogador com o valor deste inimigo
            isDestroyed = true;  // Marca o inimigo como destruído para evitar chamadas duplicadas
            Destroy(gameObject);  // Destroi o objeto inimigo
        }
    }

    public virtual void MeiaVida()
    {
        // Método para implementar efeitos especiais ou habilidades quando o inimigo está com metade da vida
    }
}
