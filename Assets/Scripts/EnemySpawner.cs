using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

// Classe EnemySpawner responsável por gerar ondas de inimigos com dificuldade escalonada
public class EnemySpawner : MonoBehaviour
{
    #region singleton
    static public EnemySpawner main; // Singleton para facilitar o acesso global à instância
    #endregion

    // Referências aos prefabs dos inimigos a serem gerados
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;

    // Atributos de configuração da onda de inimigos
    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 8;                  // Número base de inimigos por onda
    [SerializeField] private float enemiesPerSecond = 0.5f;        // Taxa base de inimigos gerados por segundo
    [SerializeField] private float timeBeetwenWaves = 5f;          // Tempo entre ondas
    [SerializeField] private float difficultyScalingFactor = 0.75f; // Fator de escalonamento de dificuldade
    [SerializeField] float enemiesPerSecondCap = 10f;              // Limite superior para a taxa de geração de inimigos

    // Evento acionado ao destruir um inimigo
    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    // Variáveis de controle do estado das ondas e inimigos
    private int currentWave = 1;                 // Número da onda atual
    private float timeSinceLastSpawn;            // Tempo desde o último inimigo gerado

    public int enemiesAlive;                     // Contador de inimigos vivos na onda atual
    private int enemiesLeftToSpawn;              // Contador de inimigos restantes para gerar na onda atual
    private float eps;                           // Taxa de inimigos por segundo, ajustada para a dificuldade
    private bool isSpawning = false;             // Controle para verificar se uma onda está em andamento

    public int inimigosVivos;
    public TextMeshProUGUI inimigosVivosT;

    // Método Awake, define o singleton e adiciona o listener para destruição de inimigos
    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
        main = this;
    }

    // Método chamado ao destruir um inimigo, decrementa o contador de inimigos vivos
    public void EnemyDestroyed()
    {
        enemiesAlive--;
        onEnemyDestroy.RemoveListener(EnemyDestroyed); // Remove o listener após a destruição do inimigo
    }

    // Método Start inicia a primeira onda de inimigos
    private void Start()
    {
        StartCoroutine(StartWave());
        inimigosVivosT.text = "Inimigos Vivos: " + inimigosVivos.ToString();
    }

    // Método Update gerencia a geração de inimigos enquanto uma onda está ativa
    private void Update()
    {
        if (!isSpawning) return; // Se não está gerando, sai do método

        timeSinceLastSpawn += Time.deltaTime; // Atualiza o tempo desde o último spawn

       
        // Condição para gerar um novo inimigo baseado no tempo e nos inimigos restantes
        if (timeSinceLastSpawn >= (1f / eps) && enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();            // Gera um novo inimigo
            enemiesLeftToSpawn--;     // Decrementa o contador de inimigos restantes para gerar
            enemiesAlive++;           // Incrementa o contador de inimigos vivos
            timeSinceLastSpawn = 0f;  // Reinicia o contador de tempo para o próximo spawn
        }

        // Verifica se todos os inimigos foram gerados e destruídos para encerrar a onda
        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            EndWave();
        }
    }

    // Método que inicia uma nova onda de inimigos após o intervalo especificado
    private IEnumerator StartWave()
    {
        enemiesAlive = 0;                           // Reinicia o contador de inimigos vivos
        yield return new WaitForSeconds(timeBeetwenWaves); // Aguarda o tempo entre ondas
        isSpawning = true;                          // Marca que a onda está em andamento
        enemiesLeftToSpawn = EnemiesPerWave();      // Define o número de inimigos para gerar nesta onda
        eps = EnemiesPerSecond();                   // Ajusta a taxa de geração de inimigos
    }

    // Método EndWave encerra a onda e inicia a próxima
    private void EndWave()
    {
        isSpawning = false;          // Marca que a geração de inimigos foi interrompida
        timeSinceLastSpawn = 0f;     // Reinicia o contador de tempo para a próxima onda
        currentWave++;               // Incrementa o número da onda atual
        StartCoroutine(StartWave()); // Inicia a próxima onda
    }

    // Método SpawnEnemy seleciona aleatoriamente um prefab e o instancia no ponto inicial
    private void SpawnEnemy()
    {
        int index = Random.Range(0, enemyPrefabs.Length);                  // Seleciona um índice aleatório para o prefab
        GameObject prefabToSpawn = enemyPrefabs[index];                    // Escolhe o prefab correspondente
        Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity); // Instancia o inimigo
        inimigosVivos++;
        inimigosVivosT.text = "Inimigos Vivos: " + inimigosVivos.ToString();
    }

    // Método para calcular o número de inimigos na onda atual, com escalonamento de dificuldade
    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
    }

    // Método para calcular a taxa de inimigos por segundo, com limite de velocidade máxima
    private float EnemiesPerSecond()
    {
        return Mathf.Clamp(enemiesPerSecond * Mathf.Pow(currentWave, difficultyScalingFactor), 0f, enemiesPerSecondCap);
    }

    // Método público para forçar o início de uma nova onda
    public void ForçeWave()
    {
        StartWave();
    }
}