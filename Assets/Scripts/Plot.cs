using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] private Color hoverColor;

    private GameObject tower;
    private Color startColor;

    private void Start()
    {
        startColor = sr.color;
    }
    private void OnMouseEnter()
    {
        sr.color = hoverColor;
    }
    private void OnMouseExit()
    {
        sr.color = startColor;
    }
    private void OnMouseDown()
    {
        if (tower != null) return;


        Tower towerToBuild = buildManager.main.GetSelectedTower();
        
        if (towerToBuild.cost > LevelManager.main.currency)
        {
            Debug.Log("Você não tem dinheiro");
            return;
        }
        LevelManager.main.SpendCurrency(towerToBuild.cost);
        tower = Instantiate(towerToBuild.preFab, transform.position,Quaternion.identity);
    }
}
