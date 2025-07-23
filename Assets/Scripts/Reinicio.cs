using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reinicio : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//Destroy (GameObject.Find("Musica"));
		Destroy (GameObject.Find("Vars"));
		Destroy (GameObject.Find("Fondo"));
		Destroy( FindObjectOfType<TraspasedVars> ());
		Debug.Log (this.gameObject+" cargando el reinicio");

		StartCoroutine ("Reiniciar");

		if(TraspasedVars.Lenguaje=="Spanish") gameObject.GetComponent<TextMesh> ().text = "Cargando";
		else gameObject.GetComponent<TextMesh> ().text = "Loading";
	}
	
	IEnumerator Reiniciar(){
		yield return new WaitForSeconds (3);
		/*Destroy (GameObject.Find("Vars"));
		Destroy (GameObject.Find("Fondo"));
		Destroy (GameObject.Find("Musica"));*/
		//Destroy (varGO);
		//Destroy (fondo);
		//Destroy (Musica);
		SceneManager.LoadScene ("Menu Principal");
	}
}
