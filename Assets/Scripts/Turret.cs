using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using Unity.Mathematics;

public class Turret : MonoBehaviour
{
    #region singleton
    public static Turret main;
    #endregion

    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    
    [Header("Attributes")]
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float bps = 1f; //bullet per second :D

    private Transform target;
    private float timeUntilFire;

    private void Awake()
    {
        main = this;
    }
    private void Update()
    {
        if (target == null)
        {
            FindTarget();
            return;
        }
        RotateTowardsTarget();

        if (target != null)
        {
            timeUntilFire += Time.deltaTime;
            if(timeUntilFire >= bps)
            {
                Shoot();
                timeUntilFire = 0f;
            }
        }
    
    }

    public void Shoot()
    {
       GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
       Bullet bulletScript = bulletObj.GetComponent<Bullet>();
       bulletScript.SetTarget(target);        
    }
    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);

        if (hits.Length > 0 )
        {
            target = hits[0].transform;
        }
    }

    private bool CheckTargetIsInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    private void RotateTowardsTarget()
    {
        // Velocidade de rota��o (ajust�vel)
        float rotationSpeed = 2.0f;

        // Dire��o para o alvo
        Vector3 directionToTarget = target.position - turretRotationPoint.position;

        // Calcula o �ngulo desejado em rela��o ao eixo Z
        float targetAngle = Mathf.Atan2(-directionToTarget.x, directionToTarget.y) * Mathf.Rad2Deg;

        // Interpola suavemente o �ngulo atual para o �ngulo desejado
        float currentAngle = Mathf.LerpAngle(turretRotationPoint.eulerAngles.z, targetAngle, rotationSpeed * Time.deltaTime);

        // Aplica a rota��o interpolada � torreta
        turretRotationPoint.rotation = Quaternion.Euler(0, 0, currentAngle);
    }

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.blue;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }
}