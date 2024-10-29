using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] public float moveSpeed = 2f;


    [Header("LifeAttribute")]
    [SerializeField] public int hitPoints = 2;
    [SerializeField] private int currencyWorth = 50;

    private bool isDestroyed = false;
    public int metadeDaVida;
    public bool habilidadeAtiva = false;

    private Transform target;
    private int pathIndex = 0;

    public float baseSpeed;
    
    private void Start()
    {
        baseSpeed = moveSpeed;
        target = LevelManager.main.path[pathIndex];
        metadeDaVida = hitPoints / 2;
    }
    private void Update()
    {
        Updt();
    }

    private void FixedUpdate()
    {
        FxdUpdate();   
    }
    public void Updt()
    {
        if (Vector2.Distance(target.position, transform.position) <= 0.1f)
        {

            pathIndex++;
            if (pathIndex == LevelManager.main.path.Length)
            {
                EnemySpawner.onEnemyDestroy.Invoke();
                Destroy(gameObject);
                EnemySpawner.main.enemiesAlive--;
                //fazer um metodo que tira vida da torre
                return;
            }
            else
            {
                target = LevelManager.main.path[pathIndex];
            }
        }
    }
    public void FxdUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;

        rb.velocity = direction * moveSpeed;
    }

    public void UpdateSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }

    public void ResetSpeed()
    {
        moveSpeed = baseSpeed;
    }

    public void TakeDamage(int dmg)
    {
        hitPoints -= dmg;

        if (hitPoints <= 0 && !isDestroyed)
        {
            EnemySpawner.main.EnemyDestroyed();
            LevelManager.main.IncreaseCurrency(currencyWorth);
            isDestroyed = true;
            Destroy(gameObject);
        }

    }
    public virtual void MeiaVida()
    {

    }
}
