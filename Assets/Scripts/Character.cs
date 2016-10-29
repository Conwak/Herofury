using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Character : MonoBehaviour {

    public GameObject player;
    //public GameObject punchCollider;
    public Rigidbody2D rigidbody2D;
    public Animator anim;
    public LayerMask groundMask;
    public LayerMask ladderMask;
    public SpriteRenderer spriteRender;
    public float walkingSpeed;
    public float jumpSpeed;
    public float climbSpeed;
    public int health;
    int specialAbility = 0;
    int specialCounter;
	int score = 0;
	int lives;

    public bool checkLadder;
    bool isGrounded;
    int direction;

    public Slider slider;
	public Slider healthSlider;
	public Text liveText;
	public Text scoreText;

	public GameObject pausePanel;

	bool isInSlime = false;

	public AudioSource jumpsound;
	public AudioSource playerhit;
	public AudioSource deathSound;
	public AudioSource blueCoin;
	public AudioSource coinSound;

    public Checkpoint checkpoint;
	bool isPaused;

	void Start () {
        player = this.gameObject;
        rigidbody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRender = GetComponent<SpriteRenderer>();
        walkingSpeed = 0.1f;
        climbSpeed = 0.1f;
        isGrounded = false;
        checkLadder = false;
        jumpSpeed = 12.0f;
        direction = 0; //facing right
        health = 100;
        checkpoint = GameObject.FindGameObjectWithTag("CheckpointOBJ").GetComponent<Checkpoint>();

        specialCounter = 0;
        slider = GameObject.FindGameObjectWithTag("SpecialSlider").GetComponent<Slider>();
		healthSlider = GameObject.FindGameObjectWithTag ("Health").GetComponent<Slider> ();
		liveText = GameObject.FindGameObjectWithTag ("Lives").GetComponent<Text> ();
		scoreText = GameObject.Find("Score").GetComponent<Text> ();
		pausePanel = GameObject.Find ("PauseScreen");
		lives = 3;
		jumpsound = GameObject.Find ("Jump").GetComponent<AudioSource> ();
		playerhit = GameObject.Find ("PlayerHit").GetComponent<AudioSource> ();
		deathSound = GameObject.Find ("HulkSmash").GetComponent<AudioSource> ();
		coinSound = GameObject.Find ("Coin").GetComponent<AudioSource> ();
		blueCoin = GameObject.Find ("BlueCoin").GetComponent<AudioSource> ();
		pausePanel.SetActive (false);
		isPaused = false;
	}
	
    void Spawn()
    {
        Debug.Log("checkpoint log " + checkpoint.getCheckpoint());
        player.transform.position = checkpoint.getCheckpoint();
		health = 100;
    }

	public int getScore()
	{
		return score;
	}

	public void getScore(int s)
	{
		score = s;
	}

    public int getSpecial()
    {
        return specialAbility;
    }

    public void setSpecial(int n)
    {
        specialAbility = n;
    }

    void FixedUpdate()
    {
        specialCounter++;
        if(specialCounter>=35)
        {
            if (specialAbility < 100)
            {
                Debug.Log("counting");
                specialAbility++;
                specialCounter = 0;
            }
            Debug.Log(specialAbility);
        }

        slider.value = specialAbility;
		healthSlider.value = health;
		liveText.text = "Lives: " + lives.ToString ();
		scoreText.text = score.ToString ();
	

        if(checkLadder)
        {
            rigidbody2D.isKinematic = true;
        }
        else
        {
            rigidbody2D.isKinematic = false;
        }

        if (direction == 0) //right
        {
            spriteRender.flipX = false;
            //punchCollider.transform.position = new Vector2(player.transform.position.x + 0.61f, punchCollider.transform.position.y);
        }
        else if (direction == 1) //left
        {
            spriteRender.flipX = true;
            //punchCollider.transform.position = new Vector2(player.transform.position.x - 0.37f, punchCollider.transform.position.y);
        }
    }

    public int GetDirection()
    {
        return direction;
    }

	public bool PauseGame()
	{
		return isPaused;
	}

    void KeyPress()
    {
        // Movement
		if (!isPaused) {
			if (Input.GetKey (KeyCode.A)) {
				direction = 1; //facing left
				player.transform.position = new Vector2 (player.transform.position.x - walkingSpeed, player.transform.position.y);
				anim.SetFloat ("WalkSpeed", walkingSpeed);
			} else if (Input.GetKey (KeyCode.D)) {
				direction = 0; //facing right
				player.transform.position = new Vector2 (player.transform.position.x + walkingSpeed, player.transform.position.y);
				anim.SetFloat ("WalkSpeed", walkingSpeed);
			} else {
				anim.SetFloat ("WalkSpeed", 0.0f);
			}
		}

        // Jumping
        if (Input.GetKeyDown(KeyCode.W) && isGrounded && !checkLadder)
        {
            rigidbody2D.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
			jumpsound.Play ();
        }
        else if(Input.GetKey(KeyCode.W) && checkLadder)
        {
            player.transform.position = new Vector2(player.transform.position.x, player.transform.position.y + climbSpeed);
        }
        else if(Input.GetKey(KeyCode.S) && checkLadder)
        {
            player.transform.position = new Vector2(player.transform.position.x, player.transform.position.y - climbSpeed);
        }

        if(!isGrounded)
        {
            anim.SetBool("isGrounded", false);
        }
        else
        {
            anim.SetBool("isGrounded", true);
        }

        if(health <= 0)
        {
            Spawn();
			deathSound.Play ();
			lives--;
        }
		if (lives <= 0) {
			Application.LoadLevel("GameOver");
		}

		if (isInSlime) {
			health -= 1;
		}

		if (Input.GetKeyDown (KeyCode.Escape)) {
			isPaused = !isPaused;
			if (isPaused) {
				Time.timeScale = 0;
				pausePanel.SetActive (true);
			} else if (!isPaused) {
				pausePanel.SetActive (false);
				Time.timeScale = 1;
			}
		}
    }

	void Update () {
		isGrounded = Physics2D.OverlapCircle(player.transform.position, 1, groundMask);
		checkLadder = Physics2D.OverlapCircle(player.transform.position, 0, ladderMask);
        KeyPress();
	}

    public bool getIsGrounded()
    {
        return isGrounded;
    }

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Bullet") 
		{
			health -= 10;
			playerhit.Play ();
			Debug.Log(health);
		}
		if (other.tag == "Slime") 
		{
			playerhit.Play ();
			isInSlime = true;
		}
		if (other.tag == "End") {
			Application.LoadLevel ("Winner");
		}
		if (other.tag == "Coin") {
			coinSound.Play ();
			score += 10;
			Destroy (other.gameObject);
		}
		if (other.tag == "Blue") {
			blueCoin.Play ();
			specialAbility += 30;
			Destroy (other.gameObject);
		}
	}
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag == "Slime") 
		{
			isInSlime = false;
		}
	}
}
