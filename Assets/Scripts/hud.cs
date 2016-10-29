using UnityEngine;
using System.Collections;

public class hud : MonoBehaviour {


	public void ChangeLevel(string lvlName)
	{
		Application.LoadLevel (lvlName);
	}

	public void Quit()
	{
		Application.Quit ();
	}
}
