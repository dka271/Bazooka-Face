using UnityEngine;
using System.Collections;

public class Container : MonoBehaviour {
	void OnTriggerExit2D(Collider2D collider) {
		Destroy (collider.gameObject);
	}
}
