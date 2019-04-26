using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public enum SceneMode
{
    mainMenu,
    gamePlay
}

public class Meteor : MonoBehaviour {

    //to make juice attract toward specific ships
    public float magnetStrength = 0.5f;
    public int magnetDirection = -1; // 1 to attract and -1 to repel
    public bool looseMagnet = true;

    private Transform trans; // ref to this game obejct
    private Rigidbody2D rigidBD;
    private Transform shipTransform;//ref to ship
    private bool magnetZone;//to check wheather the ship is withing range


    //to keep track in which scene meteor is spawn
    public SceneMode sceneMode;
    public float speed;//use this in menu scene

    public float maxRotateSpeed = 50;//the max rotation speed with which meteor rotate
    public float minRotateSpeed = 15;//the min rotation speed with which meteor rotate
    float rotateSpeed;//the value which we will get from above 2
    public float maxMoveSpeed = 4;//min movement speed of meteor
    public float minMoveSpeed = 6;//this value must be greater tha max value so that meteor move fast
    float moveSpeed;//the value which we will get from above 2
    private Vector3 direction;//directon in which meteor moves
    int i;
    private Transform cameraPos;//to keep track of camera

	// Use this for initialization
	void Start ()
    {
        //at start we get all the necesary values
        i = Random.Range(0, 2);
        rotateSpeed = Random.Range(minRotateSpeed, maxRotateSpeed);
        moveSpeed = Random.Range(minMoveSpeed, maxMoveSpeed);
        direction = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0.0f).normalized;
        cameraPos = Camera.main.gameObject.transform;

        //here we get the scene name and depending on that we decide the behaviour of meteor

        Scene sceneName = SceneManager.GetActiveScene();    
        if (sceneName.name == "MainMenu")
        {
            sceneMode = SceneMode.mainMenu;
        }
        else
        {
            sceneMode = SceneMode.gamePlay;
        }

    }

    // Update is called once per frame
    void Update ()
    {
        ModeSelect();

        if (GameController.instance.isGameOver == false && sceneMode == SceneMode.gamePlay)
        {
            MagneticEffect();
        }
    }

    //method to decide weather to rotate clockwise or anticlockwise
    void Movement()
    {
        if (i == 0)
        {
            transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * rotateSpeed);
        }
        else
        {
            transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * -rotateSpeed);
        }

        transform.position += direction * Time.deltaTime / moveSpeed;
    }

    //method to check weather the meteor is within the screen or out of it and deactivate it if its out
    void OutOfScreen()
    {
        //we get the camera gameobject
        cameraPos = Camera.main.gameObject.transform;
        //get the position of meteor
        float x = transform.position.x;
        float y = transform.position.y;
        //the orthographic size is half of height , to get width we multiply orthographic size with aspect
        //here we get the weidht and height of our screen , due to movement we keep track of 4 coordinates
        //of rectangle created by camera
        float maxX = cameraPos.position.x + Camera.main.orthographicSize * Camera.main.aspect;
        float minX = cameraPos.position.x - Camera.main.orthographicSize * Camera.main.aspect;
        float maxY = cameraPos.position.y + Camera.main.orthographicSize;
        float minY = cameraPos.position.y - Camera.main.orthographicSize;

        //deactivate
        if (x > maxX + 4 || x < minX - 4 || y > maxY + 4 || y < minY - 4)
        {
            gameObject.SetActive(false);
        }
    }

    //here we select the movement of meteor depending on scene
    void ModeSelect()
    {
        switch (sceneMode)
        {
            case SceneMode.mainMenu:
                transform.position += new Vector3(-1,0,0) * Time.deltaTime * speed;

                //for rotation
                if (i == 0)
                {
                    transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * rotateSpeed);
                }
                else
                {
                    transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * -rotateSpeed);
                }

                OutOfScreen();
                break;
            case SceneMode.gamePlay:
                Movement();
                OutOfScreen();
                break;
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
            Vector3 directionToShip = shipTransform.position - trans.position;
            rigidBD.AddForce(directionToShip * magnetDirection * Time.deltaTime, ForceMode2D.Force);          
        }
    }

}
