using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    #region singleton
    public static EnemySpawner main;  // Singleton para permitir acesso fácil ao EnemySpawner
    #endregion

    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;  // Array de prefabs dos inimigos para spawnar diferentes tipos de inimigos

    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 8;  // Número base de inimigos por onda
    [SerializeField] private float enemiesPerSecond = 0.5f;  // Taxa base de inimigos gerados por segundo
    [SerializeField] private float timeBeetwenWaves = 5f;  // Tempo entre ondas
    [SerializeField] private float difficultyScalingFactor = 0.75f;  // Fator de escala de dificuldade entre as ondas
    [SerializeField] float enemiesPerSecondCap = 10f;  // Limite máximo para a taxa de geração de inimigos

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();  // Evento para notificar a destruição de inimigos

    private int currentWave = 1;  // Número da onda atual
    private float timeSinceLastSpawn;  // Tempo decorrido desde o último spawn de inimigo

    private int enemiesAlive;  // Contagem de inimigos vivos na onda atual
    private int enemiesLeftToSpawn;  // Contagem de inimigos restantes para spawnar nesta onda
    private float eps;  // Enemies Per Second, ajustada para a dificuldade da onda atual
    private bool isSpawning = false;  // Flag para indicar se a onda atual está em processo de spawn

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);  // Inscreve EnemyDestroyed como ouvinte no evento onEnemyDestroy
        main = this;  // Define o singleton para este objeto
    }

    public void EnemyDestroyed()
    {
        enemiesAlive--;  // Reduz o número de inimigos vivos
        onEnemyDestroy.RemoveListener(EnemyDestroyed);  // Remove o ouvinte para evitar chamadas duplicadas
    }

    private void Start()
    {
        StartCoroutine(StartWave());  // Inicia a primeira onda
    }

    private void Update()
    {
        if (!isSpawning) return;  // Se não estiver spawnando, sai do método

        timeSinceLastSpawn += Time.deltaTime;  // Incrementa o tempo desde o último spawn

        // Verifica se o tempo para o próximo spawn foi atingido e se ainda restam inimigos a spawnar
        if (timeSinceLastSpawn >= (1f / eps) && enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();  // Spawna um novo inimigo
            enemiesLeftToSpawn--;  // Reduz a contagem de inimigos restantes para spawnar
            enemiesAlive++;  // Aumenta a contagem de inimigos vivos
            timeSinceLastSpawn = 0f;  // Reseta o tempo para o próximo spawn
        }

        // Verifica se todos os inimigos foram derrotados e não há mais inimigos para spawnar nesta onda
        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            EndWave();  // Encerra a onda
        }
    }

    private IEnumerator StartWave()
    {
        enemiesAlive = 0;  // Reseta o contador de inimigos vivos
        yield return new WaitForSeconds(timeBeetwenWaves);  // Espera o tempo entre ondas
        isSpawning = true;  // Ativa o spawn de inimigos
        enemiesLeftToSpawn = EnemiesPerWave();  // Calcula a quantidade de inimigos desta onda
        eps = EnemiesPerSecond();  // Ajusta a taxa de spawn para a dificuldade da onda
    }

    private void EndWave()
    {
        isSpawning = false;  // Para o spawn de inimigos
        timeSinceLastSpawn = 0f;  // Reseta o tempo desde o último spawn
        currentWave++;  // Incrementa o número da onda
        StartCoroutine(StartWave());  // Inicia a próxima onda
    }

    private void SpawnEnemy()
    {
        int index = Random.Range(0, enemyPrefabs.Length);  // Seleciona um inimigo aleatoriamente
        GameObject prefabToSpawn = enemyPrefabs[index];  // Escolhe o prefab para spawnar
        Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);  // Instancia o inimigo no ponto de partida
    }

    private int EnemiesPerWave()
    {
        // Calcula o número de inimigos para a onda atual com base na escala de dificuldade
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
    }

    private float EnemiesPerSecond()
    {
        // Calcula a taxa de spawn para a onda atual e limita com o cap definido
        return Mathf.Clamp(enemiesPerSecond * Mathf.Pow(currentWave, difficultyScalingFactor), 0f, enemiesPerSecondCap);
    }

    public void ForçeWave()
    {
        StartWave();  // Método para forçar o início de uma nova onda
    }
}
