using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;

namespace Puppeteer
{
	/// <summary>
	/// This script links UnityAds to your app. You must set the GameID and how often the ads appear.
	/// The Game Controller in the scene calls this script on Game Over to try and show an ad.
	/// </summary>
	public class PupUnityAdsController:MonoBehaviour 
	{
		// The game ID of the app registered on the UnityAds website (http://unityads.unity3d.com/)
		public string gameID = "73177";
		
		// How often to show the ad. The ad is shown by default when restarting after Game Over
		public int showAdEvery = 3;
		
		// The current count to the next advertisment
		public int adCount = 0;
		
		// This is set for testing purposes. When releasing the actual game, set this to false
		public bool testMode = true;

		// The screen that appears when the player watches an ad to the end and gets a reward
		public Transform rewardScreen;

		// Runs before Start()
		void Awake()
		{
			// Get the current ad count from PlayerPrefs
			adCount = PlayerPrefs.GetInt( "AdCount", adCount);
			
			// Checking and initializing unity ads
			if (Advertisement.isSupported)
			{
				// Initialize the ad service. The number is your app ID on the unity ads website
				if (testMode == true)
				{
					// Runnig in test mode
					Advertisement.Initialize(gameID, true);
				}
				else
				{
					// Running in release mode
					Advertisement.Initialize(gameID, false);
				}
			}
		}

		/// <summary>
		/// Counts the ad and then shows it
		/// </summary>
		public void CountAd()
		{
			// Count to the next time we need to show an ad
			if ( adCount < showAdEvery )
			{
				adCount++;
			}
			else
			{
				ShowAd(false);
			}
			
			// Record the current ad count in PlayerPrefs
			PlayerPrefs.SetInt( "AdCount", adCount);
		}

		/// <summary>
		/// Shows the ad, with or without a reward
		/// </summary>
		/// <param name="giveReward">If set to <c>true</c> give reward.</param>
		public void ShowAd( bool giveReward )
		{
			// Show the ad and check if the player watches it to the end for a reward
			if ( giveReward == true ) 
			{
				// If the ad is ready, show it and reset the ad counter
				if ( Advertisement.IsReady("rewardedVideoZone") )
				{
					var options = new ShowOptions { resultCallback = HandleShowResult };

					Advertisement.Show ("rewardedVideoZone", options);
				} 
			}
			else if ( Advertisement.IsReady("defaultZone") )
			{
				Advertisement.Show("defaultZone");
			}

			adCount = 0;

			// Record the current ad count in PlayerPrefs
			PlayerPrefs.SetInt( "AdCount", adCount);
		}

		/// <summary>
		/// Checks if an ad was viewed by the user to the end, then gives a reward
		/// </summary>
		/// <param name="result">Result.</param>
		private void HandleShowResult(ShowResult result)
		{
			switch (result)
			{
			case ShowResult.Finished:
				Debug.Log("The ad was successfully shown.");

				//Create the reward screen after we saw the ad
				if ( rewardScreen )    Instantiate(rewardScreen);
				
				break;
			case ShowResult.Skipped:
				Debug.Log("The ad was skipped before reaching the end.");
				break;
			case ShowResult.Failed:
				Debug.LogError("The ad failed to be shown.");
				break;
			}
		}

		/// <summary>
		/// At the start of the level counts towards showing the ad. For example after 3 times loading the level, an ad will be shown at the start of the level
		/// </summary>
		public void OnLevelWasLoaded()
		{
			CountAd();
		}
	}
}
