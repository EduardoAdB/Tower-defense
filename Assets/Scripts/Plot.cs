using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;  // Referência ao SpriteRenderer deste plot, usado para alterar a cor
    [SerializeField] private Color hoverColor;  // Cor ao passar o mouse sobre o plot

    private GameObject tower;  // Referência à torre construída neste plot
    private Color startColor;  // Cor inicial do SpriteRenderer

    private void Start()
    {
        startColor = sr.color;  // Armazena a cor inicial do plot para restaurar após o hover
    }

    private void OnMouseEnter()
    {
        sr.color = hoverColor;  // Altera a cor do plot ao passar o mouse por cima
    }

    private void OnMouseExit()
    {
        sr.color = startColor;  // Restaura a cor original do plot ao remover o mouse de cima
    }

    private void OnMouseDown()
    {
        if (tower != null) return;  // Se uma torre já foi construída neste plot, sai do método

        Tower towerToBuild = buildManager.main.GetSelectedTower();  // Obtém a torre selecionada a partir do buildManager
        
        if (towerToBuild.cost > LevelManager.main.currency)  // Verifica se o jogador tem moeda suficiente
        {
            Debug.Log("Você não tem dinheiro");  // Log de erro caso o jogador não tenha moeda suficiente
            return;
        }

        LevelManager.main.SpendCurrency(towerToBuild.cost);  // Deduz o custo da torre do total de moeda
        tower = Instantiate(towerToBuild.preFab, transform.position, Quaternion.identity);  // Constrói a torre no plot
    }
}
