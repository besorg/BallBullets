using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasOpcionesScript : MonoBehaviour {
	public GameObject sonidoBoton, musicaBoton;
	public GameObject ocultar_uiBoton, ocultar_uiBotonTooltip;
	public TraspasedVars vars;

	void Awake(){
		if (FindObjectsOfType<CanvasOpcionesScript> ().Length > 1) {
			DestroyImmediate (this.gameObject);
			return;
		}
		sonidoBoton = GameObject.Find ("SonidoBoton");
		musicaBoton = GameObject.Find ("MusicaBoton");
		GetComponent<Canvas > ().enabled = false;
		vars = FindObjectOfType <TraspasedVars>();

		if (!Application.isEditor) {
			GameObject.Find ("ForceButton").SetActive(false);
		}
	}

	public void AbrirCerrarMenuOpciones(){
		if (vars == null)
			vars = FindObjectOfType<TraspasedVars > ();

		vars.Establecer_inputText ();
		vars.ChangeName();
		if (GetComponent<Canvas > ().enabled) {
			GetComponent<Canvas > ().enabled = false;
		} else {
			//GameObject.Find ("CanvasOpciones").
			GetComponent<Canvas > ().enabled = true;
			sonidoBoton.GetComponent<SonidoAndMusicaControl>().SliderSet (vars.vars.soundVol);
			musicaBoton.GetComponent<SonidoAndMusicaControl>().SliderSet (vars.vars.musicaVol);
			//ocultar_uiBoton.enabled = vars.vars.ocultar_ui;
			/*if (vars.vars.ocultar_ui) {
				ocultar_uiBoton.GetComponent<CheckedButtomControl> ().ocultar_uiBoton.sprite = ocultar_uiBoton.GetComponent<CheckedButtomControl> ().sprites [1];
			} else {
				ocultar_uiBoton.GetComponent<CheckedButtomControl> ().ocultar_uiBoton.sprite = ocultar_uiBoton.GetComponent<CheckedButtomControl> ().sprites [0];
				}*/
			//ocultar_uiBoton.GetComponent<CheckedButtomControl> ().Click (false);
			//ocultar_uiBotonTooltip.GetComponent<CheckedButtomControl> ().Click (false);

		}
		//ocultar_uiBoton.GetComponent<CheckedButtomControl>().Boton_chequeable.SetActive (vars.vars.ocultar_ui);

		if (SceneManager.GetActiveScene ().name == "Menu Principal") {
			Menu_control controlador = GameObject.Find ("Menu inicial").GetComponent<Menu_control> ();
			controlador.clickeables = !GetComponent<Canvas > ().enabled;
			controlador.activar_clickeables = !GetComponent<Canvas > ().enabled;
		}
			
	}
}
