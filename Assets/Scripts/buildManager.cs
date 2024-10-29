using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildManager : MonoBehaviour
{
    #region singleton
    public static buildManager main;  // Cria uma referência estática para permitir acesso global a esta instância de buildManager
    #endregion

    [Header("References")]
    [SerializeField] private Tower[] towers;  // Array de torres disponíveis para construção

    private int SelectedTower = 0;  // Índice da torre selecionada atualmente

    private void Awake()
    {
        main = this;  // Define esta instância de buildManager como a instância principal (singleton)
    }

    public Tower GetSelectedTower()
    {
        return towers[SelectedTower];  // Retorna a torre atualmente selecionada do array de torres
    }

    public void SetSelectedTower(int _selectedTower)
    {
        SelectedTower = _selectedTower;  // Atualiza o índice da torre selecionada com o valor fornecido
    }
}
