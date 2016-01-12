using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Destroy (this.gameObject, 0.5f);
	}

	void OnTriggerEnter2D(Collider2D coll) {
		PlayerController player = coll.gameObject.GetComponent<PlayerController> ();
		if (player != null) {
			player.KillPlayer();
		} else {
			Destroy (coll.gameObject);
		}
	}

}
