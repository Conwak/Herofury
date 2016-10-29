using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CharacterSelection : MonoBehaviour {

    public List<GameObject> characters;
    public GameObject[] findPlayers;
    Vector2 lastPos;

    //UI Stuff
    bool showPanel;
    public GameObject panel;

	void Start () 
	{
        showPanel = false;
        panel.SetActive(false);
	}

    void DestroyLastPlayer()
    {
        if (findPlayers != null)
            findPlayers = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < findPlayers.Length; i++)
            Destroy(findPlayers[i]);
    }

    public void SpawnPlayer(int buttonNum)
	{
        DestroyLastPlayer();
		for (int i = 0; i < findPlayers.Length; i++)
            setLastLocation(findPlayers[i].transform.position);
        Instantiate(characters[buttonNum], lastPos, transform.rotation);
        panel.SetActive(false);
        showPanel = false;
	}

    void setLastLocation(Vector2 pos)
    {
        lastPos = pos;
    }

	void Update () 
	{
	    if(Input.GetKeyDown(KeyCode.C))
        {
            showPanel = !showPanel;

            if (showPanel)
                panel.SetActive(true);
            else if (!showPanel)
                panel.SetActive(false);
        }
	}
}
