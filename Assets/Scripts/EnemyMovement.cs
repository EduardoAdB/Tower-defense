using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Classe EnemyMovement, respons�vel pelo comportamento de movimento e vida dos inimigos
public class EnemyMovement : MonoBehaviour
{
    // Refer�ncias ao Rigidbody2D para controlar o movimento do inimigo
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    // Atributos de movimento e configura��o de velocidade
    [Header("Attributes")]
    [SerializeField] public float moveSpeed = 2f;

    // Atributos relacionados � vida e valor do inimigo
    [Header("LifeAttribute")]
    [SerializeField] public int hitPoints = 2;
    [SerializeField] private int currencyWorth = 50;

    // Vari�veis de controle do estado do inimigo
    private bool isDestroyed = false;
    public int metadeDaVida;           // Vida do inimigo quando chega � metade
    public bool habilidadeAtiva = false; // Controla se uma habilidade especial est� ativa

    // Vari�veis relacionadas ao caminho e movimento do inimigo
    private Transform target;            // Pr�ximo ponto de destino no caminho
    private int pathIndex = 0;           // �ndice do ponto atual no caminho

    // Guarda a velocidade inicial para que possa ser restaurada posteriormente
    public float baseSpeed;

    // M�todo Start � chamado no in�cio da execu��o
    private void Start()
    {
        baseSpeed = moveSpeed;                       // Define a velocidade base
        target = LevelManager.main.path[pathIndex];  // Define o primeiro alvo no caminho
        metadeDaVida = hitPoints / 2;                // Define metade da vida inicial
    }

    // M�todo Update chamado a cada frame
    private void Update()
    {
        Updt(); // Chama o m�todo Updt, que lida com o avan�o no caminho
    }

    // M�todo FixedUpdate chamado em intervalos fixos, ideal para f�sica
    private void FixedUpdate()
    {
        FxdUpdate(); // Chama FxdUpdate para realizar o movimento do inimigo
    }

    // M�todo Updt para controlar o movimento entre pontos do caminho
    public void Updt()
    {
        // Verifica se o inimigo chegou perto do alvo atual no caminho
        if (Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            pathIndex++; // Avan�a para o pr�ximo ponto no caminho

            // Se o �ndice alcan�ar o �ltimo ponto, o inimigo � destru�do
            if (pathIndex == LevelManager.main.path.Length)
            {
                EnemySpawner.onEnemyDestroy.Invoke();   // Notifica a destrui��o
                Destroy(gameObject);                   // Destroi o GameObject
                EnemySpawner.main.enemiesAlive--;      // Decrementa o contador de inimigos vivos
                EnemySpawner.main.inimigosVivos--;
                return;
            }
            else
            {
                target = LevelManager.main.path[pathIndex]; // Define o pr�ximo ponto do caminho
            }
        }
    }

    // M�todo FxdUpdate realiza o movimento em dire��o ao alvo
    public void FxdUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized; // Calcula a dire��o para o alvo
        rb.velocity = direction * moveSpeed; // Define a velocidade do Rigidbody2D
    }

    // M�todo para atualizar a velocidade do inimigo
    public void UpdateSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }

    // M�todo para resetar a velocidade para a baseSpeed inicial
    public void ResetSpeed()
    {
        moveSpeed = baseSpeed;
    }

    // M�todo para causar dano ao inimigo
    public void TakeDamage(int dmg)
    {
        hitPoints -= dmg; // Reduz os pontos de vida

        // Se os pontos de vida chegarem a zero e o inimigo ainda n�o estiver destru�do
        if (hitPoints <= 0 && !isDestroyed)
        {
            EnemySpawner.main.EnemyDestroyed();                // Notifica a destrui��o do inimigo
            LevelManager.main.IncreaseCurrency(currencyWorth); // Aumenta a moeda do jogador
            isDestroyed = true;                                // Marca o inimigo como destru�do
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