using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PongController : MonoBehaviour {
	public GameObject PS_succion,PS_electricidad,PS_XP;
	public Controlador controlador;

	void Start(){
		controlador = GameObject.Find ("C_GO").GetComponent<Controlador>();
	}

	void Update(){
		if (controlador._succion) {
			PS_succion.SetActive (true);
			if (controlador._pongEnergyUsada > 50 + controlador.vars.energyBonus)
				PS_electricidad.SetActive (true);
			else
				PS_electricidad.SetActive (false);
			if (controlador._pongEnergyUsada >= 100 + controlador.vars.energyBonus)
				PS_XP.SetActive (true);
			else
				PS_XP.SetActive (false);
		} else {
			PS_succion.SetActive (false);
			PS_electricidad.SetActive (false);
		}
	}
}
