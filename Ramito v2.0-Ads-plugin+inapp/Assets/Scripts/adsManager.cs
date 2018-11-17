using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;
using Heyzap;
using UnityEngine.Advertisements;
using System;

public class adsManager : MonoBehaviour {

	//ads manager's instance
	private static adsManager _instance;
	public static adsManager Instance{

		get
		{

			if(_instance==null)
			{

				_instance= GameObject.FindObjectOfType<adsManager>();
				DontDestroyOnLoad(_instance.gameObject);

			}
			return _instance;

		}

	}

	private bool init_once=true;
	private bool showAdmobInsterstitial = false;
	public string InterstitialAdID;
	public string RewardedVideoADID;
	public string BannerAdID;
	public string heyzapAD;
	public string unityid;
	InterstitialAd myInterstitialAd;
	BannerView myBannerView;

	void Awake()
	{




		if(_instance == null)
		{

			_instance = this;
			DontDestroyOnLoad(this);
		}
		else
		{

			if(this != _instance)
				Destroy(this.gameObject);
		}


		if (init_once) {
			init_once = false;

			initAdmobInterstitial ();
			LoadBannerView ();
			InitHeyzap ();
			LoadInterstitialAd ();
			InitUnityadd ();
		}





	}


	void Start(){


	}




	// Admob banner Ads fuction

	public void LoadBannerView(){
		if (PlayerPrefs.GetInt ("Removeads", 0) == 0) {
		this.myBannerView = new BannerView (BannerAdID, AdSize.Banner, AdPosition.Top);
		AdRequest request = new AdRequest.Builder ().Build ();
		this.myBannerView.LoadAd (request);
		hideBanner ();
	}
	}

	public void showBanner(){
		if (PlayerPrefs.GetInt ("Removeads", 0) == 0) {
			myBannerView.Show ();
		}
	}
	public void hideBanner(){
		myBannerView.Hide ();
	}


	// Admob interstitial ads fuctions

	public void initAdmobInterstitial()
	{

		if (PlayerPrefs.GetInt ("Removeads", 0) == 0) {
		myInterstitialAd = new InterstitialAd(InterstitialAdID);
		AdRequest request = new AdRequest.Builder().Build();
		myInterstitialAd.LoadAd(request);
		myInterstitialAd.OnAdClosed += admobClosed;
		myInterstitialAd.OnAdLoaded += delegate(object sender, EventArgs args)
		{
			if(showAdmobInsterstitial)
			{
				showAdmobInsterstitial = false;
				myInterstitialAd.Show();
			}
		};
	}
	}

	void admobClosed(object sender, EventArgs args)
	{

		print("interstitial closed.");
		requestInterstitial ();

		// Handle the ad loaded event.
	}
	public void requestInterstitial(){
		if (PlayerPrefs.GetInt ("Removeads", 0) == 0) {
			AdRequest request = new AdRequest.Builder ().Build ();
			myInterstitialAd.LoadAd (request);
		}
	}

	public void ShowInterstitail()
	{

		if (PlayerPrefs.GetInt ("Removeads", 0) == 0) {
		if(myInterstitialAd.IsLoaded() == false)
		{
			showAdmobInsterstitial = true;

			requestInterstitial();
		}
		else
			myInterstitialAd.Show();


	}
	}




	//heyzap

	public void InitHeyzap()
	{
		if (PlayerPrefs.GetInt ("Removeads", 0) == 0) {
		#if !UNITY_EDITOR
		HeyzapAds.Start (heyzapAD, HeyzapAds.FLAG_NO_OPTIONS);
		HZInterstitialAd.Fetch ();
		#endif
	}
	}

	public void heyzapShow()
	{
		if (PlayerPrefs.GetInt ("Removeads", 0) == 0) {
		#if !UNITY_EDITOR
		HZInterstitialAd.Show ();
		HZInterstitialAd.Fetch ();
		#endif
	}
	}


