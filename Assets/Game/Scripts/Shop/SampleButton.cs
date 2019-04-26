using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SampleButton : MonoBehaviour {

    public Image buttonIcon;
    public Button button;
    public Text buttonCost;
    public int cost;
    public int shipIndex; // this index is used to tell game manager which ship has unlocked

    [HideInInspector]
    public string description;

    // Use this for initialization
    void Start ()
    {
        button.GetComponent<Button>().onClick.AddListener(() => { BUttonPressed(); });
    }
	
	// Update is called once per frame
	void Update () {}

    /// <summary>
    /// When the player press the button the ship get unlocked if the juice is equal or greater than the cost
    /// </summary>
    public void BUttonPressed()
    {
        //1st we check if the ship is already unlocked is no we buy it and select it
        if (GameController.instance.players[shipIndex] == false)
        {
            if (GameController.instance.juice >= cost)
            {
                GameController.instance.players[shipIndex] = true;
                GameController.instance.juice = GameController.instance.juice - cost;
                //here we set which current player is selected
                GameController.instance.selectedPlayer = shipIndex;
                //here we save the button position
                Vector2 tempPos = ShipSelector.instance._container.anchoredPosition;
                GameController.instance.anchorPosX = tempPos.x;
                GameController.instance.anchorPosY = tempPos.y;
                GameController.instance.Save();
                buttonCost.text = "" + description;
            }
            else
            {
                //pop to buy juice or watch video
                MainMenuManager.instance.ShopPanel.SetActive(true);
            }
        }
        //we only select it
        else
        {
            //here we set which current player is selected
            GameController.instance.selectedPlayer = shipIndex;
            Vector2 tempPos = ShipSelector.instance._container.anchoredPosition;
            GameController.instance.anchorPosX = tempPos.x;
            GameController.instance.anchorPosY = tempPos.y;
            GameController.instance.Save();
        }
    }
}
