using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildManager : MonoBehaviour
{
    #region singleton
    public static buildManager main;  // Cria uma refer�ncia est�tica para permitir acesso global a esta inst�ncia de buildManager
    #endregion

    [Header("References")]
    [SerializeField] private Tower[] towers;  // Array de torres dispon�veis para constru��o

    private int SelectedTower = 0;  // �ndice da torre selecionada atualmente

    private void Awake()
    {
        main = this;  // Define esta inst�ncia de buildManager como a inst�ncia principal (singleton)
    }

    public Tower GetSelectedTower()
    {
        return towers[SelectedTower];  // Retorna a torre atualmente selecionada do array de torres
    }

    public void SetSelectedTower(int _selectedTower)
    {
        SelectedTower = _selectedTower;  // Atualiza o �ndice da torre selecionada com o valor fornecido
    }
}
