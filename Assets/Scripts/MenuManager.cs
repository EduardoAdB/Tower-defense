using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    #region singleton
    static public MenuManager main;                
    #endregion
    [SerializeField]
    public GameObject menuGame;
    [SerializeField]
    public GameObject menuStart;
    [SerializeField]
    public GameObject mapa;

    private void Start()
    {
        Time.timeScale = 0f;
    }
    
    public void StartGame()
    { 
        mapa.SetActive(true);
        menuGame.SetActive(true);
        menuStart.SetActive(false);

        Time.timeScale = 1f;
    }
    public void PauseGame()
    {
        mapa.SetActive(false);
        menuGame.SetActive(false);
        menuStart.SetActive(true);

        Time.timeScale = 0f;
    }
}
