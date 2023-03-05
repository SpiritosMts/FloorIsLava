using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GoogleMobileAds.Api;
using GoogleMobileAds;
using UnityEngine.Events;
using GoogleMobileAds.Common;
using UnityEngine.UI;

public class Ad_Manager_banner : MonoBehaviour
{
    //string appid = "ca-app-pub-3940256099942544/3419835294";
    string adUnitId = "ca-app-pub-9603841211455109/1807457729";

    private BannerView bannerView;
    public bool shown;





    public void Start()
        {
        //Make it true when you show it 
            shown = false;
           // this.RequestBanner();

    }
    

    
    public void ShowBanneroAd()
    {
      
    }

    public void HideBanner()
    {
        bannerView.Hide();
    }


    public void RequestBanner()
    {

        this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Top);

        // Called when an ad request has successfully loaded.
        this.bannerView.OnAdLoaded += this.HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.bannerView.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;
        // Called when an ad is clicked.
        this.bannerView.OnAdOpening += this.HandleOnAdOpened;
        // Called when the user returned from the app after an ad click.
        this.bannerView.OnAdClosed += this.HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
                   //this.bannerView.OnAdLeavingApplication += this.HandleOnAdLeavingApplication;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();

        // Load the banner with the request.
        this.bannerView.LoadAd(request);

        ShowBanneroAd();
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleOnAdLoaded");
    }
    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleOnAdFailedToLoad");
    }
    public void HandleOnAdOpened(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleOnAdOpened");
    }
    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleOnAdClosed");
    }
    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleOnAdLeavingApplication");
    }



}