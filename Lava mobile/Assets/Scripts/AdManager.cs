using UnityEngine;

using GoogleMobileAds.Api;
using GoogleMobileAds;
using System.Collections.Generic;


public class AdManager : MonoBehaviour
{
    ////#if UNITY_ANDROID
    //[SerializeField]
    //private bool StartLoadBanner;
    //[SerializeField]
    //private bool StartLoadInterstitialAd;
    //[SerializeField]
    //private bool StartLoadRewarded;
    //BannerAdGameObject bannerAd;
    //InterstitialAdGameObject interstitialAd;
    //public RewardedAdGameObject rewardedAd;
    

    //void Start()
    //{
    //    bannerAd = MobileAds.Instance.GetAd<BannerAdGameObject>("Bannerr");
    //    interstitialAd = MobileAds.Instance.GetAd<InterstitialAdGameObject>("AdMob Demo Interstitial Ad");
    //    rewardedAd = MobileAds.Instance.GetAd<RewardedAdGameObject>("AdMob Demo Rewarded Ad");

    //    MobileAds.Initialize((initStatus) =>
    //    {
    //        Debug.Log("Initialized MobileAds");
    //    });

    //    if (StartLoadBanner)
    //    {
    //        bannerAd.LoadAd();

    //    }
    //    if (StartLoadInterstitialAd)
    //    {
    //        interstitialAd.LoadAd();

    //    }
    //    if (StartLoadRewarded)
    //    {
    //        rewardedAd.LoadAd();

    //    }

    //}
    //private void Update()
    //{
    //    /*
    //    if (Input.GetKeyDown(KeyCode.S))
    //    {
    //        OnClickShowbannerAd();
    //    } 
    //    if (Input.GetKeyDown(KeyCode.Q))
    //    {
    //        OnClickHidebannerAd();
    //    }

    //    if (Input.GetKeyDown(KeyCode.D))
    //    {
    //        OnClickShowinterstitialAd();
    //    } 
    //    if (Input.GetKeyDown(KeyCode.F))
    //    {
    //        OnClickShowrewardedAd();
    //    }
    //    */
    //}
    ////######################
    ////BANNERS
    //public void OnClickShowbannerAd()
    //{

    //      bannerAd.Show();
    //   // Debug.Log("bannerAd show");

    //}
    //public void OnClickHidebannerAd()
    //{

    //      bannerAd.Hide();
    //   // Debug.Log("bannerAd hide");

    //}
    ////######################

    //public void OnClickShowinterstitialAd()
    //{
    //    //compile prob here
    //    interstitialAd.ShowIfLoaded();
    //}
    //public void OnClickShowrewardedAd()
    //{
    //    if (GameObject.Find("AudioManager"))
    //    {
    //        GameObject.Find("AudioManager").GetComponent<AudioSource>().Play();
    //    }
    //    Debug.Log("clicked rewarded");
    //    //compile prob here
    //    rewardedAd.ShowIfLoaded();
    //}




    ////JUST TO SHOW IN CONSOLE
    //public void LoadedShowbannerAd()
    //{
    //    Debug.Log("bannerAd Was Loaded");
    //} 
    //public void LoadedShowinterstitialAd()
    //{
    //    Debug.Log("Ad Was Loaded interstitialAd");
    //} 
    //public void LoadedShowrewardedAd()
    //{
    //    Debug.Log("Ad Was Loaded rewardedAd");
    //}


    //public void RestartAfterInterstitial()
    //{

    //}
    ////TO RELOAD ADS AFTER SHOWING
    //public void ReloadinterstitialAd()
    //{
    //    Debug.Log("ReLoadeding interstitialAd ...");
    //    interstitialAd.LoadAd();
    //}
    //public void RealoadrewardedAd()
    //{
    //    Debug.Log("ReLoadeding rewardedAd ...");
    //    rewardedAd.LoadAd();

    //}
    //#endif
}