	public void testsuit(){
		if (PlayerPrefs.GetInt ("Removeads", 0) == 0) {
			HeyzapAds.ShowMediationTestSuite ();
		}
	}





	//unity ads

	public void InitUnityadd()
	{
		if (PlayerPrefs.GetInt ("Removeads", 0) == 0) {

		if(!Advertisement.isInitialized){
			Advertisement.Initialize (unityid);
			print ("unity init."+Advertisement.isInitialized);
		}

	}
	}

	public void ShowUnityADS()
	{
		if (PlayerPrefs.GetInt ("Removeads", 0) == 0) {
		if (Advertisement.IsReady ()) {
			Advertisement.Show ();
		} else {
			InitUnityadd ();
		}
	}
	}

	//unity rewarded videoooooo

	public void ShowRewardedUnityADS()
	{
		//		#if !UNITY_EDITOR
		if(Advertisement.IsReady())
		{ 

			if (PlayerPrefs.GetInt ("Removeads", 0) == 0) {
				
			Advertisement.Show(null, new ShowOptions {
				resultCallback = result => {
					Debug.Log(result.ToString());


					if(result == ShowResult.Finished) {
						Debug.Log ("Watched full Video . Result was :: "+result);
						//		CoinManager.Instance.AddCoins(1);
						//Give Your Reward Here
					}
					else {
						Debug.Log ("No award given. Result was :: "+result);
					}

				}
			});

			}}
		else
			Debug.Log("UnityAD Failed to Fetch");

		//		#endif
	
	}






	//  ******************************  priority ads ***************************************************


	public void levelfailed(){
		#if !UNITY_EDITOR
		if (PlayerPrefs.GetInt ("Removeads", 0) == 0) {

		if (HZInterstitialAd.IsAvailable ()) {
		heyzapShow ();
		InitHeyzap();

		} else if (Advertisement.IsReady ()) {
		Advertisement.Show ();
		InitUnityadd();
		} }
		#endif
	
}

	public void Levelcomplete(){

		#if !UNITY_EDITOR

	if (PlayerPrefs.GetInt ("Removeads", 0) == 0) {
		if (Advertisement.IsReady ()) {
		Advertisement.Show ();
		InitUnityadd();
		}}
		#endif
	
}

	public void Levelpaused(){

		#if !UNITY_EDITOR
		if (PlayerPrefs.GetInt ("Removeads", 0) == 0) {
		if (Advertisement.IsReady ()) {
		Advertisement.Show ();
		InitUnityadd();
		} 

		else if (HZInterstitialAd.IsAvailable ()) {
		heyzapShow ();
		InitHeyzap();

		}}
		#endif
	}

	public void Levelquit ()
	{
		#if !UNITY_EDITOR
		if (PlayerPrefs.GetInt ("Removeads", 0) == 0) {
		if (HZInterstitialAd.IsAvailable ())
		{
			heyzapShow();
		}}
		#endif
	}

//before loading admob intertitail

	public void onLoadingScene(){
		if (PlayerPrefs.GetInt ("Removeads", 0) == 0) {
		if (myInterstitialAd.IsLoaded()) {
			ShowInterstitail ();
			initAdmobInterstitial ();
			print ("admob");
		}
	}
	}






	//***************************** test scene functions *****************************************************************************





	public void LoadInterstitialAd(){
		if (PlayerPrefs.GetInt ("Removeads", 0) == 0) {
			AdRequest request = new AdRequest.Builder ().Build ();
			myInterstitialAd.LoadAd (request);
		}
	}

	public void showInterstitialAd(){
		if (PlayerPrefs.GetInt ("Removeads", 0) == 0) {
		if (myInterstitialAd.IsLoaded ()) {
			myInterstitialAd.Show ();
		} else {
			LoadInterstitialAd ();
		}
	}
	}

	public void destroyInterstitailAd(){
		if (PlayerPrefs.GetInt ("Removeads", 0) == 0) {
			myInterstitialAd.Destroy ();
		}
	}




	public void nextScene(){
		SceneManager.LoadScene (1);
	}


}
