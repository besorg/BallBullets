using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class InterfaceScripts : MonoBehaviour {
	private TraspasedVars vars;
	private Controlador controlador;
	private Text text;
	void Start(){
		vars = FindObjectOfType<TraspasedVars> ();	
		controlador = FindObjectOfType<Controlador> ();
		text = GetComponent<Text> ();
	}

	void Update () {
		string texto = "  ";
		if (name == "HPText") {
			if (SceneManager.GetActiveScene ().name == "Menu Principal") {
				if (vars.vars.vidasActual == 0) {
					gameObject.GetComponent<Text> ().enabled = false;
					gameObject.transform.parent.gameObject.GetComponent<Image> ().enabled = false;
				}

				//if (vars.vars.vidasActual == 0 && vars.vars.puntajeActual == 0) {
					//gameObject.GetComponent<Text> ().enabled = false;
					//gameObject.transform.parent.gameObject.GetComponent<Image> ().enabled = false;
					//GameObject.Find ("HPPanel").SetActive (false);
				//}
				else {
					gameObject.GetComponent<Text> ().enabled = true;
					gameObject.transform.parent.gameObject.GetComponent<Image> ().enabled = true;
					texto = "x";
					if (vars.vars.vidasActual > 99)
						texto = "x99";
					else
						texto = "x" + vars.vars.vidasActual;
				}
			} else {
				texto = "x";
				if (vars.vars.vidasActual > 99)
					texto = "x99";
				else
					texto = "x" + vars.vars.vidasActual;
			}
		}
		
		if (name == "PuntajeText") {
			texto = "Score: ";
			if (vars.vars.puntajeActual < 999999999)
				texto = texto+vars.vars.puntajeActual;
			else
				texto = texto+"999999999";
		}
		if (name == "NivelText") {
			texto = "Nivel: ";
			if (vars.vars.levelActual < 10000)
				texto = texto+vars.vars.levelActual;
			else
				texto = texto+"10000";
		}
		if (name == "EnergiaText") {
			float valor = 100-(controlador._pongEnergyUsada * 100 / (vars.vars.energyBonus + 100));
			if (valor > 100)
				valor = 100;
			else if (valor < 0)
				valor = 0;
			texto = valor+"%";
		}
		if (name == "HoraText") {
			//Debug.Log(/*System.TimeSpan.FromMinutes(*/System.DateTime.Now.ToLongTimeString());
			texto = System.DateTime.Now.ToLongTimeString();
		}
		if (name == "Aguja") {
			transform.localEulerAngles = new Vector3 (0,0,(float)System.DateTime.Now.TimeOfDay.TotalMinutes*-.5f);
			//Debug.Log ("Minutos "+(float)System.DateTime.Now.TimeOfDay.TotalMinutes + " - valor "+((float)System.DateTime.Now.TimeOfDay.TotalMinutes/1440));
		}

		//establecer el texto
		if (texto != "  ")
			text.text = texto;
	}
}
