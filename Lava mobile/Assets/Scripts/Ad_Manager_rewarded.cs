using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GoogleMobileAds.Api;
using GoogleMobileAds;
using UnityEngine.Events;
using GoogleMobileAds.Common;
using UnityEngine.UI;

public class Ad_Manager_rewarded : MonoBehaviour
{
    //string appid = "ca-app-pub-3940256099942544/3419835294";
    string adUnitId = "ca-app-pub-9603841211455109/5335639198";

    private RewardedAd video;
    public bool shown;



    void Start()
    {
        //MobileAds.Initialize(appid);
        //MobileAds.Initialize(initStatus => { });
        shown = false;
    }




    public void RequestVideo()
    {
        this.video = new RewardedAd(adUnitId);

        // Called when an ad request has successfully loaded.
        this.video.OnAdLoaded += HandleRewardedAdLoaded;
        // Called when an ad request failed to load.
                  //this.video.OnAdFailedToLoad += HandleRewardedAdFailedToLoad;
        // Called when an ad is shown.
        this.video.OnAdOpening += HandleRewardedAdOpening;
        // Called when an ad request failed to show.
        this.video.OnAdFailedToShow += HandleRewardedAdFailedToShow;
        // Called when the user should be rewarded for interacting with the ad.
        this.video.OnUserEarnedReward += HandleUserEarnedReward;
        // Called when the ad is closed.
        this.video.OnAdClosed += HandleRewardedAdClosed;

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        // this.video.LoadAd(request);
        this.video.LoadAd(request);

        ShowVideoAd();
    }


    void ShowVideoAd()
    {
        if (this.video.IsLoaded())
        {
            this.video.Show();
        }
    }

    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdLoaded event received");
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToLoad event received with message: ");
    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdOpening event received");
    }

    public void HandleRewardedAdFailedToShow(object sender, AdErrorEventArgs args)
    {
        MonoBehaviour.print(
            "HandleRewardedAdFailedToShow event received with message: ");
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleRewardedAdClosed event received");
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        string type = args.Type;
        double amount = args.Amount;
        MonoBehaviour.print(
            "HandleRewardedAdRewarded event received for "
                        + amount.ToString() + " " + type);
    }

}