using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpsScripts : MonoBehaviour {
	public static ControladorLevelCompletedScene controlador;
	public TraspasedVars Vars;
	public GameObject _pointer;
	public static int indexSeleccion=0;
	private int _id;



	void Start(){
		Vars = GameObject.Find("Vars").GetComponent<TraspasedVars >();
		controlador = Camera.main.GetComponent<ControladorLevelCompletedScene> ();
		_pointer = GameObject.Find ("Aumento sel");
		//gameObject.name = "PowerUp_" + _id;
		indexSeleccion = 0;
	}

	void OnMouseEnter(){
		//indexSeleccion = _id;
		//Debug.Log (_pointer.transform.position + " pointer position..." );
		_pointer.transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,_pointer.transform.position.z);
		//Debug.Log (new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,_pointer.transform.position.z) + " pointer future position..." );
		//Debug.Log (_pointer.name + " display the name..." );
	}



	void OnMouseDown(){
		if (Vars.vars.vidasActual > 0) {
			bool descontarPunto = false;
			if (name == "PowerUp0") {
				if (Vars.vars.aceleracionBonus < .3f) {
					Vars.vars.aceleracionBonus += .01f;
					descontarPunto = true;
				}
			} else if (name == "PowerUp1") {// velocidad máxima es 8, con el bonus aumenta a 20
				if (Vars.vars.velocidadMaximaBonus < 12) {
					Vars.vars.velocidadMaximaBonus += .5f;
					descontarPunto = true;
				}
			} else if (name == "PowerUp2") {// largo duración es 30 segundos + 30 segundos de bonus máximo
				if (Vars.vars.largoBonus < 30) {
					Vars.vars.largoBonus += 3;
					descontarPunto = true;
				}
			} else if (name == "PowerUp3") {
				if (Vars.vars.largoTamañoBonus < 1) {// largo scala es 3 + 1 del bonus de tamaño
					Vars.vars.largoTamañoBonus += .05f;
					descontarPunto = true;
				}
			} else if (name == "PowerUp4") { // a velocidad tope... :v duración bonus - 30 + 60;
				if (Vars.vars.velocidadAtopeBonus < 60) {
					Vars.vars.velocidadAtopeBonus += 5;
					descontarPunto = true;
				}
			} else if (name == "PowerUp5") {// 100 base + 100 bonus;
				if (Vars.vars.energyBonus < 100) {
					Vars.vars.energyBonus += 5;
					descontarPunto = true;
				}
			} else if (name == "PowerUp6") {// SP recuperados al chocar los bordes +10;
				if (Vars.vars.perforantesRecuperadasContraBordes < 10) {
					Vars.vars.perforantesRecuperadasContraBordes += 1;
					descontarPunto = true;
				}
			} else if (name == "PowerUp7") {
				Vars.vars.explosivasActual += 1;
				descontarPunto = true;
			} else if (name == "PowerUp8") {
				Vars.vars.dirigidasBonus += 2;
				descontarPunto = true;
			} else if (name == "PowerUp9") {
				if (Vars.vars.scoreBonus < 99999) {
					Vars.vars.scoreBonus += 50;
					descontarPunto = true;
				}
			} else if (name == "PowerUp10") {
				if (Vars.vars.dropeoBonus < (50 * Vars.vars.dificultadActual) + Vars.vars.levelActual + 51) {//base es 50, por eso se le agrega 51 que al empezar el nivel es 1, 51-1=50
					Vars.vars.dropeoBonus += 1;
					descontarPunto = true;
				}
			}
			if (descontarPunto) {
				Vars.vars.puntosLibres--;
				Vars.save ();
				controlador.Establecer_Texto ();
				if (Vars.vars.puntosLibres <= 0) {
					controlador.Reiniciar ();
				}
				_pointer.GetComponent<SpriteRenderer> ().color = Color.clear;
				StopCoroutine ("Reestablecer_Color");
				StartCoroutine ("Reestablecer_Color");
			}
		}
	}



	IEnumerator Reestablecer_Color(){
		yield return new WaitForSeconds (.5f);
		_pointer.GetComponent<SpriteRenderer> ().color = GameObject.Find ("Vars").GetComponent<TraspasedVars > ().vars.pong_color;
	}
}
