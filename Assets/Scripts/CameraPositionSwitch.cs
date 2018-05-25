using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CameraPositionSwitch : MonoBehaviour {

	[SerializeField] GameObject camera;
	[SerializeField] GameObject positionBottom;
	[SerializeField] GameObject positionTop;
	[SerializeField] GameObject menuPanel;

	[SerializeField] Camera leftEye;
	[SerializeField] Camera rightEye;

	// Test central eye -to be removed
	[SerializeField] Camera centralEye;
	public Light light;

	public GameObject centralAnchor;



	public bool activeWalk = false;
	public float referenceX;
	public float currentX;
	public int differenceX;

	public int speed = 0;


	public NavMeshAgent agentt;

	void Start() {
		//camera1.SetActive(true);
		menuPanel.SetActive (false);
		//agentt = GameObject.Find ("CameraAnchor").GetComponent<NavMeshAgent> (); 
		Debug.Log ("VR: you got the nav mesh agent" + agentt.name);
	}

	void Update() {
		//SwitchCamera();
		Move ();
		Translate ();
	}


	public void SwitchCamera(){
		Debug.Log ("VR: Clicked to switch");
		//if (OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad) || Input.GetKeyDown("space")){
			if (camera.transform.position != positionTop.transform.position) {
				// Deactivate navmesh agent 
				agentt.enabled = false;
				Debug.Log ("VR: the nav mesh agent is in status " + agentt.isActiveAndEnabled);

				// Update bottom anchor position
				positionBottom.transform.position = camera.transform.position;
				// Show the Canvas Menu
				menuPanel.SetActive (true);
				// Change camera position
				camera.transform.position = positionTop.transform.position;

				camera.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
				leftEye.orthographic = true;
				rightEye.orthographic = true;
				//test
				centralEye.orthographic = true;

				light.shadowStrength = 0;
				//camera.Find ("LeftEyeAnchor").orthographic = false;
				//camera.Find ("RightEyeAnchor").orthographic = false;
			} else if (camera.transform.position == positionTop.transform.position) { 
				activeWalk = false;
				menuPanel.SetActive (false);
				camera.transform.position = positionBottom.transform.position;
				// Re-activate the navmesh agent property
				agentt.enabled = true;
				Debug.Log ("VR: the nav mesh agent is in status " + agentt.isActiveAndEnabled);
				camera.transform.rotation = Quaternion.identity;

				// Test to change view type -> doesn't work outside unity editor mode
				leftEye.orthographic = false;
				rightEye.orthographic = false;
				//test
				centralEye.orthographic = false;

				light.shadowStrength = 1;
			}
		//}
	}
	// for simplicity we deal with reasonable cases, that is the player will start from rest position ~10-0-350 and raise up to ~290
	public void Move(){
		if ((OVRInput.GetDown (OVRInput.Button.PrimaryTouchpad) && !activeWalk) && camera.transform.position != positionTop.transform.position) {
			activeWalk = true;
			referenceX = OVRInput.GetLocalControllerRotation (OVRInput.Controller.RTrackedRemote).eulerAngles.x;
			if (referenceX < 91)
				referenceX +=  360.0f;
		} else if (OVRInput.GetDown (OVRInput.Button.PrimaryTouchpad) && activeWalk) {
			activeWalk = false;
			speed = 0;
		}
		if (activeWalk) {
			currentX = OVRInput.GetLocalControllerRotation (OVRInput.Controller.RTrackedRemote).eulerAngles.x;
			differenceX = (int)(referenceX - currentX);
			//speed = 0; Attention, need upper-lower boundaries because otherwise it will default to the next opt without upper boudary
			if (differenceX >= 70 && differenceX < 120) {
				if (speed != 12) {
					speed = 12;
					Debug.Log ("VR: Speed set to " + speed);
				}
			} else if (differenceX >= 50 && differenceX < 70) {
				if (speed != 6) {
					speed = 6;
					Debug.Log ("VR: Speed set to " + speed);
				}
			} else if (differenceX >= 30 && differenceX < 50) {
				if (speed != 3) {
					speed = 3;
					Debug.Log ("VR: Speed set to " + speed);
				}
			} else if (differenceX >= 0 && differenceX < 30) {
				if (speed != 1) {
					speed = 1;
					Debug.Log ("VR: Speed set to 1");
				}
			} else {
				speed = 0;
				Debug.Log ("VR: Default case Speed set to " + speed);
			}
		} else if (!activeWalk) {
			if (speed != 0) speed = 0;
		}
	}
	public void Translate(){
		Vector3 move = Vector3.zero;
		move += new Vector3(centralAnchor.transform.forward.x, 0,centralAnchor.transform.forward.z);
		move = move.normalized * Time.deltaTime * speed;
		camera.transform.Translate (move, Space.World);
	}
}
