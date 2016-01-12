using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {	
	public float Acceleration;
	public float MaxSpeed;
	public int Player;
	public float Rotation;
	public float Reload;
	public GameObject BloodExplosion;
	public GameObject Projectile;

	private GameObject lastProjectile;
	private GameManager manager;
	private string[] axis;
	private	Transform shotSpawn;
	private float shootTime;

	void Start() {
		axis = new string[] {
			"HorizontalMove" + Player.ToString (),
			"VerticalMove" + Player.ToString (),
			"HorizontalGravity" + Player.ToString(),
			"VerticalGravity" + Player.ToString(),
			"Rotation" + Player.ToString(),
			"Shoot" + Player.ToString(),
			"AltHorizontalMove" + (Player).ToString(),
			"AltVerticalMove" + (Player).ToString(),
			"RotationX" + (Player).ToString(),
			"RotationY" + (Player).ToString()
		};
		manager = GameObject.FindObjectOfType<GameManager> ();
	}

	// Update is called once per frame
	void Update () {
		if (manager.paused) {
			return;
		}
		shootTime -= Time.deltaTime;
		if (lastProjectile != null && shootTime < Reload - 0.25f) {
			try{ 
				Physics2D.IgnoreCollision(lastProjectile.collider2D, collider2D, false);
			} catch (UnityException e) {

			}
		}
		if (Input.GetAxis (axis[9]) != 0 || Input.GetAxis(axis[8]) != 0) {
			rigidbody2D.rotation = (Mathf.Atan2(Input.GetAxis(axis[9]), Input.GetAxis(axis[8])))*Mathf.Rad2Deg;
		}
		Vector2 force = new Vector2 (Input.GetAxis (axis [6]), Input.GetAxis (axis [7]));
		if (Player < 3) {
			if (Input.GetAxis(axis[0]) > 0) {
				force += Vector2.right;
			}
			if (Input.GetAxis(axis[0]) < 0) {
				force -= Vector2.right;
			}
			if (Input.GetAxis(axis[1]) > 0) {
				force += Vector2.up;
			}
			if (Input.GetAxis(axis[1]) < 0) {
				force -= Vector2.up;
			}
			if (Input.GetAxis(axis[2]) > 0) {
				FieldController.SetFieldStates(Player, Vector2.right);
			}
			if (Input.GetAxis(axis[2]) < 0) {
				FieldController.SetFieldStates(Player, -Vector2.right);
			}
			if (Input.GetAxis(axis[3]) > 0) {
				FieldController.SetFieldStates(Player, Vector2.up);
			}
			if (Input.GetAxis(axis[3]) < 0) {
				FieldController.SetFieldStates(Player, -Vector2.up);
			}
			if (Input.GetAxis(axis[4]) > 0) {
				rigidbody2D.rotation += Rotation * Time.deltaTime;
			}
			if (Input.GetAxis(axis [4]) < 0) {
				rigidbody2D.rotation -= Rotation * Time.deltaTime;
			}
		}
		if (Input.GetAxis(axis[5]) > 0 && shootTime < 0) {
			lastProjectile = (GameObject)Instantiate(Projectile, transform.position, transform.rotation);
			audio.Play();
			Physics2D.IgnoreCollision(lastProjectile.rigidbody2D.collider2D, collider2D);
			shootTime = Reload;
		}
		if (rigidbody2D.velocity.sqrMagnitude > MaxSpeed * MaxSpeed) {
			rigidbody2D.AddForce((force.normalized * MaxSpeed - rigidbody2D.velocity).normalized * Acceleration * rigidbody2D.mass);
		} else {
			rigidbody2D.AddForce(force * Acceleration * rigidbody2D.mass);
		}
	}

	public void KillPlayer() {
		Destroy(Instantiate (BloodExplosion, this.transform.position, this.transform.rotation), 2);
		GameObject.FindObjectOfType<GameManager> ().PlayerKilled (Player);
		Destroy(this.gameObject);
	}

	public void Reset(Transform t) {
		transform.position = t.position;
		transform.rotation = t.rotation;
		rigidbody2D.velocity = Vector2.zero;
	}
}
