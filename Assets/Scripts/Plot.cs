using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;  // Refer�ncia ao SpriteRenderer deste plot, usado para alterar a cor
    [SerializeField] private Color hoverColor;  // Cor ao passar o mouse sobre o plot

    private GameObject tower;  // Refer�ncia � torre constru�da neste plot
    private Color startColor;  // Cor inicial do SpriteRenderer

    private void Start()
    {
        startColor = sr.color;  // Armazena a cor inicial do plot para restaurar ap�s o hover
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
        if (tower != null) return;  // Se uma torre j� foi constru�da neste plot, sai do m�todo

        Tower towerToBuild = buildManager.main.GetSelectedTower();  // Obt�m a torre selecionada a partir do buildManager
        
        if (towerToBuild.cost > LevelManager.main.currency)  // Verifica se o jogador tem moeda suficiente
        {
            Debug.Log("Voc� n�o tem dinheiro");  // Log de erro caso o jogador n�o tenha moeda suficiente
            return;
        }

        LevelManager.main.SpendCurrency(towerToBuild.cost);  // Deduz o custo da torre do total de moeda
        tower = Instantiate(towerToBuild.preFab, transform.position, Quaternion.identity);  // Constr�i a torre no plot
    }
}
