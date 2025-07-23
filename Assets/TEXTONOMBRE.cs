using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TEXTONOMBRE : MonoBehaviour {
	TraspasedVars vars;
	Text texto;

	void Start(){
		vars = GameObject.Find ("Vars").GetComponent<TraspasedVars>();
		texto = GetComponent<Text> ();
	}

	void Update () {
		texto.text = " " + vars.vars.Nombre + " - " + Time.frameCount;
	}
}
