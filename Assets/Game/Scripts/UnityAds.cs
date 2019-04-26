using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

#if UNITY_ADS
using UnityEngine.Advertisements;
#endif

public class UnityAds : MonoBehaviour
{
    public static UnityAds instance;

    private int i = 0;
    private bool rewardAdReady = false;

    private bool doubleCoins = false;

    public bool RewardAdReady
    {
        get { return rewardAdReady; }
    }

    [HideInInspector]
    public ManageVariables vars;

    void OnEnable()
    {
        vars = Resources.Load<ManageVariables>("ManageVariablesContainer");
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
#if UNITY_ADS
        if (Advertisement.IsReady("rewardedVideo"))
        {
            rewardAdReady = true;
        }
        else if (!Advertisement.IsReady("rewardedVideo"))
        {
            rewardAdReady = false;
        }
#endif
    }

    public void ShowAd()
    {
#if UNITY_ADS
        if (Advertisement.IsReady())
        {
            Advertisement.Show();
        }
#endif
    }

    //use this function for showing reward ads
    public void ShowRewardedAd()
    {
#if UNITY_ADS
        if (Advertisement.IsReady("rewardedVideo"))
        {
            var options = new ShowOptions { resultCallback = HandleShowResult };
            Advertisement.Show("rewardedVideo", options);
        }
        else
        {
            Debug.Log("Ads not ready");
        }
#endif
}

#if UNITY_ADS
    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");
                GameController.instance.juice += 20; /*here we give 20 poinst as reward*/

                GameController.instance.Save();
                MainMenuManager.instance.TotalJuice.text = GameController.instance.juice;
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");

                break;
        }
    }
#endif

}
