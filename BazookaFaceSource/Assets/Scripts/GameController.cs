using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	//public GUIText restartText;
	//public GUIText gameOverText;
	private bool gameOver;

	void Start ()
	{
		gameOver = false;
		//restartText.text = "";
		//gameOverText.text = "";
	}
	
	void Update ()
	{
		if (gameOver)
		{
			if (Input.GetKeyDown (KeyCode.R))
			{
				Application.LoadLevel (Application.loadedLevel);
			}
		}
	}

	public void GameOver ()
	{
		//gameOverText.text = "Game Over!";
		//restartText.text = "Press 'R' for Restart";
		gameOver = true;
	}

}
