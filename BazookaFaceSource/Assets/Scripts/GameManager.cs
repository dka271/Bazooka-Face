using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	public float randomizeOwnerTime;
	public float randomizeDirectionTime;
	public FieldController[] randomFields;
	public int scoreLimit, winner;
	public int Players;
	public Texture Pause;
	public bool paused;
	public bool gameOver;
	public float Respawn;
	public GameObject[] playerObjects;
	public Texture ScoreBoard;

	private float respawnTimer;
	private float counterOwner;
	private float counterDirection;
	private float start;
	private int[] playerScore = new int[4];
	private int playersLive;

	void Start() {
		winner = 0;
		Reset ();
		Globals global = GameObject.FindObjectOfType<Globals> ();
		if (global != null) {
			scoreLimit = global.MaxScore;
		}
	}

	public void Reset() {
		foreach (ShotScript s in FindObjectsOfType<ShotScript>()) {
			Destroy(s.gameObject);
		}
		foreach (PlayerController p in FindObjectsOfType <PlayerController>()) {
			Destroy(p.gameObject);
		}
		start = 2;
		playersLive = (1 << Players) - 1;
		transform.parent.position = new Vector3 (transform.parent.position.x, transform.parent.position.y, -2);
		paused = true;
	}

	void OnGUI() {
		if (paused && start < 0) {
			print("Paused");
			GUI.DrawTexture(camera.pixelRect, Pause);
			if (GUI.Button(new Rect(0.4f * camera.pixelWidth, 0.8f * camera.pixelHeight,0.2f * camera.pixelWidth, 0.1f * camera.pixelHeight), "Quit")) {
				//Do Something
			}		
		}
	}

	void Update() {
		if (start > 0) {
			if (start < 1.5f) {
				Transform temp;
				if (GameObject.Find("Player1(Clone)") == null) {
					temp = GameObject.Find("P1Spawn").transform;
					Instantiate(playerObjects[0], temp.position, temp.rotation);
				} else {
					GameObject.Find("Player1(Clone)").GetComponent<PlayerController>().Reset(GameObject.Find ("P1Spawn").transform);
				}
				if (GameObject.Find("Player2(Clone)") == null) {
					temp = GameObject.Find ("P2Spawn").transform;
					Instantiate (playerObjects [1], temp.position, temp.rotation);
				} else {
					GameObject.Find("Player2(Clone)").GetComponent<PlayerController>().Reset(GameObject.Find ("P2Spawn").transform);
				}
				if (Players > 2) {
					if (GameObject.Find("Player3(Clone)") == null) {
						temp = GameObject.Find ("P3Spawn").transform;
						Instantiate(playerObjects[2], temp.position, temp.rotation);
					} else {
						GameObject.Find("Player3(Clone)").GetComponent<PlayerController>().Reset(GameObject.Find ("P3Spawn").transform);
					}
				}
				if (Players > 3) {
					if (GameObject.Find("Player4(Clone)") == null) {
						temp = GameObject.Find ("P4Spawn").transform;
						Instantiate(playerObjects[3], temp.position, temp.rotation);
					} else {
						GameObject.Find("Player4(Clone)").GetComponent<PlayerController>().Reset(GameObject.Find ("P4Spawn").transform);
					}
				}
			}
			start -= Time.deltaTime;
			transform.FindChild ("Logo").transform.localScale = new Vector3 (2, 2.5f, 1) * (start + 1);
			if (start <= 0) {
				transform.parent.position = new Vector3(transform.parent.position.x, transform.parent.position.y, 20);
				transform.FindChild ("Logo").transform.localScale = new Vector3 (2, 2.5f, 1);
				paused = false;
			}
		}
		if (respawnTimer > 0) {
			respawnTimer -= Time.deltaTime;
			if (respawnTimer <= 0) {
				Reset();
				for (int c = 1; c <= 4; c++) {
					FieldController.SetFieldStates(c, Vector2.zero);
				}
			}
		}
		if (randomizeOwnerTime > 0) {
			counterOwner += Time.deltaTime;
			if (counterOwner > randomizeOwnerTime) {
				counterOwner = 0;
				foreach (FieldController f in randomFields) {
					f.SetRandomOwner(Players);
				}
			}
		}
		if (randomizeDirectionTime > 0) {
			counterDirection += Time.deltaTime;
			if (counterDirection > randomizeDirectionTime) {
				counterDirection = 0;
				int rand = Random.Range(0, 4);
				FieldController.SetFieldStates(5, new Vector2((rand > 1 ? 2 * rand - 5 : 0), (rand < 2 ? 2 * rand - 1 : 0)));
			}
		}
		if (gameOver)
		{
			if (Input.GetKeyDown (KeyCode.R))
			{
				Application.LoadLevel ("MainMenu");
			}
		}
		if (Input.GetKeyDown(KeyCode.Escape)) {
			if (Time.timeScale == 0) {
				Time.timeScale = 1;
				paused = false;
			} else {
				Time.timeScale = 0;
				paused = true;
			}
		}
	}

	public void PlayerKilled(int player) {
		playersLive -= 1 << (player - 1);
		int n = -1;
		switch(playersLive) {
		case 1:
			n = 0;
			break;
		case 2:
			n = 1;
			break;
		case 4:
			n = 2;
			break;
		case 8:
			n = 3;
			break;
		}
		if (n != -1) {
			playerScore[n]++;
			GameObject.Find("P"+(n+1).ToString()).GetComponent<Text>().text = playerScore[n].ToString();
			if (playerScore[n] > scoreLimit) {
				GameObject.Find("GameOver").GetComponent<Text>().text = "Player "+(n+1).ToString()+" Wins\nPress 'R' To Return to Menu";
				Color color = Color.black;
				switch (n) {
				case 0:
					color = Color.red;
					break;
				case 1:
					color =Color.blue;
					break;
				case 2: 
					color = Color.green;
					break;
				case 3: color = Color.yellow;
					break;
				}
				GameObject.Find("GameOver").GetComponent<Text>().color = color;
				audio.Play();
				gameOver = true;
				winner = n + 1;
			} else {
				respawnTimer = Respawn;
			}
		}
	}
}
