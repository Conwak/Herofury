using UnityEngine;
using System.Collections;

public class HulkController : MonoBehaviour {

    public Character character;
    public GameObject player;
    public GameObject punchCollider;
    public Animator anim;
    //punch
    public BoxCollider2D box;
    public CircleCollider2D circle;

    int counter;

	public AudioSource punch;

	// Use this for initialization
	void Start () {
        player = this.gameObject;
        character = GetComponent<Character>();
        anim = GetComponent<Animator>();
        circle.enabled = false;
        counter = 0;
		punch = GameObject.Find ("Shoot").GetComponent<AudioSource> ();
	}
	
    void Punch()
    {
		punch.Play ();
        if(character.GetDirection() == 0)
        {//punch right
            box.enabled = true;
            punchCollider.transform.position = new Vector2(player.transform.position.x + 0.61f, punchCollider.transform.position.y);
        }
        else if(character.GetDirection() == 1)
        {//punch left
            Debug.Log("left");
            box.enabled = true;
            punchCollider.transform.position = new Vector2(player.transform.position.x - 0.61f, punchCollider.transform.position.y);
        }
    }

	// Update is called once per frame
	void Update () {
        // Punching
        counter++;
        if (Input.GetKeyDown(KeyCode.Space) && counter>=35)
        {
			counter = 0;
            Debug.Log("punch");
            anim.SetBool("isPunching", true);
            Punch();
        }
        else
        {
            anim.SetBool("isPunching", false);
            box.enabled = false;
        }

        if (character.getSpecial() >= 100 && character.getIsGrounded() == false && Input.GetKey(KeyCode.F))
        {
            circle.enabled = true;
            anim.SetBool("isSmash", true);
            character.setSpecial(0);
        }
        else
        {
            circle.enabled = false;
            anim.SetBool("isSmash", false);
        }
	}
}
