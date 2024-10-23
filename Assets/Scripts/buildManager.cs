using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buildManager : MonoBehaviour
{
    #region singleton
    public static buildManager main;
    #endregion

    [Header("References")]
    [SerializeField] private GameObject[] towerPrefabs;

    private int SelectedTower = 0;

    private void Awake()
    {
        main = this;
    }
    public GameObject GetSelectedTower()
    {
        return towerPrefabs[SelectedTower];
    }
}
