using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OnClick : MonoBehaviour {

	public Menu_control controlador;
	public SpriteRenderer pongSR;
	public GameObject[] Señalador;
	public GameObject[] Pildoras;
	private int dificultad = 2;
	public GameObject C,texto;
	Sonido_control sonido_control;
	public SpriteRenderer[] HijosRenders;
	bool hold_click=false;
	public static TraspasedVars vars;
	public TextMesh texto_text;
	public static bool block_mouseenter=false;
	public GameObject fondo;

	void Start(){
		//if(SceneManager.GetActiveScene().name!="LevelCompleted")
		sonido_control = controlador.AudioControl.GetComponent<Sonido_control> ();
		vars = GameObject.Find ("Vars").GetComponent<TraspasedVars > ();
		if (name == "Boton erase") {
			//texto.GetComponent<TextMesh >().text=" ";
			//texto_text = texto.GetComponent<TextMesh > ();
			//C.SetActive (false);
		}
		if (name == "V") {
			if(vars.vars.vidasActual > 0 && vars.vars.puntajeActual > 0)	dificultad = vars.vars.dificultadActual;
			else dificultad = vars.vars.dificultadMenuPrincipal;
		}
	}
		

	void SetHijosColor(Color rgb){
		HijosRenders [0].color = rgb;
		HijosRenders [1].color = rgb;
		HijosRenders [2].color = rgb;
	}



	void OnMouseExit(){
		if (hold_click) {
			//Debug.Log ("Mouse exit");
			hold_click = false;
			if (controlador != null) {
				controlador.StopCoroutine ("OcultarTextoDescriptivo");
				controlador.StartCoroutine ("OcultarTextoDescriptivo");
			}
		}
	}

	void OnMouseEnter(){
		if (controlador.clickeables && controlador.cuadrado_presed==0 && !block_mouseenter) {
			if (name == "X") {
				if (vars.vars.vidasActual > 0) {
					if (TraspasedVars.Lenguaje == "Spanish")texto_text.text = "Continuar al siguiente nivel " + vars.vars.levelActual;
					else texto_text.text = "Continue to next level " + vars.vars.levelActual;
				}else{
					if (TraspasedVars.Lenguaje == "Spanish")texto_text.text = "Empezar partida en dificultad " + TraspasedVars.dificultad [vars.vars.dificultadMenuPrincipal];
					else texto_text.text = "Start new game at " + TraspasedVars.dificultad [vars.vars.dificultadMenuPrincipal] + " difficulty";
				}
				controlador.StopCoroutine ("OcultarTextoDescriptivo");
				controlador.StartCoroutine ("OcultarTextoDescriptivo");
			}
			if (name == "D") {
				if (TraspasedVars.Lenguaje == "Spanish")texto_text.text = "Menu de selección del color del pong";
				else texto_text.text = "Pong's color selection menu";
				controlador.StopCoroutine ("OcultarTextoDescriptivo");
				controlador.StartCoroutine ("OcultarTextoDescriptivo");
			}
			if (name == "O") {
				if (TraspasedVars.Lenguaje == "Spanish")texto_text.text = "Opciones";
				else texto_text.text = "Settings";
				controlador.StopCoroutine ("OcultarTextoDescriptivo");
				controlador.StartCoroutine ("OcultarTextoDescriptivo");
			}
			if (name == "V") {
				if (vars.vars.vidasActual > 0) {
					if (TraspasedVars.Lenguaje == "Spanish")texto_text.text = "Dificultad actual " + TraspasedVars.dificultad [vars.vars.dificultadActual];
					else texto_text.text = "Current difficulty " + TraspasedVars.dificultad [vars.vars.dificultadActual];
				} else {
					if (TraspasedVars.Lenguaje == "Spanish")texto_text.text = "Dificultad seleccionada " + TraspasedVars.dificultad [dificultad];
					else texto_text.text = "Difficulty selected " + TraspasedVars.dificultad [dificultad];
				}
				controlador.StopCoroutine ("OcultarTextoDescriptivo");
				controlador.StartCoroutine ("OcultarTextoDescriptivo");
			}
		}
	}

	void OnMouseDrag(){
		if (controlador.clickeables) {
			if (hold_click) {
				if (gameObject.name == "Boton erase") {
					//Debug.Log ("boton eliminar drag");
				}
				if (gameObject.name == "azul") {
					//sonido_control.Crear_Sonido (4, 0, GameObject.Find ("Vars").GetComponent<TraspasedVars> ().GetVolumen (), false);
					//Debug.Log ("setting color...");
					//Debug.Log ("es azul!");
					pongSR.color = new Color (pongSR.color.r, pongSR.color.g, (Input.mousePosition.y / Camera.main.pixelHeight));
					Señalador [0].transform.position = new Vector2 (6.282f, pongSR.color.b * 10);
					vars.vars.pong_color = pongSR.color;
				}

				if (gameObject.name == "rojo") {
					//sonido_control.Crear_Sonido (4, 0, GameObject.Find ("Vars").GetComponent<TraspasedVars> ().GetVolumen (), false);
					//Debug.Log ("setting color...");
					//Debug.Log ("es rojo!");
					pongSR.color = new Color ((Input.mousePosition.y / Camera.main.pixelHeight), pongSR.color.g, pongSR.color.b);
					Señalador [2].transform.position = new Vector2 (10.302f, pongSR.color.r * 10);
					vars.vars.pong_color = pongSR.color;
				}

				if (gameObject.name == "verde") {
					//sonido_control.Crear_Sonido (4, 0, GameObject.Find ("Vars").GetComponent<TraspasedVars> ().GetVolumen (), false);
					//Debug.Log ("setting color...");
					//Debug.Log ("es verde!");
					//Debug.Log (Camera.main.pixelHeight);
					pongSR.color = new Color (pongSR.color.r, (Input.mousePosition.y / Camera.main.pixelHeight), pongSR.color.b);
					Señalador [1].transform.position = new Vector2 (8.274f, pongSR.color.g * 10);
					vars.vars.pong_color = pongSR.color;
				}
				hold_click = true;
			}
		}
	}
	
	void OnMouseDown(){
		//Debug.Log ("Mouse down");
		if (controlador.clickeables) {
			hold_click = true;
			if (gameObject.name == "O") {
				vars.Establecer_inputText ();
				vars.ChangeName ();

			}
			if (gameObject.name == "azul" || gameObject.name == "rojo" || gameObject.name == "verde") {
				sonido_control.Crear_Sonido (4, 0, vars.GetVolumen (), false);
			}
		}
	}

	void OnMouseUp ()
	{
		if (hold_click) {
			//Debug.Log ("Mouse up");
			if (SceneManager.GetActiveScene ().name == "Ball Bullets") {
				//Debug.Log ("entrá acá");
				if (gameObject.name == "Boton_musica") {
					//Debug.Log ("Se está clickeando");
					SetHijosColor (vars.GetConversionDeVolumenAColor (vars.vars.musicaVol - .04f, true));
					vars.save ();
					//Debug.Log (GameObject.Find ("Vars").GetComponent<TraspasedVars>().vars.musicaVol);
					//GameObject.Find ("Vars").GetComponent<TraspasedVars> ().vars.musicaVol = (float)valorlocal / 5;//se guarda directo
					//Debug.Log (valor);
				}
			}

			if (SceneManager.GetActiveScene ().name == "Menu Principal") {
				if (controlador.clickeables) {
					controlador.activar_clickeables = true;

					if (gameObject.name == "Boton erase") {
						vars.vars.dificultadActual = vars.vars.dificultadMenuPrincipal;
						//vars.vars.dificultadMenuPrincipal = dificultad;
						vars.vars.puntajeActual = 0;
						vars.vars.vidasActual = 0;
						if(TraspasedVars.Lenguaje=="Spanish")texto.GetComponent<TextMesh> ().text = "Partida borrada";
						else texto.GetComponent<TextMesh> ().text = "Current game erased";
						//Debug.Log ("partida guardada luego de borrars");
						vars.save ();
						C.SetActive (false);
						controlador.StopCoroutine ("OcultarTextoDescriptivo");
						controlador.StartCoroutine ("OcultarTextoDescriptivo");
						block_mouseenter = false;
						vars.Establecer_inputText ();
						vars.ChangeName ();
					}

					if (gameObject.name == "V") {
						//usa la variable C como el boton para eliminar, y a texto como el gameobject que contiene el texto a mostrar
						sonido_control.Crear_Sonido (0, 0, vars.GetVolumen (), false);

						if (vars.vars.vidasActual <= 0 || vars.vars.puntajeActual <= 0) {
							dificultad++;//0 sandbox, 1 facilucho, 2 facil, 3 normal, 4 dificil, 5 hardcore
							if (dificultad > 5)
								dificultad = 0;
							vars.vars.dificultadMenuPrincipal = dificultad;
						}
						for (int i = 0; i < 6; i++) {
							if (dificultad >= i) {
								Pildoras [i].SetActive (true);
							} else
								Pildoras [i].SetActive (false);
						}

						controlador.StopCoroutine ("OcultarPildoras");
						controlador.StartCoroutine ("OcultarPildoras");
						controlador.StopCoroutine ("OcultarTextoDescriptivo");
						controlador.StartCoroutine ("OcultarTextoDescriptivo");
						//Debug.Log ("dificultad...");
						if (vars.vars.vidasActual > 0 && vars.vars.puntajeActual > 0) {
							block_mouseenter = true;
							dificultad = vars.vars.dificultadActual;
							C.SetActive (true);
							if(TraspasedVars.Lenguaje=="Spanish")texto_text.text = "Partida en curso, ¿Eliminar?";
							else texto_text.text = "Game in process, Erase it?";

						} else {
							dificultad = vars.vars.dificultadMenuPrincipal;
							if(TraspasedVars.Lenguaje=="Spanish")texto_text.text = "Dificultad: " + TraspasedVars.dificultad [dificultad];
							else texto_text.text = "Difficulty: " + TraspasedVars.dificultad [dificultad];
							C.SetActive (false);
							block_mouseenter = false;
							//Debug.Log ("Save file in progres");
							FindObjectOfType<TraspasedVars> ().save ();
						}
					}

					if (gameObject.name == "O") {
						sonido_control.Crear_Sonido (0, 0, vars.GetVolumen (), false);
						//Debug.Log ("opciones...");
						//Debug.Log ("Save file in progres");
						//FindObjectOfType<TraspasedVars> ().save ();
						//Application.Quit ();
						//vars.Establecer_inputText ();

						if (GameObject.Find ("CanvasOpciones").GetComponent<Canvas > ().enabled) {
							//GameObject.Find ("CanvasOpciones").GetComponent<Canvas > ().enabled = false;
							//FindObjectOfType<CanvasOpcionesScript >().AbrirCerrarMenuOpciones();
						} else {
							//GameObject.Find ("CanvasOpciones").GetComponent<Canvas > ().enabled = true;
							controlador.clickeables = false;
							controlador.activar_clickeables = false;
						}
						FindObjectOfType<CanvasOpcionesScript >().AbrirCerrarMenuOpciones();
					}

					if (gameObject.name == "D") {
						controlador.StopCoroutine ("OcultarTextoDescriptivo");
						C.SetActive (false);
						texto_text.text = " ";
						controlador.clickeables = false;
						sonido_control.Crear_Sonido (0, 0, vars.GetVolumen (), false);
						controlador.cuadrado_presed += 1;
						if (controlador.cuadrado_presed == 2)
							controlador.cuadrado_presed = 0;
						//Debug.Log ("cambiando...");
						block_mouseenter = false;
					}

					if (gameObject.name == "X") {
						sonido_control.Crear_Sonido (0, 0, vars.GetVolumen (), false);
						//Debug.Log ("Save file in progres");
						vars.save ();
						//Debug.Log ("cargando...");
						GameObject.Destroy (GameObject.Find ("colores"));
						GameObject.Destroy (GameObject.Find ("pong"));
						GameObject.Destroy (GameObject.Find ("New Sprite"));
						GameObject.Destroy (GameObject.Find ("V"));
						GameObject.Destroy (GameObject.Find ("D"));
						GameObject.Destroy (GameObject.Find ("O"));
						//falta una variable fondo para X
						fondo.SetActive (true);
						//fondo.GetComponent<SpriteRenderer> ().material.color = new Color (0, 0, 0, 1f);
						//DontDestroyOnLoad (fondo);
						//Sonido_control.MusicaController.CargarMusicaSiguiente();
						SceneManager.LoadScene ("Ball Bullets");
						//if(vars.vars.ocultar_ui)GameObject.Find ("C_CO").GetComponent<Controlador>().Interface.SetActive(true);
						GameObject.Destroy (GameObject.Find ("X"));
						block_mouseenter = false;
					}

					if (gameObject.name == "azul" || gameObject.name == "rojo" || gameObject.name == "verde") {
						//sonido_control.Crear_Sonido (4, 0, GameObject.Find ("Vars").GetComponent<TraspasedVars> ().GetVolumen (), false);
						//Debug.Log ("Save file in progres");
						FindObjectOfType<TraspasedVars> ().save ();
					}
				}
			}
		}
	}

	public void ActualizarColor(Color c){
		c.a = 1;
		pongSR.color = c;
		Señalador [0].transform.position = new Vector2 (Señalador[0].transform.position.x, pongSR.color.b * 10);
		Señalador [1].transform.position = new Vector2 (Señalador[1].transform.position.x, pongSR.color.g * 10);
		Señalador [2].transform.position = new Vector2 (Señalador[2].transform.position.x, pongSR.color.r * 10);
		vars.vars.pong_color = c;
	}
}
