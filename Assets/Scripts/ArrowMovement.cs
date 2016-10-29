using UnityEngine;
using System.Collections;

public class ArrowMovement : MonoBehaviour
{
    public Rigidbody2D rigidbody2D;
    public float bulletSpeed;
    public int destroyTimer;
    public int destroyDuration;

    // Use this for initialization
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        bulletSpeed = 10.0f;
        destroyTimer = 0;
        destroyDuration = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.rotation.z >= 0)
        {
            rigidbody2D.velocity = new Vector2(bulletSpeed, rigidbody2D.velocity.y);
            rigidbody2D.angularVelocity = 1.0f;
        }
        else if (transform.rotation.z <= 0)
        {
            rigidbody2D.velocity = new Vector2(-bulletSpeed, rigidbody2D.velocity.y);
            rigidbody2D.angularVelocity = 1.0f;
        }


        destroyTimer++;
        if (destroyTimer >= destroyDuration)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player" && other.tag != "Ladder")
        {
            Destroy(this.gameObject);
        }
    }

}
