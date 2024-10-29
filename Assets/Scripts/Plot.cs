using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Classe Plot representa o local onde as torres podem ser construídas
public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;   // Componente SpriteRenderer para manipular a cor
    [SerializeField] private Color hoverColor;    // Cor ao passar o mouse sobre o plot

    private GameObject tower;    // Referência à torre construída no plot
    private Color startColor;    // Cor inicial do plot

    // Define a cor inicial do plot
    private void Start()
    {
        startColor = sr.color;
    }

    // Altera a cor do plot para hoverColor quando o mouse entra
    private void OnMouseEnter()
    {
        sr.color = hoverColor;
    }

    // Restaura a cor inicial do plot quando o mouse sai
    private void OnMouseExit()
    {
        sr.color = startColor;
    }

    // Constrói uma torre no plot ao clicar, se houver dinheiro suficiente
    private void OnMouseDown()
    {
        if (tower != null) return; // Evita construir múltiplas torres no mesmo plot

        Tower towerToBuild = buildManager.main.GetSelectedTower(); // Obtém a torre selecionada pelo jogador

        // Verifica se o jogador tem dinheiro suficiente para construir a torre
        if (towerToBuild.cost > LevelManager.main.currency)
        {
            Debug.Log("Você não tem dinheiro");
            return;
        }

        // Deduz o custo da torre e instancia a torre no plot
        LevelManager.main.SpendCurrency(towerToBuild.cost);
        tower = Instantiate(towerToBuild.preFab, transform.position, Quaternion.identity);
    }
}
