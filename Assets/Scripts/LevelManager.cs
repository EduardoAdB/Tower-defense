using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Classe LevelManager: gerencia o nível, incluindo o caminho dos inimigos, o ponto de partida e a moeda do jogador
public class LevelManager : MonoBehaviour
{
    public static LevelManager main; // Instância singleton da classe

    public Transform[] path; // Array de transformações representando o caminho dos inimigos
    public Transform startPoint; // Ponto de início para os inimigos

    public int currency; // Quantidade de moeda do jogador

    private void Awake()
    {
        main = this; // Inicializa a instância singleton
    }

    private void Start()
    {
        currency = 100; // Define a moeda inicial do jogador
    }

    // Método para aumentar a quantidade de moeda do jogador
    public void IncreaseCurrency(int amount)
    {
        currency += amount; // Aumenta a moeda pelo valor especificado
    }

    // Método para gastar moeda; retorna true se a transação for bem-sucedida
    public bool SpendCurrency(int amount)
    {
        // Verifica se há moeda suficiente para gastar
        if (amount <= currency)
        {
            Debug.Log("Comprou"); // Loga a transação
            currency -= amount; // Deduz a quantidade gasta da moeda total
            return true; // Transação bem-sucedida
        }
        else
        {
            Debug.Log("Você não tem dinheiro"); // Loga se a transação falhar
            return false; // Transação falhou
        }
    }
}
