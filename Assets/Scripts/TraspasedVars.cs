using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[Serializable]
public class TraspasedVars: MonoBehaviour{
	static readonly string Data = "data.dat";

	private Colores colores;
	public GameObject fondo;
	public TextMesh fondo_textMesh;
	public GameObject fondo_textoGO;
	public FondoScript fondo_script;
	public bool firstWalkThought=true;
	public static string[] dificultad;

	public InputField textInputScript;
	public GameObject textInputTextoGO;

	public SaveData vars = new SaveData ();

	public static string Lenguaje;
	public static Lenguaje lenguajeJson;

	public void Establecer_inputText(){
		textInputScript.text = vars.Nombre;
	}

	public void ChangeName(){
		if (textInputTextoGO == null)
			textInputTextoGO = GameObject.Find ("TextoNoSePuedeCambiar");
		if (textInputScript == null)
			textInputScript = FindObjectOfType<InputField > ();

		if (vars.vidasActual > 0 && vars.puntajeActual > 0) {
			//string Name = textInputScript.text;
			textInputTextoGO.GetComponent<Text> ().enabled = true;
			textInputScript.interactable = false;
			textInputScript.text = vars.Nombre;
		}
		else {
			textInputTextoGO.GetComponent<Text> ().enabled = false;
			textInputScript.interactable = true;
			string Name = textInputScript.text;
			if (Name != "" && Name != vars.Nombre) {
				//Debug.Log ("Funcionando!");
				vars.Nombre = Name;
			}
			//save ();
		}
	}

	public static void CargarLenguaje(){
		string loadPath = Path.Combine (Application.persistentDataPath,"lenguaje.dat");
		if (File.Exists (loadPath)) {
			string datos = File.ReadAllText (loadPath);
			TraspasedVars.lenguajeJson = JsonUtility.FromJson<Lenguaje> (datos);
			TraspasedVars.Lenguaje = TraspasedVars.lenguajeJson.lenguaje;
		} else {
			TraspasedVars.lenguajeJson = new global::Lenguaje ();
			if (Application.systemLanguage == SystemLanguage.Spanish)
				TraspasedVars.lenguajeJson.lenguaje = SystemLanguage.Spanish.ToString ();
			string java = JsonUtility.ToJson (TraspasedVars.lenguajeJson);
			//Debug.Log (java);
			string savePath = Path.Combine (Application.persistentDataPath, "lenguaje.dat");
			//Debug.Log (savePath);
			if (File.Exists (savePath))
				File.Delete (savePath);
			File.WriteAllText (savePath, java);
			TraspasedVars.CargarLenguaje ();
		}


	}

	public static void SaveLenguaje(){
		if (TraspasedVars.lenguajeJson == null) {
			TraspasedVars.lenguajeJson = new global::Lenguaje ();
			if (Application.systemLanguage == SystemLanguage.Spanish)
				TraspasedVars.lenguajeJson.lenguaje = SystemLanguage.Spanish.ToString ();
		}else TraspasedVars.lenguajeJson.lenguaje = TraspasedVars.Lenguaje;
		string java = JsonUtility.ToJson (TraspasedVars.lenguajeJson);
		//Debug.Log (java);
		string savePath = Path.Combine (Application.persistentDataPath, "lenguaje.dat");
		//Debug.Log (savePath);
		if (File.Exists (savePath))
			File.Delete (savePath);
		File.WriteAllText (savePath, java);
		//TraspasedVars.CargarLenguaje ();
	}

	public void LenguajeSeleccion(){
		
	}

	void Awake(){
		CargarLenguaje ();
		//TraspasedVars.Lenguaje = Application.systemLanguage.ToString ();
		Debug.Log (TraspasedVars.Lenguaje);
		if (TraspasedVars.Lenguaje == "Spanish") {

		} else {
			TraspasedVars.Lenguaje = "English";
		}
		DontDestroyOnLoad (this.gameObject);
		DontDestroyOnLoad (GameObject.Find("CanvasOpciones"));
		DontDestroyOnLoad (GameObject.Find("EventSystem"));
		load ();
		if (TraspasedVars.Lenguaje == "Spanish")dificultad = new string[] {"SandBox","Facilucho","Fácil", "Normal", "Dificil", "Hardcore"};
		else dificultad = new string[] {"SandBox","Easier","Easy", "Normal", "Hard", "Hardcore"};
	}

	void Start(){
		GameObject.Find("pongIcono").gameObject.transform.parent.GetComponentInChildren<OnClick>().ActualizarColor (vars.pong_color);
		colores = GetComponent<Colores>();
		//textInput = GameObject.Find ("TextInput").GetComponent<Text> ();
		//Establecer_inputText();
		SetVolumen (vars.soundVol);
		SetMusicVolumen (vars.musicaVol);
		GameObject.Find ("Musica").GetComponent<AudioSource>().volume  = vars.musicaVol;
		GameObject.Find ("Musica").GetComponent<Sonido_control> ().music_Level = vars.musicaVol;

		if(GameObject.Find ("Musica")!=null)
			GameObject.Find ("Musica").GetComponent<Sonido_control>().CargarTemaPrincipal ();

		/*if (vars.vidasActual > 0 && vars.puntajeActual > 0) {
			textInputScript.interactable = false;
			textInputTextoGO.GetComponent<Text> ().enabled = true;
		} else {
			textInputScript.interactable = true;
			textInputTextoGO.GetComponent<Text> ().enabled = false;
		}*/

	}


	public void save(){
		string java = JsonUtility.ToJson (vars);
		//Debug.Log (java);
		string savePath = Path.Combine (Application.persistentDataPath, Data);
		//Debug.Log (savePath);
		if (File.Exists (savePath))
			File.Delete (savePath);
		File.WriteAllText (savePath, java);
		Debug.Log ("Save();");
	}

	public void load(){
		string loadPath = Path.Combine (Application.persistentDataPath,Data);
		if (File.Exists (loadPath)) {
			string datos = File.ReadAllText (loadPath);
			vars = JsonUtility.FromJson<SaveData> (datos);
			Debug.Log ("Load();");
			//Debug.Log (vars.Nombre );
		} else {
			Debug.Log ("failed to get the file, creating new one");
			save ();
		}
	}

	public void SetMusicVolumen(float volumen){
		vars.musicaVol = volumen;
		GameObject.Find ("Musica").GetComponent<Sonido_control> ().music_Level = volumen;
	}

	public void SetVolumen(float volumen){
		vars.soundVol = volumen;
	}

	public float GetVolumen(){
		return vars.soundVol;
	}

	public Color GetConversionDeVolumenAColor(float valor_restado_modificador, bool guardar_valor){//boton de musica
		//usar valor_restado_modificador = -.04f para que entren los 21 colores (contando el blanco)
		Color A = Color.white;
		if (valor_restado_modificador > 1)			valor_restado_modificador = 1;
		if (valor_restado_modificador < 0)			valor_restado_modificador = 1;
		int conv = Mathf.RoundToInt (valor_restado_modificador * 20);
		if (conv > 20)			conv = 20;
		if (conv < 0)			conv = 20;
		A = (Color)colores.colores [20-conv];
		//Debug.Log (A);

		if (guardar_valor) {
			vars.musicaVol = (float)Mathf.RoundToInt(100*valor_restado_modificador)/100;
			GameObject.Find ("Musica").GetComponent<AudioSource> ().volume = vars.musicaVol;
		}
		return A;
	}

}
