using UnityEngine;
using System.Collections;

public class PurchaseHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
		adsManager.Instance.showBanner ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void inappButton(){
		InApp.instance.BuyCoins (0);
	}
}
