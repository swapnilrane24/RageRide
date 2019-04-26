using UnityEngine;
using System.Collections;

public class PlayerSpawner : MonoBehaviour {

    [SerializeField]
    private GameObject[] players;
    private int selectedPlayer;

    void Awake()
    {
        //we check which player is selected
        selectedPlayer = GameController.instance.selectedPlayer;

        //depending on that we spawn the player from our array
        /// <summary>
        /// NOTE: Remember that the shipIndex in button must be equal to the corresponding player in the array  
        /// </summary>
        GameObject playerObj = (GameObject)Instantiate(players[selectedPlayer], transform.position, Quaternion.identity);
    }

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
