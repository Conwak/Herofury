using UnityEngine;
using System.Collections;

public class HawkeyeController : MonoBehaviour {

    GameObject player;
    Character character;
    Animator anim;

    public GameObject arrow;

    public GameObject[] arrowArray;
    int counter;

	public AudioSource arrowSound;
	public AudioSource specialSound;

	// Use this for initialization
	void Start () {
        player = this.gameObject;
        character = GetComponent<Character>();
        anim = GetComponent<Animator>();
        counter = 0;
		arrowSound = GameObject.Find ("HawkeyeFire").GetComponent<AudioSource> ();
		specialSound = GameObject.Find ("HawkeyeTripleFire").GetComponent<AudioSource> ();
	}
	
    void FireArrow()
    {
		arrowSound.Play ();
        if (character.GetDirection() == 0) // right
        {
            Instantiate(arrow, new Vector2(player.transform.position.x + .6f, player.transform.position.y + .8f),
                new Quaternion(arrow.transform.rotation.x, arrow.transform.rotation.y, arrow.transform.rotation.z + 0.0f, arrow.transform.rotation.w));
        }
        else if (character.GetDirection() == 1) // left
        {
            Instantiate(arrow, new Vector2(player.transform.position.x - .6f, player.transform.position.y + .8f),
                new Quaternion(arrow.transform.rotation.x, arrow.transform.rotation.y, arrow.transform.rotation.z - 180.0f, arrow.transform.rotation.w));
        }
    }

    void Special()
    {
		specialSound.Play ();
        for (int i = 0; i < arrowArray.Length; i++)
        {
            if (character.GetDirection() == 0) // right
            {
                Instantiate(arrowArray[i], new Vector2(player.transform.position.x + .6f + i * 1.6f, player.transform.position.y + .8f),
                    new Quaternion(arrow.transform.rotation.x, arrow.transform.rotation.y, arrow.transform.rotation.z + 0.0f, arrow.transform.rotation.w));
            }
            else if (character.GetDirection() == 1) // left
            {
                Instantiate(arrowArray[i], new Vector2(player.transform.position.x - .6f + i * -1.6f, player.transform.position.y + .8f),
                    new Quaternion(arrow.transform.rotation.x, arrow.transform.rotation.y, arrow.transform.rotation.z -180.0f, arrow.transform.rotation.w));
            }
        }
    }

	// Update is called once per frame
	void Update () {
        counter++;
        if (Input.GetKeyDown(KeyCode.Space) && counter>=30)
        {
            counter = 0;
            anim.SetBool("isShooting", true);
            FireArrow();
        }
        else
        {
            anim.SetBool("isShooting", false);
        }

        if(character.getSpecial() >= 100 && Input.GetKeyDown(KeyCode.F))
        {
            character.setSpecial(0);
            anim.SetBool("isSpecialAbility", true);
            Special();
        }
        else
        {
            anim.SetBool("isSpecialAbility", false);
        }
	}
}
