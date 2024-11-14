using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

// Classe EnemySpawner respons�vel por gerar ondas de inimigos com dificuldade escalonada
public class EnemySpawner : MonoBehaviour
{
    #region singleton
    static public EnemySpawner main; // Singleton para facilitar o acesso global � inst�ncia
    #endregion

    // Refer�ncias aos prefabs dos inimigos a serem gerados
    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;

    // Atributos de configura��o da onda de inimigos
    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 8;                  // N�mero base de inimigos por onda
    [SerializeField] private float enemiesPerSecond = 0.5f;        // Taxa base de inimigos gerados por segundo
    [SerializeField] private float timeBeetwenWaves = 5f;          // Tempo entre ondas
    [SerializeField] private float difficultyScalingFactor = 0.75f; // Fator de escalonamento de dificuldade
    [SerializeField] float enemiesPerSecondCap = 10f;              // Limite superior para a taxa de gera��o de inimigos

    // Evento acionado ao destruir um inimigo
    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();

    // Vari�veis de controle do estado das ondas e inimigos
    private int currentWave = 1;                 // N�mero da onda atual
    private float timeSinceLastSpawn;            // Tempo desde o �ltimo inimigo gerado

    public int enemiesAlive;                     // Contador de inimigos vivos na onda atual
    private int enemiesLeftToSpawn;              // Contador de inimigos restantes para gerar na onda atual
    private float eps;                           // Taxa de inimigos por segundo, ajustada para a dificuldade
    private bool isSpawning = false;             // Controle para verificar se uma onda est� em andamento

    public int inimigosVivos;
    public TextMeshProUGUI inimigosVivosT;

    // M�todo Awake, define o singleton e adiciona o listener para destrui��o de inimigos
    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
        main = this;
    }

    // M�todo chamado ao destruir um inimigo, decrementa o contador de inimigos vivos
    public void EnemyDestroyed()
    {
        enemiesAlive--;
        onEnemyDestroy.RemoveListener(EnemyDestroyed); // Remove o listener ap�s a destrui��o do inimigo
    }

    // M�todo Start inicia a primeira onda de inimigos
    private void Start()
    {
        StartCoroutine(StartWave());
        inimigosVivosT.text = "Inimigos Vivos: " + inimigosVivos.ToString();
    }

    // M�todo Update gerencia a gera��o de inimigos enquanto uma onda est� ativa
    private void Update()
    {
        if (!isSpawning) return; // Se n�o est� gerando, sai do m�todo

        timeSinceLastSpawn += Time.deltaTime; // Atualiza o tempo desde o �ltimo spawn

       
        // Condi��o para gerar um novo inimigo baseado no tempo e nos inimigos restantes
        if (timeSinceLastSpawn >= (1f / eps) && enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();            // Gera um novo inimigo
            enemiesLeftToSpawn--;     // Decrementa o contador de inimigos restantes para gerar
            enemiesAlive++;           // Incrementa o contador de inimigos vivos
            timeSinceLastSpawn = 0f;  // Reinicia o contador de tempo para o pr�ximo spawn
        }

        // Verifica se todos os inimigos foram gerados e destru�dos para encerrar a onda
        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            EndWave();
        }
    }

    // M�todo que inicia uma nova onda de inimigos ap�s o intervalo especificado
    private IEnumerator StartWave()
    {
        enemiesAlive = 0;                           // Reinicia o contador de inimigos vivos
        yield return new WaitForSeconds(timeBeetwenWaves); // Aguarda o tempo entre ondas
        isSpawning = true;                          // Marca que a onda est� em andamento
        enemiesLeftToSpawn = EnemiesPerWave();      // Define o n�mero de inimigos para gerar nesta onda
        eps = EnemiesPerSecond();                   // Ajusta a taxa de gera��o de inimigos
    }

    // M�todo EndWave encerra a onda e inicia a pr�xima
    private void EndWave()
    {
        isSpawning = false;          // Marca que a gera��o de inimigos foi interrompida
        timeSinceLastSpawn = 0f;     // Reinicia o contador de tempo para a pr�xima onda
        currentWave++;               // Incrementa o n�mero da onda atual
        StartCoroutine(StartWave()); // Inicia a pr�xima onda
    }

    // M�todo SpawnEnemy seleciona aleatoriamente um prefab e o instancia no ponto inicial
    private void SpawnEnemy()
    {
        int index = Random.Range(0, enemyPrefabs.Length);                  // Seleciona um �ndice aleat�rio para o prefab
        GameObject prefabToSpawn = enemyPrefabs[index];                    // Escolhe o prefab correspondente
        Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity); // Instancia o inimigo
        inimigosVivos++;
        inimigosVivosT.text = "Inimigos Vivos: " + inimigosVivos.ToString();
    }

    // M�todo para calcular o n�mero de inimigos na onda atual, com escalonamento de dificuldade
    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
    }

    // M�todo para calcular a taxa de inimigos por segundo, com limite de velocidade m�xima
    private float EnemiesPerSecond()
    {
        return Mathf.Clamp(enemiesPerSecond * Mathf.Pow(currentWave, difficultyScalingFactor), 0f, enemiesPerSecondCap);
    }

    // M�todo p�blico para for�ar o in�cio de uma nova onda
    public void For�eWave()
    {
        StartWave();
    }
}