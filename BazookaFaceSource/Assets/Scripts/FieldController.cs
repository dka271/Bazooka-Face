using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FieldController : MonoBehaviour {
	public Sprite[] sprites;
	public Sprite[] arrows;
	public float Magnitude;
	public int owner;

	private GameObject arrow;
	private Vector2 forceDirection;

	void Start() {
		arrow = transform.parent.FindChild("Arrow").gameObject;
		SetOwner (owner);
		SetDirection (Vector2.zero);
	}

	void OnTriggerStay2D(Collider2D coll) {
		Rigidbody2D obj = coll.rigidbody2D;
		obj.AddForce (Magnitude * forceDirection);
	}
	
	public void SetOwner(int player) {
		owner = player;
		GetComponent<SpriteRenderer>().sprite = sprites[player-1];
		arrow.GetComponent<SpriteRenderer> ().sprite = arrows [player- 1];
	}

	public void SetDirection(Vector2 direction) {
		forceDirection = direction;
		if (direction == Vector2.zero) {
			arrow.SetActive(false);
		} else {
			arrow.SetActive (true);
			arrow.transform.rotation = new Quaternion (0, 0, Mathf.Sqrt((1-direction.x)/2), direction.y >= 0 ? Mathf.Sqrt((1+direction.x)/2) : -Mathf.Sqrt((1+direction.x)/2));
		}
	}

	public static void SetFieldStates(int player, Vector2 direction) {
		foreach (FieldController f in FindObjectsOfType<FieldController>()) {
			if (f.owner == player) {
				f.SetDirection(direction);
			}
		}
	}

	public void SetRandomOwner(int players) {
		SetOwner (Random.Range (1, players + 1));
	}
	public void SetRandomDirection() {
		switch (Random.Range (0, 5)) {
		case 0:
			SetDirection(Vector2.up);
			break;
		case 1:
			SetDirection (-Vector2.up);
			break;
		case 2:
			SetDirection (-Vector2.right);
			break;
		case 3:
			SetDirection(Vector2.right);
			break;
		}
	}
}
