using UnityEngine;
using System.Collections;

public class Globals : MonoBehaviour {
	public int MaxScore;

	void Start () {
		DontDestroyOnLoad (this);
	}
}
