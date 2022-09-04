//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using GoogleMobileAds.Api;
//using GoogleMobileAds.Common;
//using System;

//public class AdmobAds : MonoBehaviour
//{
//    string GameID = "ca-app-pub-8190872782202842~4096171181";

//    //Sample Ads
//    string bannerAdId = "ca-app-pub-8190872782202842/7313167948";
//    string interstitialAdID = "ca-app-pub-8190872782202842/1107045767";

//    public BannerView bannerAd;
//    public InterstitialAd interstitial;
//    public static AdmobAds instance;

//    //private BannerView bannerView;

//    private void Awake()
//    {
//        //if (instance != null && instance != this)
//        //{
//        //    Destroy(gameObject);
//        //    return;
//        //}
//        //instance = this;

//        DontDestroyOnLoad(this);
//        if (instance == null)
//        {
//            instance = this;
//        }
//        else
//        {
//            Destroy(gameObject);
//        }
//    }
//    // Start is called before the first frame update
//    void Start()
//    {
//        //MobileAds.Initialize(GameID);
//        MobileAds.Initialize(InitializationStatus => { });
//        requestInterstitial();
//    }


//    #region banner

//    public void reqBannerAd()
//    {
//        //AdSize adSize = new AdSize(500, 150);
//        //this.bannerAd = new BannerView(bannerAdId, adSize, AdPosition.Bottom);
//        this.bannerAd = new BannerView(bannerAdId, AdSize.SmartBanner, AdPosition.Bottom);
//        //Called when an ad request has successfully loaded
//        this.bannerAd.OnAdLoaded += this.HandleOnAdLoaded;
//        //Called when an ad requeest failed to load
//        this.bannerAd.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;

//        AdRequest request = new AdRequest.Builder().Build();
//        this.bannerAd.LoadAd(request);

//    }

//    public void DestroyBannerAd()
//    {
//        if (bannerAd != null)
//        {
//            bannerAd.Destroy();
//        }
//    }
//    public void hideBanner()
//    {
//        this.bannerAd.Hide();
//    }
//    #endregion

//    #region interstitial

//    public void requestInterstitial()
//    {
//        //this.interstitial = new InterstitialAd(interstitialAdID);

//        //this.interstitial.OnAdLoaded += this.HandleOnAdLoaded;
//        ////Requst failed
//        //this.interstitial.OnAdFailedToLoad += this.HandleOnAdFailedToLoad;
//        ////Called when ad is cleicked
//        //this.interstitial.OnAdOpening += this.HandleOnAdOpened;
//        ////Called when user return from the app after ad click
//        //this.interstitial.OnAdClosed += this.HandleOnAdClosed;
//        ////Called when the ad click caused the user to leave app
//        //this.interstitial.OnAdLeavingApplication += this.HandleOnAdLeavingApplication;

//        //AdRequest request = new AdRequest.Builder().Build();
//        //this.interstitial.LoadAd(request);

//        interstitial = new InterstitialAd(interstitialAdID);
//        AdRequest request = new AdRequest.Builder().Build();
//        interstitial.LoadAd(request);

//        //Events
//        //interstitial.OnAdLoaded += this.HandleOnAdLoaded;
//        //interstitial.OnAdOpening += this.HandleOnAdOpened;
//        //interstitial.OnAdClosed += this.HandleOnAdClosed;
//    }
//    public void ShowInterstitialAd()
//    {
//        //requestInterstitial();
//        if (this.interstitial.IsLoaded())
//        {
//            this.interstitial.Show();
//        }
//        else
//        {
//            Debug.Log("Not loaded");
//            requestInterstitial();
//        }
//    }

//    //public void DestroyInterstitialAd()
//    //{
//    //    if (interstitial != null)
//    //    {
//    //        interstitial.Destroy();
//    //    }
//    //}
//    #endregion

//    #region adDelegates

//    public void HandleOnAdLoaded(object sender, EventArgs args)
//    {
//        MonoBehaviour.print("HandleAdLoaded event received");
//    }

//    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
//    {
//        MonoBehaviour.print("HandleFailedToReceiveAd event received with message: "
//                            );
//    }

//    public void HandleOnAdOpened(object sender, EventArgs args)
//    {
//        MonoBehaviour.print("HandleAdOpened event received");
//    }

//    public void HandleOnAdClosed(object sender, EventArgs args)
//    {
//        MonoBehaviour.print("HandleAdClosed event received");
//        interstitial.OnAdLoaded -= this.HandleOnAdLoaded;
//        interstitial.OnAdOpening -= this.HandleOnAdOpened;
//        interstitial.OnAdClosed -= this.HandleOnAdClosed;
//        requestInterstitial();
//    }


//    public void HandleOnAdLeavingApplication(object sender, EventArgs args)
//    {
//        MonoBehaviour.print("HandleAdLeavingApplication event received");
//    }

//    #endregion

//    //private void OnDestroy()
//    //{
//    //    DestroyBannerAd();
//    //    DestroyInterstitialAd();

//    //    interstitial.OnAdLoaded -= this.HandleOnAdLoaded;
//    //    interstitial.OnAdOpening -= this.HandleOnAdOpened;
//    //    interstitial.OnAdClosed -= this.HandleOnAdClosed;
//    //}
//}
