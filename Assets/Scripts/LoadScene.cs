using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour {
	// This is a test script, eventually remove it
	void Update(){
		if (Input.GetKeyDown ("space")) {
			LoadRolex ();
		}
	}
	public void LoadRolex() {
		Debug.Log ("VR: LoadRolex triggered");
		SceneManager.LoadScene ("rolex");
	}
	public void LoadFreeGarden() {
		Debug.Log ("VR: LoadRolex triggered");
		SceneManager.LoadScene ("plain");
	}
	public void onClickLeft() {
		Debug.Log ("VR: Clicked on left option");
		//SceneManager.LoadScene ("menu");
	}
}