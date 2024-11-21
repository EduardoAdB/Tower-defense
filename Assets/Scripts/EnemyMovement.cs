using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Classe EnemyMovement, responsável pelo comportamento de movimento e vida dos inimigos
public class EnemyMovement : MonoBehaviour
{
    // Referências ao Rigidbody2D para controlar o movimento do inimigo
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    // Atributos de movimento e configuração de velocidade
    [Header("Attributes")]
    [SerializeField] public float moveSpeed = 2f;

    // Atributos relacionados à vida e valor do inimigo
    [Header("LifeAttribute")]
    [SerializeField] public int hitPoints = 2;
    [SerializeField] private int currencyWorth = 50;

    // Variáveis de controle do estado do inimigo
    private bool isDestroyed = false;
    public int metadeDaVida;           // Vida do inimigo quando chega à metade
    public bool habilidadeAtiva = false; // Controla se uma habilidade especial está ativa

    // Variáveis relacionadas ao caminho e movimento do inimigo
    private Transform target;            // Próximo ponto de destino no caminho
    private int pathIndex = 0;           // Índice do ponto atual no caminho

    // Guarda a velocidade inicial para que possa ser restaurada posteriormente
    public float baseSpeed;

    // Método Start é chamado no início da execução
    private void Start()
    {
        baseSpeed = moveSpeed;                       // Define a velocidade base
        target = LevelManager.main.path[pathIndex];  // Define o primeiro alvo no caminho
        metadeDaVida = hitPoints / 2;                // Define metade da vida inicial
    }

    // Método Update chamado a cada frame
    private void Update()
    {
        Updt(); // Chama o método Updt, que lida com o avanço no caminho
    }

    // Método FixedUpdate chamado em intervalos fixos, ideal para física
    private void FixedUpdate()
    {
        FxdUpdate(); // Chama FxdUpdate para realizar o movimento do inimigo
    }

    // Método Updt para controlar o movimento entre pontos do caminho
    public void Updt()
    {
        // Verifica se o inimigo chegou perto do alvo atual no caminho
        if (Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            pathIndex++; // Avança para o próximo ponto no caminho

            // Se o índice alcançar o último ponto, o inimigo é destruído
            if (pathIndex == LevelManager.main.path.Length)
            {
                EnemySpawner.onEnemyDestroy.Invoke();   // Notifica a destruição
                Destroy(gameObject);                   // Destroi o GameObject
                EnemySpawner.main.enemiesAlive--;      // Decrementa o contador de inimigos vivos
                EnemySpawner.main.inimigosVivos--;
                return;
            }
            else
            {
                target = LevelManager.main.path[pathIndex]; // Define o próximo ponto do caminho
            }
        }
    }

    // Método FxdUpdate realiza o movimento em direção ao alvo
    public void FxdUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized; // Calcula a direção para o alvo
        rb.velocity = direction * moveSpeed; // Define a velocidade do Rigidbody2D
    }

    // Método para atualizar a velocidade do inimigo
    public void UpdateSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }

    // Método para resetar a velocidade para a baseSpeed inicial
    public void ResetSpeed()
    {
        moveSpeed = baseSpeed;
    }

    // Método para causar dano ao inimigo
    public void TakeDamage(int dmg)
    {
        hitPoints -= dmg; // Reduz os pontos de vida

        // Se os pontos de vida chegarem a zero e o inimigo ainda não estiver destruído
        if (hitPoints <= 0 && !isDestroyed)
        {
            EnemySpawner.main.EnemyDestroyed();                // Notifica a destruição do inimigo
            LevelManager.main.IncreaseCurrency(currencyWorth); // Aumenta a moeda do jogador
            isDestroyed = true;                                // Marca o inimigo como destruído
            Destroy(gameObject);                               // Destroi o GameObject
            EnemySpawner.main.inimigosVivos--;
            EnemySpawner.main.inimigosVivosT.text = "Inimigos Vivos: " + EnemySpawner.main.inimigosVivos.ToString();
        }
    }    
}

public interface IMeiaVida
{
    void MeiaVida();
}