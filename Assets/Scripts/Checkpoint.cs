using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {

    public Vector2 checkpoint;
    SpriteRenderer spriteRender;
    public GameObject[] checkPointObjects;
    bool collided;
	public GameObject startpos;

	// Use this for initialization
	void Start () {
        spriteRender = GetComponent<SpriteRenderer>();
        checkPointObjects = GameObject.FindGameObjectsWithTag("Checkpoint");
        collided = false;
		checkpoint.x = startpos.transform.position.x;
		checkpoint.y = startpos.transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(checkpoint.x + " " + checkpoint.y);
        if(collided)
        {
            spriteRender.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
        }
	}

    public Vector2 getCheckpoint()
    {
        return checkpoint;
    }

    public void SetCheckpoint(Vector2 point)
    {
        checkpoint = point;
    }

    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    if(other.tag == "Player" && !collided)
    //    {
    //        collided = true;
    //        for (int i = 0; i < checkPointObjects.Length; i++)
    //        {
    //            checkpoint.x = checkPointObjects[i].transform.position.x;
    //            checkpoint.y = checkPointObjects[i].transform.position.y;
    //        }
    //    }
    //}
}
