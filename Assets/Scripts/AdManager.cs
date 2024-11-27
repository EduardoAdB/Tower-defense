using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsShowListener
{
    private string gameId = "5735164"; 
    private bool testMode = true;


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
        Time.timeScale = 0f;
        //pausar o jogo
    }

    public void WaveAd()
    {

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
        Time.timeScale = 1f;


    }
}







