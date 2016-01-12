using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SimpleNumber : MonoBehaviour {
	public Slider slider;

	public void Update() {
		this.GetComponent<Text> ().text = "Best of " + ((int)slider.value + 1).ToString ();
	}
}
