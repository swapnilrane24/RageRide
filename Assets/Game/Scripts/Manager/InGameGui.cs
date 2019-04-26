using UnityEngine;
using System.Collections;
using UnityEngine.UI;
#if UNITY_5_3
using UnityEngine.SceneManagement;
#endif

public class InGameGui : MonoBehaviour {

    public static InGameGui instance;

    [SerializeField] private Text score;
    [SerializeField] private Text scoreGameOver;
    [SerializeField] private Text hiScore;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] private string playScene;
    [SerializeField] private string menuScene;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Use this for initialization
    void Start ()
    {
        if (gameOverPanel.activeSelf)
            gameOverPanel.SetActive(false);

        score.text = "" + GameController.instance.currentScore;

        if (GameController.instance.isMusicOn)
        {
            AudioListener.volume = 1;
        }
        else
        {
            AudioListener.volume = 0;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        HiScore();
        score.text = "" + GameController.instance.currentScore;
        if (GameController.instance.isGameOver)
        {
            StartCoroutine(WaitForExplosion());
        }
    }

    void HiScore()
    {
        if (GameController.instance.hiScore < GameController.instance.currentScore)
            GameController.instance.hiScore = GameController.instance.currentScore;
    }

    public void MainMenuButton()
    {
        GameController.instance.isGameOver = false;
        #if UNITY_5_3
        SceneManager.LoadScene(menuScene);
        #else
        Application.LoadLevel(menuScene);
        #endif
    }

    public void PlayButton()
    {
        GameController.instance.isGameOver = false;
        #if UNITY_5_3
        SceneManager.LoadScene(playScene);
        #else
        Application.LoadLevel(playScene);
        #endif
    }

    IEnumerator WaitForExplosion()
    {
        yield return new WaitForSeconds(0.5f);
        score.gameObject.SetActive(false);
        scoreGameOver.text = "" + GameController.instance.currentScore;
        hiScore.text = "" + GameController.instance.hiScore;
        gameOverPanel.SetActive(true);
    }

}
