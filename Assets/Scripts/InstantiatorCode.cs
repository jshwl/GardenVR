using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InstantiatorCode : MonoBehaviour {
	int i  = 1;
	public Transform tree;

	// public Object[] inventory;
	//public Dictionary<string, Object> inventory;
	private GameObject trees;
	private GameObject lastInstantiated;

	// test 25/04
	public Dictionary<string, GameObject> inventory = new Dictionary<string, GameObject>();
	public Dictionary<string, Sprite> images = new Dictionary<string, Sprite>();

	void Start(){
		GameObject[] models = Resources.LoadAll<GameObject>("Prefabs");
		//Sprite[] icons = Resources.LoadAll<Sprite>("Images");
		foreach(GameObject model in models)
		{
			inventory.Add(model.name, model);
			Debug.Log ("VR: " + inventory.Keys);
		}

		//inventory = Resources.LoadAll ("Prefabs");

		/*foreach (var t in inventory)
		{
			Debug.Log("ActionVR: " + t.name);
			Instantiate(t, new Vector3(3*Mathf.Pow(-i,2), 0, Mathf.Pow(-i,2)), Quaternion.identity);
			i++;
		}*/
		//Debug.Log ("Tried to instantiate trees");

	}

	void Update () {
		//if (OVRInput.GetDown (OVRInput.Button.PrimaryTouchpad) || Input.GetKeyDown ("i")) {
		//	Instant ("Oak_Tree");
			//GameObject cyl = GameObject.CreatePrimitive (PrimitiveType.Cylinder);
			//Instantiate(tree, new Vector3(i, 0, i), Quaternion.identity);
			//cyl.AddComponent<Rigidbody> ();
			//cyl.GetComponent<MeshRenderer>().material.SetColor("_Color",Color.red);
		//cyl.transform.position = new Vector3(i, i, i);}

	}
	public void Instant(string  x){
		//Instantiate(inventory.Where((GameObject obj) => obj.name == x).Single(), new Vector3(i, 0, 5), Quaternion.identity);
		Debug.Log ("VR: instantiated '" + x + "'");
		i++;
		//  TEST WITH DICTIONARY
		lastInstantiated = Instantiate(inventory[x], new Vector3(-i, 0, 2), Quaternion.identity);
		lastInstantiated.AddComponent<BoxCollider> ();

		//Debug.Log ("Action: Button clicked " + inventory [name]);
	}
}
