using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameController : MonoBehaviour {

    public static GameController instance;

    private GameData data;

    //not to store on device
    public bool isGameOver = false;
    public int currentScore;

    //to store on device
    public bool isGameStartedFirstTime;
    public bool isMusicOn;
    public int selectedPlayer;
    public int juice;
    public int hiScore;
    public bool[] players; // this keep track of which player is locked and which is not
    public bool[] achievements;//                           ||
    public float anchorPosX;
    public float anchorPosY;

    [HideInInspector]
    public int gamesPlayed;

    void Awake()
    {
        MakeSingleton();
        //isGameStartedFirstTime = true;
        //Save();
        //Load();
        InitializeGameVariables();
    }

    void MakeSingleton()
    {     
        //this state that if the gameobject to which this script is attached , if it is present in scene then destroy the new one , and if its not present
        //then create new 
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void InitializeGameVariables()
    {
        Load();
        if (data != null)
        {
            isGameStartedFirstTime = data.getIsGameStartedFirstTime();
        }
        else
        {
            isGameStartedFirstTime = true;
        }

        if (isGameStartedFirstTime)
        {
            hiScore = 0;
            juice = 200;
            selectedPlayer = 0;
            isGameStartedFirstTime = false;
            isMusicOn = false;
            players = new bool[5];             //change this if you change number of players and same with achievements
            achievements = new bool[5];
            anchorPosX = 0;
            anchorPosY = 0;

            players[0] = true;
            for (int i = 1; i < players.Length; i++)
            {
                players[i] = false;
            }

            for (int i = 0; i < achievements.Length; i++)
            {
                achievements[i] = false;
            }

            data = new GameData();

            data.setHiScore(hiScore);
            data.setJuice(juice);
            data.setIsGameStartedFirstTime(isGameStartedFirstTime);
            data.setPlayers(players);
            data.setSelectedPlayer(selectedPlayer);
            data.setMusicOn(isMusicOn);
            data.setAchievements(achievements);
            data.setAnchorePosX(anchorPosX);
            data.setAnchorePosY(anchorPosY);

            Save();
            Load();
        }
        else
        {
            hiScore = data.getHiScore();
            juice = data.getJuice();
            selectedPlayer = data.getSelectedPlayer();
            isGameStartedFirstTime = data.getIsGameStartedFirstTime();
            isMusicOn = data.getMusicOn();
            players = data.getPlayers();
            achievements = data.getAchievements();
            anchorPosX = data.getAnchorePosX();
            anchorPosY = data.getAnchorePosY();
        }
    }

    //                              .........this function take care of all saving data like score , current player , current weapon , etc
    public void Save()
    {
        FileStream file = null;

        //whicle working with input and output we use try and catch
        try
        {
            BinaryFormatter bf = new BinaryFormatter();

            file = File.Create(Application.persistentDataPath + "/GameData.dat");
            if (data != null)
            {
                data.setHiScore(hiScore);
                data.setJuice(juice);
                data.setIsGameStartedFirstTime(isGameStartedFirstTime);
                data.setPlayers(players);
                data.setSelectedPlayer(selectedPlayer);
                data.setMusicOn(isMusicOn);
                data.setAchievements(achievements);
                data.setAnchorePosX(anchorPosX);
                data.setAnchorePosY(anchorPosY);
                bf.Serialize(file , data);
            }
        }
        catch (Exception e) { }
        finally
        {
            if (file != null)
            {
                file.Close();
            }            
        }
    }
    //                            .............here we get data from save
    public void Load()
    {
        FileStream file = null;

        try {
            BinaryFormatter bf = new BinaryFormatter();
            file = File.Open(Application.persistentDataPath + "/GameData.dat" , FileMode.Open);
            data = (GameData) bf.Deserialize(file);
        }
        catch (Exception e) {
        }
        finally {
            if (file != null)
            {
                file.Close();
            }
        }
    }
}

[Serializable]
class GameData
{
    private bool isGameStartedFirstTime;
    private bool isMusicOn;
    private int selectedPlayer;
    private int juice;
    private int hiScore;
    private bool[] players; // this keep track of which player is locked and which is not
    private bool[] achievements;//                           ||
    private float anchorPosX;
    private float anchorPosY;

    public void setIsGameStartedFirstTime(bool isGameStartedFirstTime)
    {
        this.isGameStartedFirstTime = isGameStartedFirstTime;
    }

    public bool getIsGameStartedFirstTime()
    {
        return this.isGameStartedFirstTime;
    }
    //                                                                    ...............music
    public void setMusicOn(bool isMusicOn)
    {
        this.isMusicOn = isMusicOn;
    }

    public bool getMusicOn()
    {
        return this.isMusicOn;
    }
    //                                                                      .......music
    //                                                                       ......player
    public void setSelectedPlayer(int selectedPlayer)
    {
        this.selectedPlayer = selectedPlayer;
    }

    public int getSelectedPlayer()
    {
        return this.selectedPlayer;
    }
    //                                                                 ................player
    //                                                                ....................coins
    public void setJuice(int juice)
    {
        this.juice = juice;
    }

    public int getJuice()
    {
        return this.juice;
    }
    //                                                                  ........................coins
    //                                                              ........................highSocre
    public void setHiScore(int hiScore)
    {
        this.hiScore = hiScore;
    }

    public int getHiScore()
    {
        return this.hiScore;
    }
    //                                                            .......................highScore
    //                                                                 ..................Player locked/unlocked
    public void setPlayers(bool[] players)
    {
        this.players = players;
    }

    public bool[] getPlayers()
    {
        return this.players;
    }
    //                                                                     ...............Player locked/unlocked
    //                                                                                 .....Achievements unlocked
    public void setAchievements(bool[] achievements)
    {
        this.achievements = achievements;
    }

    public bool[] getAchievements()
    {
        return this.achievements;
    }
    //                                                                                 .....Achievements unlocked

    //the below variables are to store the anchore position of ship button
    public void setAnchorePosX(float anchorPosX)
    {
        this.anchorPosX = anchorPosX;
    }

    public float getAnchorePosX()
    {
        return this.anchorPosX;
    }

    public void setAnchorePosY(float anchorPosY)
    {
        this.anchorPosY = anchorPosY;
    }

    public float getAnchorePosY()
    {
        return this.anchorPosY;
    }

}