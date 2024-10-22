using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healt : MonoBehaviour
{
    [Header("Attribute")]
    [SerializeField] private int hitPoints = 2;

    public void TakeDamage(int dmg)
    {
        hitPoints -= dmg;

        if (hitPoints <= 0)
        {
            EnemySpawner.main.EnemyDestroyed();
            Destroy(gameObject);
        }
    }
}
