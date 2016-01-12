using UnityEngine;
using System.Collections;

public class ShotScript : MonoBehaviour {
	public GameObject Explosion;
	public int damage = 1;
	public float Velocity;

	// Use this for initialization
	void Start () {

		//Destroy (gameObject, 5);
		float x = Velocity * Mathf.Cos(rigidbody2D.rotation * Mathf.PI / 180);
		float y = Velocity * Mathf.Sin(rigidbody2D.rotation * Mathf.PI / 180);
		
		rigidbody2D.velocity = new Vector2 (x, y);
	}

	void Update () {
		rigidbody2D.rotation = Mathf.Atan2 (rigidbody2D.velocity.y, rigidbody2D.velocity.x) * Mathf.Rad2Deg;
	}

	void OnCollisionEnter2D (Collision2D collision) {
		PlayerController player = collision.collider.gameObject.GetComponent<PlayerController> ();
		if (player != null) {
			player.KillPlayer();
		}
		Destroy (this.gameObject);
	}
	void OnDestroy() {
		Instantiate (Explosion, transform.position, transform.rotation);
	}
}
