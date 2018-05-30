using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class NonUIInteraction : MonoBehaviour {
    // TODO: Delete outText
	public UnityEngine.UI.Text outText;
	public NavMeshAgent agent;
	public CameraPositionSwitch cameraSwitcher;
	// SampleDistance is the max distance -- Sample within this distance from sourcePosition.
	public const float navMeshSampleDistance = 4f;
	// Bool to lock the selected object & move it
	public bool locked = false;

	protected Material oldHoverMat;
	public Material yellowMat;

	private void Awake() {
		agent.updateRotation = false;
	}
	void Start () {
		agent = this.GetComponent<NavMeshAgent> ();
		if (agent == null) {
			Debug.Log ("VR: No agent attached!");
		}
		Debug.Log ("VR: you got the nav mesh agent \"" + agent.name + "\"");
	}

    public void OnHoverEnter(Transform t) {
		if (outText != null && t.name != "Ground") {
            oldHoverMat = t.gameObject.GetComponent<Renderer>().material;
            t.gameObject.GetComponent<Renderer>().material = yellowMat;
            outText.text ="hovering over: " + t.gameObject.name;
			Debug.Log ("VR: You've hovered on " + t.gameObject.name);
        } 
    }

	public void OnHoverStay(Transform t){ // && t.name != "Ground" is a temporary fix in order not to drag the ground when we want to move an object
		if (OVRInput.GetDown (OVRInput.Button.PrimaryIndexTrigger) && t.name != "Ground") {
			locked = true;
			Debug.Log ("VR: select GetDown + locked value " + locked);
			if (t.name == "PlanimetryCube") {
				cameraSwitcher.SwitchCamera ();
				locked = false;
			}
			if (t.name == "BackMenuCube") {
				Debug.Log ("VR: Clicked on left option");
				SceneManager.LoadScene ("menu");
				locked = false;
			}
			//t.transform = new Vector3 ();
			//t.transform = new Vector3(,t.transform.y,)		
			// OVRInput.Get(OVRInput.Button.Back
		} else if (OVRInput.GetDown (OVRInput.Button.Back) && t.name != "Ground" && t.tag != "control") {
			
			Debug.Log ("VR: Dpad clicked > try to destroy " + t.name + " <parent is> " + t.gameObject);
			Destroy (t.gameObject);
			Debug.Log ("VR: should have been deleted =  " + t);
		}

	}

    public void OnHoverExit(Transform t) {
		if (outText != null && t.name != "Ground") {
            t.gameObject.GetComponent<Renderer>().material = oldHoverMat;
            outText.text = "end over: " + t.gameObject.name;
			locked = false;
        }
    }

	public void OnSelected(Transform t) {
        if (outText != null) {
            outText.text = "clicked on: " + t.gameObject.name;
			Debug.Log ("VR: OnSelected you've selected " + t.gameObject.name);
			locked = false;
        }
    }

	// This is not working
	/*
	public void OnGroundClick(BaseEventData data) {
		Debug.Log ("VR: OnGroundClick activated, data is " + data);
		OVRRayPointerEventData pData = (OVRRayPointerEventData)data;
		Vector3 destinationPosition = Vector3.zero;
		NavMeshHit hit;
		if (NavMesh.SamplePosition(pData.pointerCurrentRaycast.worldPosition, out hit, navMeshSampleDistance, NavMesh.AllAreas)) {
			destinationPosition = hit.position;
			Debug.Log ("VR: [IF] the current hit position is " + destinationPosition);
		}
		else {
			destinationPosition = pData.pointerCurrentRaycast.worldPosition;
			Debug.Log ("VR: [ELSE] the current hit position is " + destinationPosition);
		}

		agent.isStopped = true;
		agent.SetDestination(destinationPosition);
		agent.isStopped = false;
	}
	*/
}
