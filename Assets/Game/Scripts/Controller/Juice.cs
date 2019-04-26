using UnityEngine;
using System.Collections;

public class Juice : MonoBehaviour {

    private Transform cameraPos;

    //to make juice attract toward specific ships
    public float magnetStrength = 0.5f;
    public int magnetDirection = 1; // 1 to attract and -1 to repel
    public bool looseMagnet = true;

    private Transform trans; // ref to this game obejct
    private Rigidbody2D rigidBD;
    private Transform shipTransform;//ref to ship
    private bool magnetZone;//to check wheather the ship is withing range

    void Awake()
    {
        trans = transform;
        rigidBD = trans.GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start ()
    {
        cameraPos = Camera.main.gameObject.transform;
    }
	
	// Update is called once per frame
	void Update ()
    {
        OutOfScreen();

        if (GameController.instance.isGameOver)
            return;

        MagneticEffect();
    }

    //when the juice goes out of scrren it is set to deactive
    void OutOfScreen()
    {
        //we get the camera postions
        cameraPos = Camera.main.gameObject.transform;
        //get the juice positions
        float x = transform.position.x;
        float y = transform.position.y;
        //the orthographic size is half of height , to get width we multiply orthographic size with aspect
        float maxX = cameraPos.position.x + Camera.main.orthographicSize * Camera.main.aspect;
        float minX = cameraPos.position.x - Camera.main.orthographicSize * Camera.main.aspect;
        float maxY = cameraPos.position.y + Camera.main.orthographicSize;
        float minY = cameraPos.position.y - Camera.main.orthographicSize;

        //we check if the object is out of screen
        //deactivate
        if (x > maxX + 4 || x < minX - 4 || y > maxY + 4 || y < minY - 4)
        {
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerAttract"))
        {
            shipTransform = other.transform;
            magnetZone = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PlayerAttract") & looseMagnet)
            magnetZone = false;
    }

    void MagneticEffect()
    {
        if (magnetZone)
        {
            if (shipTransform != null)
            {
                Vector3 directionToShip = shipTransform.position - trans.position;
                rigidBD.AddForce(directionToShip * magnetDirection * Time.deltaTime, ForceMode2D.Force);
            }
        }
    }
}
