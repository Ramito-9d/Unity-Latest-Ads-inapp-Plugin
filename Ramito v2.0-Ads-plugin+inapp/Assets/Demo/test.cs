using UnityEngine;
using System.Collections;

public class test : MonoBehaviour {

	// Use this for initialization
	void Start () {

		//		adsManager.Instance.LoadInterstitialAd ();

//		adsManager.Instance.showInterstitialAd ();
//		Invoke ("showtest", 3f);
	}

//	void showtest(){
//		adsManager.Instance.showInterstitialAd ();
//	}

	public void complete(){
		adsManager.Instance.Levelcomplete();
	}
	public void fail(){
		adsManager.Instance.levelfailed();
	}
	public void pause(){
		adsManager.Instance.Levelpaused();
	}
	public void quit(){
		adsManager.Instance.Levelquit();
	}
	public void onloading(){
		adsManager.Instance.onLoadingScene ();
	}
}
