using UnityEngine;
using System.Collections;

public class ObjectSpawners : MonoBehaviour {

    public enum SpawnPos
    {
        top,
        bottom,
        right,
        left
    }

    //ref to which spawner this script is attached
    public SpawnPos spawnPos;
    //ref to player last position
    private Vector3 lastPlayerPos; 
    //ref to player movement
    private Transform currentPlayerPos;
    //the limite when the ship cross we spawn the enemies in corresponding direction
    public float minShipMove; 
    //ref to box collider
    private BoxCollider2D box;

    // Use this for initialization
    void Start ()
    {
        box = GetComponent<BoxCollider2D>();

        if (PlayerCOntroller.instance != null)
        {
            currentPlayerPos = PlayerCOntroller.instance.player;
            lastPlayerPos = currentPlayerPos.position;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        currentPlayerPos = PlayerCOntroller.instance.player;
        SpawnObject();
    }

    void SpawnObject()
    {
        //we get half width and height of collider
        float colliderWidth = (box.size.x)/2;
        float colliderHeight = (box.size.y)/2;

        float maxX = transform.position.x + colliderWidth;
        float minX = transform.position.x - colliderWidth;
        float maxY = transform.position.y + colliderHeight;
        float minY = transform.position.y - colliderHeight;

        switch (spawnPos)
        {
            case (SpawnPos.top):

                //when ship moves top
                if (Mathf.Abs((currentPlayerPos.position.y - lastPlayerPos.y)) > minShipMove)
                {
                    for (int d = 0; d < 4; d++)
                    {
                        //Vector3 positionTop = new Vector3(Random.Range(minX, maxX), transform.position.y, transform.position.z);
                        int i = Random.Range(0, 10);
                        if (i >= 0 && i <= 5)
                        {
                            //SpawnBigMeteor(positionTop);
                            SpawnBigMeteor(GeneratedPlace(Random.Range(minX, maxX), transform.position.y));
                        }
                        else if (i >= 6 && i <= 8)
                        {
                            //SpawnMidMeteor(positionTop);
                            SpawnMidMeteor(GeneratedPlace(Random.Range(minX, maxX), transform.position.y));
                        }
                        else if (i > 8)
                        {
                            //SpawnJuice(positionTop);
                            SpawnJuice(GeneratedPlace(Random.Range(minX, maxX), transform.position.y));
                        }
                    }
                    lastPlayerPos = currentPlayerPos.position;
                }
                break;

            case (SpawnPos.bottom):

                //when ship moves bottom
                if (Mathf.Abs((currentPlayerPos.position.y - lastPlayerPos.y)) > minShipMove)
                {
                    for (int c = 0; c < 4; c++)
                    {
                        //Vector3 positionBottom = new Vector3(Random.Range(minX, maxX), transform.position.y, transform.position.z);
                        int j = Random.Range(0, 10);
                        if (j >= 0 && j <= 5)
                        {
                            //SpawnBigMeteor(positionBottom);
                            SpawnBigMeteor(GeneratedPlace(Random.Range(minX, maxX), transform.position.y));
                        }
                        else if (j >= 6 && j <= 8)
                        {
                            //SpawnMidMeteor(positionBottom);
                            SpawnMidMeteor(GeneratedPlace(Random.Range(minX, maxX), transform.position.y));
                        }
                        else if (j > 8)
                        {
                            //SpawnJuice(positionBottom);
                            SpawnJuice(GeneratedPlace(Random.Range(minX, maxX), transform.position.y));
                        }
                    }
                    lastPlayerPos = currentPlayerPos.position;
                }
                break;

            case (SpawnPos.left):

                //when ship moves left
                if (Mathf.Abs((currentPlayerPos.position.x - lastPlayerPos.x)) > minShipMove)
                {
                    for (int b = 0; b < 4; b++)
                    {
                        //Vector3 positionLeft = new Vector3(transform.position.x, Random.Range(minY, maxY), transform.position.z);
                        int k = Random.Range(0, 10);
                        if (k >= 0 && k <= 5)
                        {
                            //SpawnBigMeteor(positionLeft);
                            SpawnBigMeteor(GeneratedPlace(transform.position.x, Random.Range(minY, maxY)));
                        }
                        else if (k >= 6 && k <= 8)
                        {
                            //SpawnMidMeteor(positionLeft);
                            SpawnMidMeteor(GeneratedPlace(transform.position.x, Random.Range(minY, maxY)));
                        }
                        else if (k > 8)
                        {
                            //SpawnJuice(positionLeft);
                            SpawnJuice(GeneratedPlace(transform.position.x, Random.Range(minY, maxY)));
                        }
                    }
                    lastPlayerPos = currentPlayerPos.position;
                }
                break;

            case (SpawnPos.right):

                //when ship moves right
                if (Mathf.Abs((currentPlayerPos.position.x - lastPlayerPos.x)) > minShipMove)
                {
                    for (int a = 0; a < 4; a++)
                    {
                        //Vector3 positionRight = new Vector3(transform.position.x, Random.Range(minY, maxY), transform.position.z);
                        int l = Random.Range(0, 10);
                        if (l >= 0 && l <= 5)
                        {
                            //SpawnBigMeteor(positionRight);
                            SpawnBigMeteor(GeneratedPlace(transform.position.x, Random.Range(minY, maxY)));
                        }
                        else if (l >= 6 && l <= 8)
                        {
                            //SpawnMidMeteor(positionRight);
                            SpawnMidMeteor(GeneratedPlace(transform.position.x, Random.Range(minY, maxY)));
                        }
                        else if (l > 8)
                        {
                            //SpawnJuice(positionRight);
                            SpawnJuice(GeneratedPlace(transform.position.x, Random.Range(minY, maxY)));
                        }
                    }
                    lastPlayerPos = currentPlayerPos.position;
                }
                break;
        }
    }

    void SpawnBigMeteor(Vector3 pos)
    {
        GameObject meteor = ObjectPooling.instance.GetBigMeteor();
        meteor.transform.position = pos;
        meteor.transform.rotation = Quaternion.identity;
        meteor.SetActive(true);
    }

    void SpawnMidMeteor(Vector3 pos)
    {
        GameObject meteor = ObjectPooling.instance.GetMidMeteor();
        meteor.transform.position = pos;
        meteor.transform.rotation = Quaternion.identity;
        meteor.SetActive(true);
    }

    void SpawnJuice(Vector3 pos)
    {
        GameObject juice = ObjectPooling.instance.GetJuice();
        juice.transform.position = pos;
        juice.transform.rotation = Quaternion.identity;
        juice.SetActive(true);
    }

    Vector3 GeneratedPlace(float X , float Y)
    {
        float x, y, z;
        x = X;
        y = Y;
        z = 0;

        return new Vector3(x, y, z);
    }

}
