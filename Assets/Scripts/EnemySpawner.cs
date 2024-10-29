using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    #region singleton
    public static EnemySpawner main;  // Singleton para permitir acesso f�cil ao EnemySpawner
    #endregion

    [Header("References")]
    [SerializeField] private GameObject[] enemyPrefabs;  // Array de prefabs dos inimigos para spawnar diferentes tipos de inimigos

    [Header("Attributes")]
    [SerializeField] private int baseEnemies = 8;  // N�mero base de inimigos por onda
    [SerializeField] private float enemiesPerSecond = 0.5f;  // Taxa base de inimigos gerados por segundo
    [SerializeField] private float timeBeetwenWaves = 5f;  // Tempo entre ondas
    [SerializeField] private float difficultyScalingFactor = 0.75f;  // Fator de escala de dificuldade entre as ondas
    [SerializeField] float enemiesPerSecondCap = 10f;  // Limite m�ximo para a taxa de gera��o de inimigos

    [Header("Events")]
    public static UnityEvent onEnemyDestroy = new UnityEvent();  // Evento para notificar a destrui��o de inimigos

    private int currentWave = 1;  // N�mero da onda atual
    private float timeSinceLastSpawn;  // Tempo decorrido desde o �ltimo spawn de inimigo

    private int enemiesAlive;  // Contagem de inimigos vivos na onda atual
    private int enemiesLeftToSpawn;  // Contagem de inimigos restantes para spawnar nesta onda
    private float eps;  // Enemies Per Second, ajustada para a dificuldade da onda atual
    private bool isSpawning = false;  // Flag para indicar se a onda atual est� em processo de spawn

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);  // Inscreve EnemyDestroyed como ouvinte no evento onEnemyDestroy
        main = this;  // Define o singleton para este objeto
    }

    public void EnemyDestroyed()
    {
        enemiesAlive--;  // Reduz o n�mero de inimigos vivos
        onEnemyDestroy.RemoveListener(EnemyDestroyed);  // Remove o ouvinte para evitar chamadas duplicadas
    }

    private void Start()
    {
        StartCoroutine(StartWave());  // Inicia a primeira onda
    }

    private void Update()
    {
        if (!isSpawning) return;  // Se n�o estiver spawnando, sai do m�todo

        timeSinceLastSpawn += Time.deltaTime;  // Incrementa o tempo desde o �ltimo spawn

        // Verifica se o tempo para o pr�ximo spawn foi atingido e se ainda restam inimigos a spawnar
        if (timeSinceLastSpawn >= (1f / eps) && enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();  // Spawna um novo inimigo
            enemiesLeftToSpawn--;  // Reduz a contagem de inimigos restantes para spawnar
            enemiesAlive++;  // Aumenta a contagem de inimigos vivos
            timeSinceLastSpawn = 0f;  // Reseta o tempo para o pr�ximo spawn
        }

        // Verifica se todos os inimigos foram derrotados e n�o h� mais inimigos para spawnar nesta onda
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
        timeSinceLastSpawn = 0f;  // Reseta o tempo desde o �ltimo spawn
        currentWave++;  // Incrementa o n�mero da onda
        StartCoroutine(StartWave());  // Inicia a pr�xima onda
    }

    private void SpawnEnemy()
    {
        int index = Random.Range(0, enemyPrefabs.Length);  // Seleciona um inimigo aleatoriamente
        GameObject prefabToSpawn = enemyPrefabs[index];  // Escolhe o prefab para spawnar
        Instantiate(prefabToSpawn, LevelManager.main.startPoint.position, Quaternion.identity);  // Instancia o inimigo no ponto de partida
    }

    private int EnemiesPerWave()
    {
        // Calcula o n�mero de inimigos para a onda atual com base na escala de dificuldade
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
    }

    private float EnemiesPerSecond()
    {
        // Calcula a taxa de spawn para a onda atual e limita com o cap definido
        return Mathf.Clamp(enemiesPerSecond * Mathf.Pow(currentWave, difficultyScalingFactor), 0f, enemiesPerSecondCap);
    }

    public void For�eWave()
    {
        StartWave();  // M�todo para for�ar o in�cio de uma nova onda
    }
}
