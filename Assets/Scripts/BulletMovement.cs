using UnityEngine;
using System.Collections;

public class BulletMovement : MonoBehaviour {

    public Rigidbody2D rigidbody2D;
    public Enemy enemy;
    Animator anim;
    public float bulletSpeed;
    public int destroyTimer;
    public int destroyDuration;

	// Use this for initialization
	void Start () {
        rigidbody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        bulletSpeed = 20.0f;
        destroyTimer = 0;
        destroyDuration = 100;
	}
	
	// Update is called once per frame
	void Update () {

        if (transform.rotation.z >= 0)
        {
            rigidbody2D.velocity = new Vector2(bulletSpeed, rigidbody2D.velocity.y);
            rigidbody2D.angularVelocity = 1.0f;
        }
        else if(transform.rotation.z <= 0)
        {
            rigidbody2D.velocity = new Vector2(-bulletSpeed, rigidbody2D.velocity.y);
            rigidbody2D.angularVelocity = 1.0f;
        }


        destroyTimer++;
        if(destroyTimer>=destroyDuration)
        {
            Destroy(this.gameObject);
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //remove health
        }

        //play the bullet destroy anim here
        if (other.tag != "Enemy" && other.tag != "Untagged")
        {
            Debug.Log(other.tag);
            anim.SetBool("hasCollided", true);
            Destroy(this.gameObject);
        }
    }
    //handle collision here
    //ontriggerenter2d(collider2d other)
    //other.name == player will handle health
    //collide with anything instantiate an effect of bullet destroy
}
