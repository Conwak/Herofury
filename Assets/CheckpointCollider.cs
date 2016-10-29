using UnityEngine;
using System.Collections;

public class CheckpointCollider : MonoBehaviour {

    public Checkpoint checkpoint;
    bool collided;
    SpriteRenderer sprite;

	// Use this for initialization
	void Start () {
        collided = false;
        sprite = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !collided)
        {
            Debug.Log("collided");
            collided = true;
            checkpoint.SetCheckpoint(this.transform.position);
            sprite.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }
    }
}
