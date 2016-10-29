using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public GameObject enemy;
    public GameObject bullet;
    public SpriteRenderer spriterender;
    public Character character;
    public float walkSpeed;
    public float shootSpeed;
    public Transform point1;
    public Transform point2;
    public int direction;
    public int fireCounter;

    int health;

    //Raycast 2d
    public RaycastHit2D hit2D;

	void Start () {
        enemy = this.gameObject;
        spriterender = GetComponent<SpriteRenderer>();
        walkSpeed = 0.05f;
        direction = 0;
        fireCounter = 0;
        shootSpeed = 10.0f;
        health = 3;
	}

    void Fire()
    {
        //character.rigidbody2D.AddForce(Vector2.right * 5.0f, ForceMode2D.Impulse);

        if (direction == 0) //right
        {
            //spawn and rotate the bullet
            Instantiate(bullet, new Vector2(enemy.transform.position.x + .6f, enemy.transform.position.y), 
                new Quaternion(bullet.transform.rotation.x, bullet.transform.rotation.y, bullet.transform.rotation.z + 180.0f, bullet.transform.rotation.w));
        }
        else if (direction == 1)//left
        {
            //spawn and rotate the bullet
            Instantiate(bullet, new Vector2(enemy.transform.position.x - .6f, enemy.transform.position.y), 
                new Quaternion(bullet.transform.rotation.x, bullet.transform.rotation.y, bullet.transform.rotation.z - 180.0f, bullet.transform.rotation.w));
        }
    }

    void FixedUpdate()
    {
    }

	void Update () {
        Physics2D.IgnoreLayerCollision(9, 9);
			if (enemy.transform.position.x <= point1.transform.position.x) {
				direction = 0;
			}
			if (enemy.transform.position.x >= point2.transform.position.x) {
				direction = 1;
			}


        if(direction==0) //right
        {
            //update the raycast and flip depending on direction of enemy
            Debug.DrawRay(new Vector2(enemy.transform.position.x + 0.5f, enemy.transform.position.y), enemy.transform.right * 5, Color.red, 0, true);
            hit2D = Physics2D.Raycast(new Vector2(enemy.transform.position.x + 0.5f, enemy.transform.position.y), enemy.transform.right, 1000);
            spriterender.flipX = false;
            enemy.transform.position = new Vector2(enemy.transform.position.x + walkSpeed, enemy.transform.position.y);
        }
        else if(direction==1)//left
        {
            //update the raycast and flip depending on direction of enemy
            Debug.DrawRay(new Vector2(enemy.transform.position.x - 0.5f, enemy.transform.position.y), -enemy.transform.right * 5, Color.red, 0, true);
            hit2D = Physics2D.Raycast(new Vector2(enemy.transform.position.x - 0.5f, enemy.transform.position.y), -enemy.transform.right, 1000);
            spriterender.flipX = true;
            enemy.transform.position = new Vector2(enemy.transform.position.x - walkSpeed, enemy.transform.position.y);
        }

        if (hit2D != null)
        {
            //Debug.Log(hit2D.collider.gameObject.tag);
            if (hit2D.collider.gameObject.tag == "Player")
            {
                //direction = -1;
                fireCounter++;
                if (fireCounter >= 25)
                {
                    Fire();
                    fireCounter = 0;
                }
            }
        }

        if(health <= 0)
        {
            Destroy(this.gameObject);
        }
	}

    public int getHealth()
    {
        return health;
    }

    public void takeDmg(int h)
    {
        health -= h;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Arrow")
        {
            takeDmg(1);
        }
        if(other.tag == "Fist")
        {
            Debug.Log("hit");
            takeDmg(1);
        }
        if(other.tag == "Slam")
        {
            takeDmg(3);
        }
    }
}
