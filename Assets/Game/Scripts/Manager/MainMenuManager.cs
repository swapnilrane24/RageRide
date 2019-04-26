using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour {

    public static MainMenuManager instance;

    public GameObject ShopPanel;
    public Button adsButton;
    [SerializeField] private Text totalJuice;
    [SerializeField] private GameObject infoPanel;
    [SerializeField] private Button musicBtn;
    [SerializeField] private Sprite[] musicBtnSprite;
    [SerializeField] private string playScene;
    [SerializeField] private string gameLink = "Type link address of you game";
    [SerializeField] private string moreGameLink = "Type link address of page which contain all you games";
    private AudioSource audioSource; // ref to audio source attached to gameobject

    public Text TotalJuice { set { totalJuice = value; } }

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Use this for initialization
    void Start ()
    {
#if AdmobDef
        AdManager.instance.ShowBannerAds();
#endif
        audioSource = GetComponent<AudioSource>();
        if (infoPanel.activeSelf)
        infoPanel.SetActive(false);

        if (ShopPanel.activeSelf)
            ShopPanel.SetActive(false);

        if (GameController.instance.isMusicOn)
        {
            musicBtn.image.sprite = musicBtnSprite[1];
            AudioListener.volume = 1;
        }
        else
        {
            musicBtn.image.sprite = musicBtnSprite[0];
            AudioListener.volume = 0;
        }
    }
	
	// Update is called once per frame
	void Update () {
        totalJuice.text = "" + GameController.instance.juice;
    }

    public void PlayButton()
    {
        audioSource.Play();
        SceneManager.LoadScene(playScene);
    }

    public void MusicButton()
    {
        audioSource.Play();

        if (GameController.instance.isMusicOn)
        {
            AudioListener.volume = 0;
            musicBtn.image.sprite = musicBtnSprite[1];
            GameController.instance.isMusicOn = false;
            GameController.instance.Save();
        }
        else
        {
            AudioListener.volume = 1;
            musicBtn.image.sprite = musicBtnSprite[0];
            GameController.instance.isMusicOn = true;
            GameController.instance.Save();
        }
    }

    public void RateButton()
    {
        audioSource.Play();
        Application.OpenURL(gameLink);
    }

    public void MoreGameButton()
    {
        audioSource.Play();
        Application.OpenURL(moreGameLink);
    }

    public void InfoButton()
    {
        audioSource.Play();
        infoPanel.SetActive(true);
    }

    public void InfoCloseButton()
    {
        audioSource.Play();
        infoPanel.SetActive(false);
    }

    public void AchievementButton() //use it for achievemnet button
    {
#if GooglePlayDef
        GooglePlayServiceManager.instance.OpenAchievements();
#endif
        audioSource.Play();
    }

    public void LeaderboardButton() //use it for achievemnet button
    {
#if GooglePlayDef
        GooglePlayServiceManager.instance.OpenLeaderboardsScore();
#endif
        audioSource.Play();
    }

    public void ShopCLoseButton()
    {
        audioSource.Play();
        ShopPanel.SetActive(false);
    }

    public void AdsShowButton()
    {
        audioSource.Play();
#if UNITY_ADS
       UnityAds.instance.ShowRewardedAd();
#endif

    }
}
