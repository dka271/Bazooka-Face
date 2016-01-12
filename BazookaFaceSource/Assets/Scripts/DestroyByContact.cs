using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

	public GameObject explosion;
	public GameObject playerExplosion;

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Boundary") {
			return;
		}
		Instantiate (explosion, transform.position, transform.rotation);
		if (other.tag == "Player1") {
			Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
		}
		Instantiate (explosion, transform.position, transform.rotation);
		if (other.tag == "Player2") {
			Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
		}

		Destroy (other.gameObject);
		Destroy (gameObject);
	}
}
