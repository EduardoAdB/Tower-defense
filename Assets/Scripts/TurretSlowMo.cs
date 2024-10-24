using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TurretSlowMo : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask enemyMask;

    [Header("Attributes")]
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float aps = 4f; //attack per second:D
    [SerializeField] private float freezeTime = 1f;

    private float timeUntilFire;
    private void Update()
    {  // Atira no alvo, se o tempo de disparo for alcançado
        timeUntilFire += Time.deltaTime;
        if (timeUntilFire >= aps) // Dispara quando o tempo for maior que o intervalo definido
        {
            Debug.Log("congelou");
            FreezeEnemies();
            timeUntilFire = 0f;

        }
    }
    private void FreezeEnemies()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)transform.position, 0f, enemyMask);
        
        if(hits.Length > 0)
        {
            for(int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];

                enemyMovemenet em = hit.transform.GetComponent<enemyMovemenet>();
                em.UpdateSpeed(0.5f);
            
                StartCoroutine(ResetEnemySpeed(em));
            }
        }
    }

    private IEnumerator ResetEnemySpeed(enemyMovemenet em)
    {
        yield return new WaitForSeconds(freezeTime);

        em.ResetSpeed();
    }

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.blue;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }


}
