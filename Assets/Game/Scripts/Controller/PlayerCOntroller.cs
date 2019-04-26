using UnityEngine;
using System.Collections;

public class PlayerCOntroller : MonoBehaviour {

    public static PlayerCOntroller instance;
  
    [HideInInspector]public Transform player; //used by other scripts to get player position
    public float rotSpeed = 10;               //the speed with the ship rotate
    Vector3 rotate;  
    public float moveSpeed = 3;               //speed with which ship moves  
    [HideInInspector]         
    public bool isPlayer = false;             //keep track is game is on
    private int score;                        //ref to score 
    private int juice;                        // ref to juice 
    [SerializeField]
    private GameObject explosion;             // ref to explosion prefabs
    private SpriteRenderer sprite;            //ref to ship image 
    private AudioSource audioSource;          // ref to audio source attached to gameobject
    [SerializeField]
    private AudioClip sounds;               // for points 
    private int isGameOver;

    [HideInInspector]
    public ManageVariables vars;            //this is for editor 

    void OnEnable()
    {
        vars = Resources.Load("ManageVariablesContainer") as ManageVariables; //this is for editor 
    }

    void Awake()
    {
        //we make this instance so we dont need to give ref to other object
        if (instance == null)
            instance = this;
        //we get the player transform
        player = GetComponent<Transform>();
    }

	// Use this for initialization
	void Start ()
    {
        //we set basic settings at start
        score = 0;
        //at start we want out ship to static
        transform.Translate(Vector3.zero);
        sprite = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //when we 1st time tap the screen the game is on and we then call movement method
        if (Input.GetMouseButton(0))
        {
            isPlayer = true;
            Movement();
        }

        //if game is on then only we move the ship
        if (isPlayer == true && GameController.instance.isGameOver == false)
        {
            transform.Translate(Vector3.up * Time.deltaTime * moveSpeed);
        }

    }

    void Movement()
    {
        //we get the position on the screen where the touch happen
        Vector2 pos = Input.mousePosition;

        //if the touch is right side the we turn right and if its left side then we turn left
        if (pos.x > Screen.width / 2 && pos.x < Screen.width)
        {
            //turn right
            RightMovement();
        }
        else
        {
            LeftMovement();
        }
    }

    //here we add the angle to the ships existing angle to make it turn
    void LeftMovement()
    {
        rotate.z += (1 * rotSpeed);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotate.z));
    }

    void RightMovement()
    {
        rotate.z += (-1 * rotSpeed);
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotate.z));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Juice"))
        {
            audioSource.PlayOneShot(sounds);
            score++;
            GameController.instance.currentScore = score;
            GameController.instance.juice++;
            GameController.instance.Save();
            other.gameObject.SetActive(false);
        }

        if (other.CompareTag("Meteor"))
        {
            sprite.enabled = false;                            //here we disable ship image 
            transform.GetChild(0).gameObject.SetActive(false); //we deactivate the thrust
            GameObject boom = (GameObject)Instantiate(explosion, transform.position, Quaternion.identity); //we activate the explosion
            GameController.instance.isGameOver = true;
            ShowAds();
            gameObject.SetActive(false);
        }
    }

    void ShowAds()
    {
        GameController.instance.gamesPlayed++;

        if (GameController.instance.gamesPlayed >= vars.showInterstitialAfter)
        {
            GameController.instance.gamesPlayed = 0;
            //use any one of them
            //admob ads
#if AdmobDef
                    AdManager.instance.ShowInterstitial();
#endif
        }
    }
}
