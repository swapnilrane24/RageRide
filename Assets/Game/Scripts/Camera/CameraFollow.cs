using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public static CameraFollow instance;

    //ref to player position
    private Vector3 lastPlayerPos;
    //ref to player movement
    private Transform player;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

	// Use this for initialization
	void Start ()
    {
        player = PlayerCOntroller.instance.player;

        //we check for player and set the value of lastplayer pos
        if (PlayerCOntroller.instance != null)
            lastPlayerPos = player.position;
	
	}

	// Update is called once per frame
	void LateUpdate ()
    {
        Follow();
    }

    //thi method make the camera follow player ship
    void Follow()
    {
        if (PlayerCOntroller.instance == null)
            return;

        //the amount by which camera to move is the diffrence between player current position and player last position
        float distanceToMoveX = player.position.x - lastPlayerPos.x;

        float distanceToMoveY = player.position.y - lastPlayerPos.y;

        //then we input thos value into camera and move move it
        transform.position = new Vector3(transform.position.x + distanceToMoveX, transform.position.y + distanceToMoveY, transform.position.z);
        //we then set the current position as last player position
        lastPlayerPos = player.position;

    }
}
