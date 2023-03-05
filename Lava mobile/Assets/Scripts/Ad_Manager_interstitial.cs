using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GoogleMobileAds.Api;
using GoogleMobileAds;
using UnityEngine.Events;
using GoogleMobileAds.Common;
using UnityEngine.UI;

public class Ad_Manager_interstitial : MonoBehaviour
{
    //string appid = "ca-app-pub-9603841211455109~9623914786";
    string adUnitId = "ca-app-pub-9603841211455109/1615886032";

    private InterstitialAd Interstitial;
    public bool shown;



    void Start()
    {
        //MobileAds.Initialize(appid);
        //MobileAds.Initialize(initStatus => { });
        shown = false;
    }

    public void RequestInterstitial()
    {
        // Initialize an InterstitialAd.
        this.Interstitial = new InterstitialAd(adUnitId);
        // Called when an ad request has successfully loaded.
        this.Interstitial.OnAdLoaded += HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.Interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;
        // Called when an ad is shown.
        this.Interstitial.OnAdOpening += HandleOnAdOpened;
        // Called when the ad is closed.
        this.Interstitial.OnAdClosed += HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
                    //  this.Interstitial.OnAdLeavingApplication += HandleOnAdLeavingApplication;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.Interstitial.LoadAd(request);

        ShowInterstitialoAd();


    }

    public void ShowInterstitialoAd()
    {
        if (this.Interstitial.IsLoaded())
        {
            this.Interstitial.Show();
        }
    }


    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: ");
    }

    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdOpened event received");
    }

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdClosed event received");
    }

    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLeavingApplication event received");
    }


}