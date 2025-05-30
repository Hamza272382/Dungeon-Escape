using UnityEngine;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsListener
{
    private string rewardedAdId = "Rewarded_Android"; // Placement ID for rewarded ads
    private string gameId = "5860036"; // Replace with your actual Unity Game ID
    private bool testMode = true; // Flag to enable test mode for ads

    void Start()
    {
        Advertisement.AddListener(this); // Register this script as a listener for ad events

        if (!Advertisement.isInitialized)
        {
            Advertisement.Initialize(gameId, testMode); // Initialize Unity Ads with game ID and test mode setting
        }
    }

    // Method to show rewarded ad when requested
    public void ShowRewardedAd()
    {
        Debug.Log("Attempting to show Rewarded Ad...");

        if (Advertisement.IsReady(rewardedAdId))
        {
            Advertisement.Show(rewardedAdId); // Show rewarded ad if ready
        }
        else
        {
            Debug.LogWarning("Rewarded ad not ready."); // Warn if ad not ready to show
        }
    }

    // Called when an ad placement becomes ready
    public void OnUnityAdsReady(string placementId)
    {
        Debug.Log("Ad Ready: " + placementId);
    }

    // Called when an ad starts playing
    public void OnUnityAdsDidStart(string placementId)
    {
        Debug.Log("Ad Started: " + placementId);
    }

    // Called when an error occurs with Unity Ads
    public void OnUnityAdsDidError(string message)
    {
        Debug.LogError("Unity Ads Error: " + message);
    }

    // Called when an ad finishes playing
    public void OnUnityAdsDidFinish(string placementId, ShowResult result)
    {
        // Check if the finished ad is the rewarded ad
        if (placementId == rewardedAdId)
        {
            switch (result)
            {
                case ShowResult.Finished:
                    Debug.Log("Ad Finished — reward the player here");
                    GameManager.Instance.Player.AddGems(100); // Reward player with gems
                    UIManager.Instance.OpenShop(GameManager.Instance.Player.Diamonds); // Update shop UI with new gem count
                    break;

                case ShowResult.Skipped:
                    Debug.Log("Ad Skipped — no reward"); // Player skipped ad, no reward given
                    break;

                case ShowResult.Failed:
                    Debug.LogWarning("Ad Failed to show"); // Ad failed to show properly
                    break;
            }
        }
    }

    // Clean up listener when this object is destroyed
    void OnDestroy()
    {
        Advertisement.RemoveListener(this);
    }
}
