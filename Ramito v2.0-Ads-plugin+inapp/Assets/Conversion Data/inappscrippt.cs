using UnityEngine;
using System.Collections;

public class inappscrippt : MonoBehaviour {
	
	
	public GameObject RemovedBtn ;
	public static bool isadsOnetime = true;
	// Use this for initialization
	void Start () {
		isadsOnetime = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(InApp.instance.isAdsRemovePurchased() && isadsOnetime){
			

			isadsOnetime =false;
		}
	}
	
	
	public void dollarbtn (){
		if(InApp.instance.isAdsRemovePurchased()){
			return;
		}
		InApp.instance.RemoveAds();
		
	}
	public void restorebtn (){
		InApp.instance.RestorePurchases();
		
	}
	public void closeAds (){
		Application.LoadLevel (0);
	}
}