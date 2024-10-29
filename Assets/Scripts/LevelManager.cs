using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;  // Singleton para permitir acesso global a esta inst�ncia de LevelManager

    public Transform[] path;  // Array de pontos que define o caminho que os inimigos devem seguir
    public Transform startPoint;  // Ponto de in�cio dos inimigos no n�vel

    public int currency;  // Moeda dispon�vel para o jogador

    private void Awake()
    {
        main = this;  // Define esta inst�ncia como a principal do LevelManager
    }

    private void Start()
    {
        currency = 100;  // Inicializa a moeda com 100 unidades
    }

    public void IncreaseCurrency(int amount)
    {
        currency += amount;  // Aumenta a quantidade de moeda do jogador em uma quantidade espec�fica
    }

    public bool SpendCurrency(int amount)
    {
        if (amount <= currency)  // Verifica se o jogador tem moeda suficiente
        {
            Debug.Log("Comprou");  // Log para indicar que a compra foi realizada
            currency -= amount;  // Deduz o valor da compra da moeda
            return true;  // Retorna verdadeiro para indicar que a compra foi bem-sucedida
        }
        else
        {
            Debug.Log("Voc� n�o tem dinheiro");  // Log para indicar que o jogador n�o tem moeda suficiente
            return false;  // Retorna falso para indicar que a compra falhou
        }
    }
}
