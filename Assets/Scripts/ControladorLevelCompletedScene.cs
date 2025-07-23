using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorLevelCompletedScene : MonoBehaviour {
	public TraspasedVars Vars;
	//public Controlador controlador;
	public bool moving=false;
	public GameObject fondo;
	public Vector3 camara,fondo_vector;

	public void Establecer_Texto(){
		int puntaje_valor=Vars.vars.scoreBonus;
		if (puntaje_valor > 99999)
			puntaje_valor = 99999;
		string anexo_puntaje_valor = "";
		if(puntaje_valor<4000)anexo_puntaje_valor="";
		else if(puntaje_valor<5000)anexo_puntaje_valor="/5000";
		else if(puntaje_valor<10000)anexo_puntaje_valor="/10000";
		else if(puntaje_valor<25000)anexo_puntaje_valor="/25000";
		else if(puntaje_valor<50000)anexo_puntaje_valor="/50000";
		else anexo_puntaje_valor="/99999";

		if (TraspasedVars.Lenguaje == "Spanish") {
			GameObject.Find ("Textos").GetComponent<TextMesh > ().text =
				"Añadir aceleración " + Mathf.Round (100 * Vars.vars.aceleracionBonus / .15f) + "%/200%\n" +
			"Velocidad máxima extra " + Vars.vars.velocidadMaximaBonus + "/12\n" +
			"Duración de XL " + Vars.vars.largoBonus + "/30 segundos\n" +
			"Tamaño extra en modo XL " + (100 * Vars.vars.largoTamañoBonus) + "%/100%\n" +
			"Duración a Velocidad Máxima " + Vars.vars.velocidadAtopeBonus + "/60 segundos\n" +
			"Extender limite de Energia: " + (100 + Vars.vars.energyBonus) + "/200\n" +
			"SP recuperados al chocar los bordes " + Vars.vars.perforantesRecuperadasContraBordes + "/10\n" +
			"XP extras al recoger pildoras " + Vars.vars.explosivasBonus + "\n" +
			"Dirigidas extras al recoger pildoras " + Vars.vars.dirigidasBonus + "\n" +
			"Mejora de las pildoras de puntos " + puntaje_valor + anexo_puntaje_valor + "\n" +
			"Boost de dropeo de pildoras " + Vars.vars.dropeoBonus + "/" + ((50 * Vars.vars.dificultadActual) + Vars.vars.levelActual + 51);
		} else {
			GameObject.Find ("Textos").GetComponent<TextMesh > ().text =
				"Add acceleration " + Mathf.Round (100 * Vars.vars.aceleracionBonus / .15f) + "%/200%\n" +
			"Extra max Speed " + Vars.vars.velocidadMaximaBonus + "/12\n" +
			"XL duration " + Vars.vars.largoBonus + "/30 segundos\n" +
			"Extra size on XL mode " + (100 * Vars.vars.largoTamañoBonus) + "%/100%\n" +
			"Max speed extra duration " + Vars.vars.velocidadAtopeBonus + "/60 segundos\n" +
			"Extend energy limit: " + (100 + Vars.vars.energyBonus) + "/200\n" +
			"SP from hit borders " + Vars.vars.perforantesRecuperadasContraBordes + "/10\n" +
			"Extra XP from pills " + Vars.vars.explosivasBonus + "\n" +
			"Extra seekers from pills " + Vars.vars.dirigidasBonus + "\n" +
			"Upgrade score pills points " + puntaje_valor + anexo_puntaje_valor + "\n" +
			"Drop rate of pills " + Vars.vars.dropeoBonus + "/" + ((50 * Vars.vars.dificultadActual) + Vars.vars.levelActual + 51);
		}
			
	}

	void Start () {
		Vars = GameObject.Find ("Vars").GetComponent<TraspasedVars>();
		//controlador = GameObject.Find ("").GetComponent<TraspasedVars>();
		fondo = GameObject.Find ("Fondo1");
		fondo_vector = fondo.transform.position;
		camara = Camera.main.transform.position;
		GameObject.Find ("Aumento sel").GetComponent<SpriteRenderer >().color = Vars.vars.pong_color;
		StartCoroutine ("Cargando");
		Establecer_Texto ();
	}

	void Update () {
		if (moving) {
			if (fondo.transform.position.x != camara.x)
				fondo.transform.position = new Vector3 (Mathf.Clamp(fondo.transform.position.x-.2f,camara.x,fondo_vector.x), fondo.transform.position.y, fondo.transform.position.z);
		}
	}

	IEnumerator Cargando(){
		yield return new WaitForSeconds (1);
		if (Vars.vars.puntosLibres <= 0)
			StartCoroutine ("Continuar");
		else {
			moving = true;
		}

	}

	IEnumerator Continuar(){
		yield return new WaitForSeconds (1);

		//Sonido_control.MusicaController.CargarMusicaSiguiente ();
		SceneManager.LoadScene ("Ball Bullets");
	}

	public void Reiniciar(){
		GameObject.Find ("Vars").GetComponent<TraspasedVars> ().save ();
		//GameObject.Find ("Vars").GetComponent<TraspasedVars> ().fondo.SetActive (true);
		//GameObject.Find ("Vars").GetComponent<TraspasedVars> ().fondo_script.Boton_Musica.SetActive (false);
		//GameObject.Find ("Vars").GetComponent<TraspasedVars> ().fondo.GetComponent<OnClick>.SetActive (true);
		//Camera.main.transform.position = GameObject.Find ("CamaraPosInicial").transform.position;
		//SceneManager.LoadScene ("Ball Bullets");

		SceneManager.LoadScene ("Reinicio");
	}
}
