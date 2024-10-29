using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager main;  // Singleton para permitir acesso global a esta instância de LevelManager

    public Transform[] path;  // Array de pontos que define o caminho que os inimigos devem seguir
    public Transform startPoint;  // Ponto de início dos inimigos no nível

    public int currency;  // Moeda disponível para o jogador

    private void Awake()
    {
        main = this;  // Define esta instância como a principal do LevelManager
    }

    private void Start()
    {
        currency = 100;  // Inicializa a moeda com 100 unidades
    }

    public void IncreaseCurrency(int amount)
    {
        currency += amount;  // Aumenta a quantidade de moeda do jogador em uma quantidade específica
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
            Debug.Log("Você não tem dinheiro");  // Log para indicar que o jogador não tem moeda suficiente
            return false;  // Retorna falso para indicar que a compra falhou
        }
    }
}
