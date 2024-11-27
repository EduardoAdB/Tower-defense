using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsShowListener
{
    #region singleton
    public static AdManager main;
    #endregion

    private string gameId = "5735164"; 
    private bool testMode = true;
    [SerializeField] GameObject adButton;
    private float count;
    public bool adWave = false;
    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        Advertisement.Initialize(gameId, testMode);
        Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);
        Advertisement.Banner.Show("Rewarded_Android");
        Advertisement.Banner.Show("Banner_Android");
    }
    
    public void MostrarAD()
    {       
        Advertisement.Show("Rewarded_Android", this);
        Debug.Log("mostrou ad");
        
        //pausar o jogo
    }

    public void WaveAd()
    {
        Advertisement.Show("Interstitial_Android", this);
        Debug.Log("ad");
    }

    

    public void OnInitializationComplete()
    {
       
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Time.timeScale = 0f;
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        if (placementId == "Rewarded_Android")
        {
            Time.timeScale = 1f;
            LevelManager.main.IncreaseCurrency(500);
        }

       if (placementId == "Interstitial_Android")
        {
            Time.timeScale = 1f;
            EnemySpawner.main.StartWave();
        } 

        
    }
}







