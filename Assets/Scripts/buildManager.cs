using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildManager : MonoBehaviour
{
    #region singleton
    public static buildManager main;
    #endregion

    [Header("References")]
    [SerializeField] private Tower[] towers;
    
    private int SelectedTower = 0;

    private void Awake()
    {
        main = this;
    }
    public Tower GetSelectedTower()
    {
        return towers[SelectedTower];
    }
    public void SetSelectedTower(int _selectedTower)
    {
        SelectedTower = _selectedTower;
    }
}
