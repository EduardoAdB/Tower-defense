using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Gerenciador de construção de torres, controlando qual torre está selecionada para construção
public class buildManager : MonoBehaviour
{
    #region singleton
    public static buildManager main; // Singleton para fácil acesso ao buildManager
    #endregion

    [Header("References")]
    [SerializeField] private Tower[] towers; // Array de torres disponíveis para construção

    private int SelectedTower = 0; // Índice da torre selecionada no momento

    // Configura o singleton no início do jogo
    private void Awake()
    {
        main = this;
    }

    // Retorna a torre atualmente selecionada
    public Tower GetSelectedTower()
    {
        return towers[SelectedTower];
    }

    // Define a torre selecionada com base no índice fornecido
    public void SetSelectedTower(int _selectedTower)
    {
        SelectedTower = _selectedTower;
    }
}
