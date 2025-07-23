using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSytemRemover : MonoBehaviour {

	// Use this for initialization
	void Awake(){
		if (GameObject.FindObjectsOfType<EventSytemRemover>().Length > 1) {
			Debug.Log ("HAY MÄS DE UN EVENTSYSTEM, borrando el ultimo");
			DestroyImmediate (this.gameObject);
			return;
		}
	}
}
