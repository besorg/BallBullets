using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LenguajeSeleccion : MonoBehaviour {
	void Start () {
		Dropdown list = gameObject.GetComponent<Dropdown>();
		if (TraspasedVars.lenguajeJson.lenguaje == "Spanish") {
			list.captionText.text = "Español";
			list.value = 1;
		}
		else {
			list.captionText.text = "English";
			list.value = 0;
		}
	}

	void Update () {
		
	}

	public void CambiarValor(){
		Dropdown list = gameObject.GetComponent<Dropdown>();
		if(list.value==0)TraspasedVars.Lenguaje="English";
		else TraspasedVars.Lenguaje = "Spanish";

		TraspasedVars.SaveLenguaje ();

		TextTraslator.EstablecerTextos ();
		TextTraslatorConSufijos.EstablecerTextos ();

		if (TraspasedVars.Lenguaje == "Spanish")TraspasedVars.dificultad = new string[] {"SandBox","Facilucho","Fácil", "Normal", "Dificil", "Hardcore"};
		else TraspasedVars.dificultad = new string[] {"SandBox","Easier","Easy", "Normal", "Hard", "Hardcore"};
	}
}
