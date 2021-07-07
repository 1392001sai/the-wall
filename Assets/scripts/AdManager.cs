using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using GoogleMobileAds.Api;

[Obsolete]
public class AdManager : MonoBehaviour
{
    String AppId = "ca-app-pub-7667309402530740~6529685182";
    String BannerId = "ca-app-pub-3940256099942544/6300978111";
    String InterstitialId = "ca-app-pub-7667309402530740/5536716450";
    BannerView bannerView;
    InterstitialAd interstitial;
    static AdManager instance;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            MobileAds.Initialize(AppId);
            RequestInterstitial();
            //RequestBanner();
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
    

    public void RequestInterstitial()
    {

        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(InterstitialId);
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
        //ShowInterstitalAd();
    }

    public void ShowInterstitalAd()
    {

        
        if (this.interstitial.IsLoaded())
        {
            this.interstitial.Show();
            RequestInterstitial();
        }
    }

    public void RequestBanner()
    {
        bannerView = new BannerView(BannerId, AdSize.Banner, AdPosition.Bottom);
        //ShowBannerAd();
        /*// Called when an ad request has successfully loaded.
        this.bannerView.OnAdLoaded += this.HandleOnAdLoaded;
        // Called when an ad request failed to load.
        this.bannerView.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;
        // Called when an ad is clicked.
        this.bannerView.OnAdOpening += this.HandleOnAdOpened;
        // Called when the user returned from the app after an ad click.
        this.bannerView.OnAdClosed += this.HandleOnAdClosed;
        // Called when the ad click caused the user to leave the application.
        this.bannerView.OnAdLeavingApplication += this.HandleOnAdLeavingApplication;
        */

    }

    public void ShowBannerAd()
    {
        AdRequest request = new AdRequest.Builder().Build();
        bannerView.LoadAd(request);
        
    }


    /*public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        MonoBehaviour.print("HandleAdLoaded event received");
    }

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
                            + args.Message);
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
    }*/


}
