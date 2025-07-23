using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinuarLevelUpControllador : MonoBehaviour {
	public ControladorLevelCompletedScene controlador;
	public TextMesh tm, levelUPtitle;
	void Awake(){
		if (Application.isEditor && GameObject.FindGameObjectWithTag ("var") == null) {
			SceneManager.LoadScene (0);
			return;
		}
	}

	void Start(){
		controlador = Camera.main.GetComponent<ControladorLevelCompletedScene>();
		if (TraspasedVars.Lenguaje == "Spanish") {
			tm.text = "Nivel\ncompletado";
			if(FindObjectOfType<TraspasedVars>().vars.puntosLibres==1)levelUPtitle.text = "Subiste de nivel: selecciona mejora";
			else levelUPtitle.text = "Subiste de nivel: selecciona mejoras";
		} else {
			tm.text = "Level\ncompleted";
			if(FindObjectOfType<TraspasedVars>().vars.puntosLibres==1)levelUPtitle.text = "Level up: choose a boost";
			else levelUPtitle.text = "Level up: choose boosts";
		}
	}

	void OnMouseDown(){
		controlador.Reiniciar ();
	}
		
}
