using UnityEngine;
using System.Collections;

public class SpawnerFollow : MonoBehaviour {

    private Vector3 lastPlayerPos;
    private Transform player;

    // Use this for initialization
    void Start ()
    {
        player = PlayerCOntroller.instance.player;

        if (PlayerCOntroller.instance != null)
            lastPlayerPos = player.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
        Follow();
    }

    void Follow()
    {
        if (PlayerCOntroller.instance == null)
            return;

        float distanceToMoveX = player.position.x - lastPlayerPos.x;

        float distanceToMoveY = player.position.y - lastPlayerPos.y;

        transform.position = new Vector3(transform.position.x + distanceToMoveX, transform.position.y + distanceToMoveY, transform.position.z);


        lastPlayerPos = player.position;

    }
}
