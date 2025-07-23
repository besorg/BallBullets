using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sonido_control : MonoBehaviour {
	AudioSource thisone;
	float tiempo_vida;
	public GameObject[] Audios;
	public float music_Level;
	public int repetitions;

	public AudioClip MainTheme;
	public AudioClip[] Musicas;
	public List<int> Musicas_lista;
	public static Sonido_control MusicaController;
	public static int indiceDeTema;

	public void Crear_listaDeReproduccion(){
		int i = 0;
		Musicas_lista = new List<int> ();
		for (i = 0; i < Musicas.Length - 1; i++) {
			Musicas_lista.Insert(Random.Range(0, Musicas_lista.Count-1), i);
		}
		i = 0;
		foreach(int a in Musicas_lista){
			i++;
		}
	}

	public void CargarTemaPrincipal(){
		thisone.clip = MainTheme;
		tiempo_vida = thisone.clip.length;
		thisone.Play ();
	}

	public void CargarMusicaSiguiente(int indice){
		if (gameObject.name == "Musica") {
			thisone.clip = Musicas [indice];
			tiempo_vida = thisone.clip.length;
			thisone.Play ();
		}
	}

	public void CargarMusicaSiguiente(){
		if (gameObject.name == "Musica") {
			//Debug.Log ("xxx CargarMusicaSiguiente");
			//Debug.Log ("indice tema actual " + indiceDeTema);
			if (indiceDeTema > Musicas_lista.Count - 1)
				indiceDeTema = 0;
			else
				indiceDeTema++;
			//Debug.Log ("indice por cargar " + indiceDeTema + " tema " + Musicas [Musicas_lista [indiceDeTema]].name);
			thisone.clip = Musicas [Musicas_lista [indiceDeTema]];
			tiempo_vida = thisone.clip.length;
			thisone.Play ();
		}
	}

	public void Crear_Sonido(int tipoIndex, int reproducciones, float volumen, bool multiplicarPorGetVolumen){
		//0 click, 1 electric FX, 2 boom, 3 PildoraGet
		if(tipoIndex >= Audios.Length)return;

		int rep = reproducciones;
		float vol = volumen ;
		
		if (reproducciones > 0)
			rep = reproducciones - 1;
		else if (reproducciones <= -1)
			rep = -1;
		else
			rep = 0;

		if(volumen > 1)vol = 1;
		if (volumen < 0)
			vol = 0;
		if(multiplicarPorGetVolumen)vol *= GameObject.Find ("Vars").GetComponent<TraspasedVars> ().GetVolumen ();

		GameObject Sound = Instantiate(Audios[tipoIndex]);
		Sound.GetComponent<AudioSource> ().volume = vol;
		Sound.GetComponent<Sonido_control> ().Setup (rep,vol);//Setup usa las variables "repeticiones" y "volumen", repeticiones -1 es un loop infinito
	}

	public void Setup (int reproducciones, float volumen){
		if (reproducciones > 0)
			repetitions = reproducciones - 1;
		else if (reproducciones == -1)
			repetitions = -1;
		else
			repetitions = 0;
		music_Level = volumen;
	}

	void Awake(){
		if (gameObject.name == "Musica") {
			if (MusicaController == null)
				MusicaController = this;
			else
				DestroyImmediate (gameObject);
		}
	}

	void Start () {
		thisone = GetComponent<AudioSource> ();
		tiempo_vida = thisone.clip.length;
		if (gameObject.name == "Musica") {
			DontDestroyOnLoad (this.gameObject);
			MusicaController = this;
			thisone.volume = GameObject.Find ("Vars").GetComponent<TraspasedVars > ().vars.musicaVol;
			Crear_listaDeReproduccion ();
		}
		if (repetitions == -1) {
			thisone.loop = true;
		}
	}

	float tiempoMuerto = 2, tiempoEntreTemas = 2;
	void Update () {
		if (gameObject.name == "Musica") {
			//Debug.Log ("musica " + Mathf.Abs(tiempo_vida - thisone.clip.length) + "/" + thisone.clip.length);
			if(SceneManager.GetActiveScene().name=="Menu Principal")return;
		}

		thisone.volume = music_Level;
		//Debug.Log (thisone.time+ "tiempo/tiempo vida "+tiempo_vida);
		tiempo_vida -= Time.deltaTime;



		if (tiempo_vida <= 0){
			if (repetitions == 0)
				Destroy (gameObject);
			else if (repetitions > 0) {
				repetitions--;

				tiempo_vida = thisone.clip.length;
				thisone.Play ();
			} else if (repetitions == -1) {
				if (gameObject.name == "Musica") {
					if (tiempoEntreTemas > 0) {
						tiempoEntreTemas -= Time.deltaTime;
						thisone.Stop ();
						return;
					}else 
						tiempoEntreTemas = tiempoMuerto;

					CargarMusicaSiguiente ();
				}
			}
		}
	}
}
