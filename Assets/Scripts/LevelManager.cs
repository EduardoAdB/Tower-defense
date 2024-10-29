using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Classe LevelManager: gerencia o n�vel, incluindo o caminho dos inimigos, o ponto de partida e a moeda do jogador
public class LevelManager : MonoBehaviour
{
    public static LevelManager main; // Inst�ncia singleton da classe

    public Transform[] path; // Array de transforma��es representando o caminho dos inimigos
    public Transform startPoint; // Ponto de in�cio para os inimigos

    public int currency; // Quantidade de moeda do jogador

    private void Awake()
    {
        main = this; // Inicializa a inst�ncia singleton
    }

    private void Start()
    {
        currency = 100; // Define a moeda inicial do jogador
    }

    // M�todo para aumentar a quantidade de moeda do jogador
    public void IncreaseCurrency(int amount)
    {
        currency += amount; // Aumenta a moeda pelo valor especificado
    }

    // M�todo para gastar moeda; retorna true se a transa��o for bem-sucedida
    public bool SpendCurrency(int amount)
    {
        // Verifica se h� moeda suficiente para gastar
        if (amount <= currency)
        {
            Debug.Log("Comprou"); // Loga a transa��o
            currency -= amount; // Deduz a quantidade gasta da moeda total
            return true; // Transa��o bem-sucedida
        }
        else
        {
            Debug.Log("Voc� n�o tem dinheiro"); // Loga se a transa��o falhar
            return false; // Transa��o falhou
        }
    }
}
