using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextTraslatorConSufijos : MonoBehaviour {
	public string textPrefijo, textoPrefijo;
	public string text, texto;//los texto a escribir en sus dos idiomas, inglés y español...
	public string textSufijo, textoSufijo;

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
		foreach (TextTraslatorConSufijos a in FindObjectsOfType<TextTraslatorConSufijos>())
			a.EstablecerTexto ();
	}

	public void EstablecerTexto(){
		if (TraspasedVars.Lenguaje == "Spanish") {
			textComponent.text = textoPrefijo + texto + textoSufijo;
			lenguajeActual = TraspasedVars.Lenguaje;
		} else {
			textComponent.text = textPrefijo + text + textPrefijo;
			lenguajeActual = TraspasedVars.Lenguaje;
		}
	}
}
