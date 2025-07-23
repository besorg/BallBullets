using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OcultarSinUIMensajeInicialScript : MonoBehaviour {
	Controlador controlador;

	void Start(){
		controlador = FindObjectOfType <Controlador> ();
		//Debug.Log ("CIERTO!");
	}

	void OnMouseDown(){
		Debug.Log ("CLICKEADO");
		//controlador.StopCoroutine ("Activar_controles");
		controlador._Activar_controles_time = .5f;
		//controlador.StartCoroutine("Activar_controles");
		//controlador.Activar_controles_defunq();
		//controlador.Activar_controles_funtion();
		controlador.SetTextoDescripcion (" ",1);
	}

	void OnMouseUp(){
		//gameObject.SetActive (false);
	}
}
