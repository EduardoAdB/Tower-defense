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
        Advertisement.Banner.SetPosition(BannerPosition.TOP_CENTER);
        Advertisement.Banner.Show("Rewarded_Android");
        Advertisement.Banner.Show("Banner_Android");
    }
}




public interface IUnityAdsInitializationListener
{

}
public interface IUnityAdsShowListener
{

}
