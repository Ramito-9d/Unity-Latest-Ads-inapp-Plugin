using UnityEngine;
using System;

public class InApp : MonoBehaviour
{
	public static InApp instance;
	private PurchasableItem[] items;
	private const int REMOVE_ADS_INDEX = 0;
	private const int COIN_CURRENCY_INDEX = 0;
	public static event Action<string> purchased;

	void Start ()
	{
		instance = this;
		DontDestroyOnLoad (gameObject);
		if (UnityEngine.Resources.Load ("unibillInventory.json") == null) {
			Debug.LogError ("You must define your purchasable inventory within the inventory editor!");
			this.gameObject.SetActive (false);
			return;
		}
		
		// We must first hook up listeners to Unibill's events.
		Unibiller.onBillerReady += onBillerReady;
		Unibiller.onTransactionsRestored += onTransactionsRestored;
		Unibiller.onPurchaseFailed += onFailed;
		Unibiller.onPurchaseCompleteEvent += onPurchased;
		Unibiller.onPurchaseDeferred += onDeferred;
		
		// Now we're ready to initialise Unibill.
		Unibiller.Initialise ();

		items = Unibiller.AllPurchasableItems;

		// iOS includes additional functionality around App receipts.
		#if UNITY_IOS
		var appleExtensions = Unibiller.getAppleExtensions();
		appleExtensions.onAppReceiptRefreshed += x => {
			Debug.Log(x);
			Debug.Log("Refreshed app receipt!");
		};
		appleExtensions.onAppReceiptRefreshFailed += () => {
			Debug.Log("Failed to refresh app receipt.");
		};
		#endif
	}

	public decimal GetCoins ()
	{
		return Unibiller.GetCurrencyBalance (Unibiller.AllCurrencies [COIN_CURRENCY_INDEX]);
	}

	public bool isAdsRemovePurchased ()
	{
		return (Unibiller.GetPurchaseCount (Unibiller.AllNonConsumablePurchasableItems [REMOVE_ADS_INDEX]) != 0);
	}

	public void BuyCoins (int index)
	{
		Unibiller.initiatePurchase (items [index]);
	}
	
	public void RemoveAds ()
	{
		Unibiller.initiatePurchase (items [REMOVE_ADS_INDEX]);
	}
	
	public void RestorePurchases ()
	{
		Unibiller.restoreTransactions ();
	}

	/// <summary>
	/// Event Listeners
	/// </summary>
	void onBillerReady (UnibillState state)
	{
		Debug.Log ("onBillerReady:" + state);
	}

	void onTransactionsRestored (bool success)
	{
		Debug.Log ("Transactions restored.");
	}

	void onFailed (Unibill.PurchaseFailedEvent e)
	{
		Debug.Log ("Purchase failed: " + e.PurchasedItem.Id);
		Debug.Log (e.Reason);
	}

	void onPurchased (Unibill.PurchaseEvent e)
	{
		purchased (e.PurchasedItem.Id);
		Debug.Log ("Purchase OK: " + e.PurchasedItem.Id);
		Debug.Log ("Receipt: " + e.Receipt);
		Debug.Log (string.Format ("{0} has now been purchased {1} times.",
		                         e.PurchasedItem.name,
		                         Unibiller.GetPurchaseCount (e.PurchasedItem)));
	}

	void onDeferred (PurchasableItem item)
	{
		Debug.Log ("Purchase deferred blud: " + item.Id);
	}
}
