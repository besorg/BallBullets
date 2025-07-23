using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OneButtonControl : MonoBehaviour {
	Controlador controlador;
	Text texto;

	Touch[] ToquesFrameAnterior;

	public void SetTexto(string _texto){
		texto.text = _texto;
	}

	void Start() {
		texto = transform.Find ("OneButtonText").GetComponent<Text>();
		controlador = FindObjectOfType<Controlador> ();
		//if (controlador == null)
		//	Debug.Log ("NO SE TIENE ACCESO AL CONTROLADOR");
		ToquesFrameAnterior = Input.touches;
		/*if (!controlador.vars.ocultar_textoTutorial_UIoculta) {
			
		}
		else
			controlador.SetTextoDescripcion (" ", 1);*/
	}

	void Update(){
		if (controlador._playing&&!controlador._pause) {
			string s = "INPUTS\n";
			for (int i = 0; i < Input.touchCount; i++) {
				s = s + " Touch[" + i + "] = " + Input.touches [i].rawPosition + " - " + Input.touches [i].position + "\n";
			}
			s = s + "\nVALOR mouse pos: " + Input.mousePosition;
			if (Input.touchCount > 0)
				s = s + "\nVALOR lasttouch pos: " + Input.GetTouch (Input.touchCount - 1);
			//texto.text = s;

			bool[] ImaginaryButtonPress; //= new bool[2] {false, false};
			ImaginaryButtonPress = new bool[] { false, false };
			foreach (Touch a in Input.touches) {
				if (a.rawPosition.y < UnityEngine.Screen.height * .25f) {
					if (a.position.x < UnityEngine.Screen.width / 2) {
						controlador._pong_dir = -1;
						ImaginaryButtonPress [0] = true;
					} else {
						controlador._pong_dir = 1;
						ImaginaryButtonPress [1] = true;
					}
				} else {//si el click es en la parte superior de la pantalla para crear o no el efecto de succion
					//establece si se está succionando o no...
					if (a.position.x - a.rawPosition.x < -UnityEngine.Screen.height * .15f) {
						controlador._succion = true;
					}
				}
			}
			if (ImaginaryButtonPress [0] && ImaginaryButtonPress [1]) {
				controlador._pong_dir = 0;
			}
			if (!ImaginaryButtonPress [0] && !ImaginaryButtonPress [1]) {
				controlador._pong_dir = 0;
			}

			if (ToquesFrameAnterior.Length > Input.touches.Length) {
				//texto.text = "Algo paso que hay menos toques";
				foreach (Touch a in ToquesFrameAnterior) {
					bool _noEsta = true;
					foreach (Touch b in Input.touches) {
						if (a.rawPosition == b.rawPosition)
							_noEsta = false;
					}

					if (_noEsta) {
						//ESTE TOQUE LLAMADO "a" no se encuentra, ¿Qué hacemos ahora?
						if (a.rawPosition.y < UnityEngine.Screen.height * .25f) {
							/*if (a.rawPosition < UnityEngine.Screen.width / 2) {
							controlador._pong_dir = -1;
						} else {
							controlador._pong_dir = 1;
						}*/
						} else {
							if (a.position.x - a.rawPosition.x > UnityEngine.Screen.height * .15f) {
								controlador.Crear_Bola_Hacia (0, new Vector2 ((a.position.x / UnityEngine.Screen.width) - .5f, 1));//a.position.y/UnityEngine.Screen.height));
							} else if (a.position.x - a.rawPosition.x < -UnityEngine.Screen.height * .15f) {
								controlador._succion = false;
							} else if (a.position.y - a.rawPosition.y > UnityEngine.Screen.height * .15f) {
								controlador.Crear_Bola_Hacia (2, new Vector2 ((a.position.x / UnityEngine.Screen.width) - .5f, 1));//a.position.y/UnityEngine.Screen.height));
							} else if (a.position.y - a.rawPosition.y < -UnityEngine.Screen.height * .15f) {
								controlador.Crear_Bola_Hacia (1, new Vector2 ((a.position.x / UnityEngine.Screen.width) - .5f, 1));//a.position.y/UnityEngine.Screen.height));
							} else {
								controlador.Crear_Bola_Hacia (0, new Vector2 ((a.position.x / UnityEngine.Screen.width) - .5f, 1));
							}
						}
					}
				}
			}

			ToquesFrameAnterior = Input.touches;
		}
	}

	 
}
