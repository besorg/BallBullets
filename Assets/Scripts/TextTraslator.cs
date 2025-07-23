using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextTraslator : MonoBehaviour {
	public string text, texto;//los texto a escribir en sus dos idiomas, inglés y español...

	Text textComponent;
	public string lenguajeActual = "";

	void Awake(){
		textComponent = gameObject.GetComponent<Text> ();
	}

	void Start () {
		EstablecerTexto ();
	}

	void Update () {
		if (TraspasedVars.Lenguaje != "Spanish" || TraspasedVars.Lenguaje != "English")
			return;
		if (lenguajeActual != TraspasedVars.Lenguaje)
			EstablecerTexto ();
	}

	public static void EstablecerTextos(){
		foreach (TextTraslator a in FindObjectsOfType<TextTraslator>())
			a.EstablecerTexto ();
	}

	public void EstablecerTexto(){
		if (TraspasedVars.Lenguaje == "Spanish") {
			textComponent.text = texto;
			lenguajeActual = TraspasedVars.Lenguaje;
		} else {
			textComponent.text = text;
			lenguajeActual = TraspasedVars.Lenguaje;
		}
	}
}
