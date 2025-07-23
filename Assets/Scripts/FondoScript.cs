using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FondoScript : MonoBehaviour {
	public Menu_control controlador;
	public GameObject fondo;
	int puntos = 0;
	string s="Cargando...",textoConcatenado="";
	int intervalo = 10;
	int delay = 60;
	bool activado=false;
	public bool bloquear_click;

	public TextMesh textolocal;
	public bool escena_secundaria;
	public GameObject Controlador_GameObject;
	public GameObject Boton_Musica;
	SaveData vars;

	void Awake(){
		if (GameObject.FindGameObjectsWithTag ("fondo").Length>1) {
			DestroyImmediate (this.gameObject);
			return;
		}
		//DontDestroyOnLoad (fondo);
		delay = 60;
		transform.parent.GetComponent<SpriteRenderer>().enabled=false;
		bloquear_click = true;//bloquea en la scene Ball Bullets no en el menu principal
		textolocal = GetComponent<TextMesh> ();
	}

	void Start(){
		vars = GameObject.Find ("Vars").GetComponent<TraspasedVars>().vars;
		DontDestroyOnLoad (GameObject.Find("OpcionesMenu"));
	}

	public void ResetCounter(){
		textolocal.text = s+textoConcatenado;
		puntos = s.Length;
		intervalo = 25;
	}



	public void SetText(string t){
		s = t;
	}

	public void SetTextConcatenado(string t){
		textoConcatenado = t;
	}

	void Update () {
		if (!escena_secundaria) {
			if (SceneManager.GetActiveScene ().name == "Menu Principal") {
				if (!activado) {
					if (delay > 0)
						delay--;
					fondo.GetComponent<SpriteRenderer>()/*GetComponent<MeshRenderer> ()*/.material.color = new Color (0, 0, 0, (float)delay / 60f);
					if (delay <= 0) {
						//this.gameObject.SetActive (true);
						fondo.GetComponent<SpriteRenderer> ().enabled = true;
						fondo.GetComponent<SpriteRenderer> ().material.color = new Color (0, 0, 0, 1f);
						intervalo = 10;
						fondo.SetActive (false);
						activado = true;
						SetTextConcatenado("\n\n");
						controlador.clickeables = true;
					}
				}
			}
			if (SceneManager.GetActiveScene ().name == "Ball Bullets") {
				if (puntos >= s.Length) {
					Controlador _controlador;
					//GameObject.Find ("C_GO").GetComponent<Controlador> ()._playing = true;
					Controlador_GameObject = GameObject.Find ("C_GO");
					_controlador = Controlador_GameObject.GetComponent<Controlador> ();
					if (vars.ocultar_ui) {
						if (!vars.ocultar_textoTutorial_UIoculta) {
							_controlador.OneClickOcultadorDelMensajeInicial.SetActive (true);
							_controlador.OneClickCanvasGO.SetActive (true);
						}
					} else {
						//_controlador.Activar_controles_defunq();
						//_controlador.Activar_controles_funtion();
					}
					GameObject.Find ("C_GO").GetComponent<Controlador> ().fondo = GameObject.Find ("Fondo");
					GameObject.Find ("C_GO").GetComponent<Controlador> ().fondo_textMesh = GetComponent<TextMesh> ();
					GameObject.Find ("C_GO").GetComponent<Controlador> ().fondo_textoGO = this.gameObject;
					GameObject.Find ("C_GO").GetComponent<Controlador> ().fondo_script = this;
					//Controlador_GameObject.GetComponent<Controlador> ().Musica_boton = GameObject.Find ("Boton_musica");
					GameObject.Find ("C_GO").GetComponent<Controlador> ().escena_cargada = true;
					puntos = 0;
					intervalo = 10;
					s = "Pausado...";
					textoConcatenado = "\n\n\n\n";
					gameObject.GetComponent<TextMesh> ().text = s;
					escena_secundaria = true;
					fondo.GetComponent<SpriteRenderer> ().material.color = new Color (0,0,0,.5f);
					Boton_Musica.SetActive (true);
					Color musicaLevel = GameObject.Find("Vars").GetComponent<TraspasedVars>().GetConversionDeVolumenAColor ((float)(vars.musicaVol), false);

					Boton_Musica.GetComponent<OnClick> ().HijosRenders [0].color=musicaLevel;
					Boton_Musica.GetComponent<OnClick> ().HijosRenders [1].color=musicaLevel;
					Boton_Musica.GetComponent<OnClick> ().HijosRenders [2].color=musicaLevel;
					//if (!vars.ocultar_ui)
						_controlador.androidControls.SetActive (true);
					Destroy (controlador);
					GameObject.Find ("Fondo").SetActive (false);
				}
			}

			if (activado) {
				intervalo--;
				if (intervalo <= 0) {
					intervalo = 10;
					string ss = s.Substring (0, puntos);
					gameObject.GetComponent<TextMesh> ().text = ss;
					puntos++;
					if (puntos > s.Length)
						puntos = 0;
				}
			}
		} else {
			if (Controlador_GameObject != null && Controlador_GameObject.GetComponent<Controlador> ()._pause) {
				//se ejecuta en la segunda escena para poner pausa y demás
				intervalo--;
				if (intervalo <= 0) {
					intervalo = 10;
					string ss = s.Substring (0, puntos)+textoConcatenado;
					gameObject.GetComponent<TextMesh> ().text = ss;
					puntos++;
					if (puntos > s.Length)
						puntos = 0;
				}
			}
		}
	}
}
