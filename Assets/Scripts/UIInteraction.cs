using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIInteraction : MonoBehaviour {
    public UnityEngine.UI.Text outText;
	public InstantiatorCode inst;
	public string innerText;

	public void OnButtonClicked() {
        if (outText != null) {
            outText.text = "UI Button clicked";
        }
		innerText = EventSystem.current.currentSelectedGameObject.name;
		//Debug.Log ("Text retrieved method 1: " + this);
		//Debug.Log ("ActionVR: Text retrieved method 2: " + this.GetComponentsInChildren<Text>().text);
		Debug.Log ("VR: Element selected : " + innerText);
		inst.Instant (innerText);
		Debug.Log ("VR: Button pressed");
    }

    public void OnSliderChanged(float value) {
        if (outText != null) {
            outText.text = "UI Slider value: " + value;
        }
    }
}
