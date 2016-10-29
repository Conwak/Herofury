using UnityEngine;
using System.Collections;

public class AbyssController : MonoBehaviour {

    GameObject player;
    Character character;
    Animator anim;
    SpriteRenderer sprite;
    public GameObject spell;

    bool activeColour;
    int counter;

	public AudioSource fireSound;
	public AudioSource specialSound;

	// Use this for initialization
	void Start () {
        player = this.gameObject;
        character = GetComponent<Character>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        activeColour = false;
        counter = 0;
		fireSound = GameObject.Find ("AbyssMindControl").GetComponent<AudioSource> ();
		specialSound = GameObject.Find ("SpecialMove").GetComponent<AudioSource> ();
	}

    void Spell()
    {
		fireSound.Play ();
        if (character.GetDirection() == 0) // right
        {
            Instantiate(spell, new Vector2(player.transform.position.x + .6f, player.transform.position.y + .8f),
                new Quaternion(spell.transform.rotation.x, spell.transform.rotation.y, spell.transform.rotation.z + 0.0f, spell.transform.rotation.w));
        }
        else if (character.GetDirection() == 1) // left
        {
            Instantiate(spell, new Vector2(player.transform.position.x - .6f, player.transform.position.y + .8f),
                new Quaternion(spell.transform.rotation.x, spell.transform.rotation.y, spell.transform.rotation.z - 180.0f, spell.transform.rotation.w));
        }
    }

	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("isShooting", true);
            Spell();
        }
        else
        {
            anim.SetBool("isShooting", false);
        }

        if(character.getSpecial() >= 100 && Input.GetKeyDown(KeyCode.F))
        {
            activeColour = true;
            Debug.Log(activeColour);
        }

        if(activeColour == true)
        {
			specialSound.Play ();

            if (sprite.color.a >= 0.2f)
            {
                sprite.color = new Color(1.0f, 1.0f, 1.0f, sprite.color.a - 0.02f);
            }
            player.tag = "Invisible";

            counter++;
            //Debug.Log("counter " + counter);
            if(counter >= 300)
            {
                activeColour = false;
                counter = 0;
            }
            character.setSpecial(0);
        }
        else if(activeColour == false)
        {
            sprite.color = new Color(1.0f, 1.0f, 1.0f, sprite.color.a + 0.01f);
            player.tag = "Player";
        }

	}
}
