using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckedButtomControl : MonoBehaviour {
	public Sprite[] sprites;
	private TraspasedVars vars;
	public GameObject Boton_chequeable, Boton_dependiente;//Boton_dependiente si está desabilitado el BotonUITooltip ésta es la variable usada por BotonOcultarUI para acceder al otro botón
	public Image ocultar_uiBoton;
	bool Checked_value;//variable generica usada para los botones hijos de "BotonBarraDeEstados" para no tener que definir muchas variables... :v

	void Start(){
		vars = GameObject.Find ("Vars").GetComponent<TraspasedVars >();
		//sprite = GetComponent<SpriteRenderer> ();
		if(name=="BotonUI")Boton_dependiente=GameObject.Find("BotonUITooltip");
	}

	void Click(){
		Click (true);
	}

	public void Click(bool Cambiara=false){
		//Debug.Log ("Click");
		if(transform.IsChildOf(GameObject.Find("BotonShowBarraDeEstados").transform)){
			if(Checked_value){
				Checked_value=false;
				ocultar_uiBoton.sprite = sprites [0];
			}
			else {
				Checked_value=true;
				ocultar_uiBoton.sprite = sprites [1];
			}
			Debug.Log ("Valor = " + Checked_value);
		}

		if (name == "BotonUI") {
			if (Cambiara) {
				if (vars.vars.ocultar_ui) {
					vars.vars.ocultar_ui = false;
					//sprite.sprite = sprites[0];
					//Boton_chequeable.SetActive(false);
				} else {
					vars.vars.ocultar_ui = true;
					//sprite.sprite = sprites[1];
					//Boton_chequeable.SetActive(true);
				}
			}
			Boton_dependiente.SetActive (vars.vars.ocultar_ui);
			if (vars.vars.ocultar_ui) {
				ocultar_uiBoton.sprite = sprites [1];

			} else {
				ocultar_uiBoton.sprite = sprites [0];
			}
			//ocultar_uiBoton.enabled = vars.vars.ocultar_ui;
			//Boton_chequeable.GetComponent<Toggle>().isOn=vars.vars.ocultar_ui;
			//Boton_chequeable.SetActive (vars.vars.ocultar_ui);

		}

		if (name == "BotonShowBarraDeEstados") {
		}

		if (name == "BotonUITooltip") {
			//Debug.Log ("CLICKEADO");
				if (Cambiara) {
					if (vars.vars.ocultar_textoTutorial_UIoculta) {
						vars.vars.ocultar_textoTutorial_UIoculta = false;
						//sprite.sprite = sprites[0];
						//Boton_chequeable.SetActive(false);
					} else {
						vars.vars.ocultar_textoTutorial_UIoculta = true;
						//sprite.sprite = sprites[1];
						//Boton_chequeable.SetActive(true);
					}
				}
				if (vars.vars.ocultar_textoTutorial_UIoculta) {
					ocultar_uiBoton.sprite = sprites [1];
				} else {
					ocultar_uiBoton.sprite = sprites [0];
				}
				//ocultar_uiBoton.enabled = vars.vars.ocultar_ui;
				//Boton_chequeable.GetComponent<Toggle>().isOn=vars.vars.ocultar_ui;
				//Boton_chequeable.SetActive (vars.vars.ocultar_ui);
		}
	}
}
