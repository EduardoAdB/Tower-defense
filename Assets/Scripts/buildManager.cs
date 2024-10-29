using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Gerenciador de constru��o de torres, controlando qual torre est� selecionada para constru��o
public class buildManager : MonoBehaviour
{
    #region singleton
    public static buildManager main; // Singleton para f�cil acesso ao buildManager
    #endregion

    [Header("References")]
    [SerializeField] private Tower[] towers; // Array de torres dispon�veis para constru��o

    private int SelectedTower = 0; // �ndice da torre selecionada no momento

    // Configura o singleton no in�cio do jogo
    private void Awake()
    {
        main = this;
    }

    // Retorna a torre atualmente selecionada
    public Tower GetSelectedTower()
    {
        return towers[SelectedTower];
    }

    // Define a torre selecionada com base no �ndice fornecido
    public void SetSelectedTower(int _selectedTower)
    {
        SelectedTower = _selectedTower;
    }
}
