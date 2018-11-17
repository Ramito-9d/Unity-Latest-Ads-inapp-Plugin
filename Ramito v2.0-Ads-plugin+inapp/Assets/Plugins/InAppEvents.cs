using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InAppEvents : MonoBehaviour
{

		void Start ()
		{
				InApp.purchased += Purchased;
		}

		public void BuyCoins (int index)
		{
				InApp.instance.BuyCoins (index);
		}
	
		public void RemoveAds ()
		{
				InApp.instance.RemoveAds ();
		}

		void Purchased (string id)
		{
				switch (id) {
				case "com.appteeka.testInapp23.removeads":
						PlayerPrefs.SetInt ("Removeads", 1); 
						PlayerPrefs.Save ();
						print("you remove the ads..." + PlayerPrefs.GetInt("Removeads"));
						break;
				case "com.appteeka.clash.egyptian.archers.3d7kcoins":
						PlayerPrefs.SetString ("bundle2", "yes");
						PlayerPrefs.Save ();
						break;
				case "com.appteeka.clash.egyptian.archers.3d10kcoins":
						PlayerPrefs.SetString ("bundle3", "yes");
						PlayerPrefs.Save ();
						break;
				default:
			
						break;
				}
		}
}
