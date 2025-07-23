using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClick_control : MonoBehaviour {
	GameObject G;
	Controlador c_controlador;
	Creador_de_niveles c_niveles;
	public Vector3 tamañoOriginal=Vector3.one, tamañoClickeado=Vector3.one;

	void Start(){
		G = GameObject.Find ("C_GO");
		c_controlador = G.GetComponent<Controlador> ();
		c_niveles = G.GetComponent<Creador_de_niveles> ();
	}

	void Update(){
		if (gameObject.name == "boton bola" || gameObject.name == "boton bola2") {
			if (c_controlador.vars.vidasActual > 0) {
				gameObject.GetComponent<SpriteRenderer> ().enabled = true;
				transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().enabled = true;
			} else {
				gameObject.GetComponent<SpriteRenderer> ().enabled = false;
				transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().enabled = false;
			}
		} else if (gameObject.name == "boton dirigidas") {
			if (c_controlador.vars.perseguidorasActual > 0) {
				gameObject.GetComponent<SpriteRenderer> ().enabled = true;
				transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().enabled = true;
			} else {
				gameObject.GetComponent<SpriteRenderer> ().enabled = false;
				transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().enabled = false;
			}
		} else if (gameObject.name == "boton XP") {
			if (c_controlador.vars.explosivasActual > 0) {
				gameObject.GetComponent<SpriteRenderer> ().enabled = true;
				transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().enabled = true;
			} else {
				gameObject.GetComponent<SpriteRenderer> ().enabled = false;
				transform.GetChild (0).gameObject.GetComponent<SpriteRenderer> ().enabled = false;
			}
		}
	}


	void OnMouseDown(){
		if (c_controlador._playing) {
			if (gameObject.name == "flecha derecha") {
				c_controlador._pong_dir = 1;
				c_controlador._click_direccion_press = true;
			}else if (gameObject.name == "flecha izquierda"){
				c_controlador._pong_dir = -1;
			c_controlador._click_direccion_press = true;
			}else if (gameObject.name == "boton bola") {
				c_controlador.Crear_Bola_Hacia(0, new Vector2(-.5f,0));
			}else if (gameObject.name == "boton bola2") {
				c_controlador.Crear_Bola_Hacia(0, new Vector2(.5f,0));
			} else if (gameObject.name == "boton succion") {
				c_controlador._succion=true;//variable de succión al colisionar la bola
				c_controlador._pong.GetComponent<PongController> ().PS_succion.SetActive(true);
			}else if (gameObject.name == "boton dirigidas") {c_controlador.Crear_Bola(1);
			}else if (gameObject.name == "boton XP") {c_controlador.Crear_Bola(2);
			}

			gameObject.transform.localScale = new Vector3 (1, 1, 1);
			gameObject.transform.localScale = tamañoClickeado;
		}
	}

	void OnMouseUp(){
		if (c_controlador._playing) {


			if (gameObject.name == "flecha derecha") {
				c_controlador._pong_dir = 0;
				c_controlador._click_direccion_press = false;
			}else if (gameObject.name == "flecha izquierda"){
				c_controlador._pong_dir = 0;
				c_controlador._click_direccion_press = false;
			}

			//c_controlador._pong_dir = 0;
			gameObject.transform.localScale = tamañoOriginal;

			if (gameObject.name == "boton succion") {
				c_controlador._succion=false;//variable de succión al colisionar la bola
				c_controlador._pong.GetComponent<PongController> ().PS_succion.SetActive (false);
			}
		}
	}
}
