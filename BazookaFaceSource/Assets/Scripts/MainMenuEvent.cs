using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class MainMenuEvent : MonoBehaviour {
	public GameObject[] MainOptions;
	public GameObject[] Option1;
	public GameObject[] Option2;
	private Globals global;
	void Start() {
		Globals[] globals = GameObject.FindObjectsOfType<Globals> ();
		for (int c = 1; c < globals.Length; c++) {
			Destroy(globals[c].gameObject);
		}
		global = globals [0];
	}

	public void BeginGame(string name) {
		global.MaxScore = ((int)GameObject.FindObjectOfType<Slider> ().value);
		Application.LoadLevel (name);
	}

	public void Quit() {
		Application.Quit ();
	}

	public void OpenMapOptions (int button) {
		bool valid = true;
		GameObject[] options = null;
		switch(button) {
		case 0:
			options = Option1;
			break;
		case 1:
			options = Option2;
			break;
		case 2:
				
			break;
		case 3:

			break;
		default:
			valid = false;
			break;
		}
		if (valid) {
			foreach (GameObject g in options) {
				g.SetActive(true);
			}
			foreach (GameObject g in MainOptions) {
				g.GetComponent<Selectable>().interactable = false;
			}
			GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(options[0]);
		}
	}

	void Update () {
		if (Input.GetAxis("Cancel") > 0) {
			foreach (GameObject g in MainOptions) {
				g.GetComponent<Selectable>().interactable = true;
			}
			GameObject.Find("EventSystem").GetComponent<EventSystem>().SetSelectedGameObject(MainOptions[0]);
			foreach (GameObject g in Option1) {
				//g.GetComponent<Selectable>().interactable = false;
				g.SetActive(false);
			}
			foreach (GameObject g in Option2) {
				//g.GetComponent<Selectable>().interactable = false;
				g.SetActive(false);
			}
		}
	}
}
