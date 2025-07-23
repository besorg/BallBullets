using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BloqueControlador: MonoBehaviour {
	private int id = 0;
	public static Controlador controlador;

	void Awake(){
		id = Creador_de_niveles._valor;
		controlador = GameObject.Find ("C_GO").GetComponent<Controlador> ();

	}

	public int Get_damage(int daño){
		if (Creador_de_niveles.Get_grid_vida (id) <= daño) {
			int a = daño;
			//controlador.vars.puntajeActual += 100*Creador_de_niveles.Get_grid_vida (id);
			controlador.AddScore (100*Creador_de_niveles.Get_grid_vida (id));
			daño -= Creador_de_niveles.Get_grid_vida (id);
			Creador_de_niveles.Restar_grid_vida (id, a);
		} else if (daño == 0) {
			Creador_de_niveles.Restar_grid_vida (id, 1);
			//controlador.vars.puntajeActual += 100;
			controlador.AddScore (100);
		} else {
			Creador_de_niveles.Restar_grid_vida (id, daño);
			//controlador.vars.puntajeActual += 100 * daño;
			controlador.AddScore (100 * daño);
			daño = 0;
		}
		if (Creador_de_niveles.Get_grid_vida (id) <= 0) {
			GetComponent<SpriteRenderer> ().color = new Color (1, 1, 1, .5f);
			GetComponent<Collider> ().isTrigger = true;
			controlador._bloques.Remove (gameObject);
		}

		if (Creador_de_niveles.Get_grid_vida (id) <= 0) {
			if (controlador._bloques.Count > 0)
				Drop ();
			else {
				//NIVEL COMPLETADO...
				controlador.vars.levelActual++;
				if(controlador.vars.dificultadActual>0)controlador.vars.vidasActual += controlador._bola.Count;
				int i = 0;
				foreach (PildoraControlador a in FindObjectsOfType<PildoraControlador>()) {
					if(a!=null)a.GetPildorita ();
					i++;
				}
				//Debug.Log ("PILDORITAS EXTRAS AGARRADAS " + i);
				controlador.SaveTraspasedVars.save ();
				SceneManager.LoadScene ("LevelCompleted");
				//carga la siguiente mision:...
			}
		}
			
		return daño;
	}

	void Drop(){
		int pildorita_tipo, dificultad;
		dificultad = Creador_de_niveles.dificultad;
		float A;
		//0 sandbox, 1 facilucho, 2 facil, 3 normal, 4 dificil, 5 hardcore
		if (dificultad == 0)
			A = Random.Range (0, Mathf.Clamp(100-controlador.vars.dropeoBonus+controlador.vars.levelActual,50, Mathf.Infinity));
		else if (dificultad == 1)
			A = Random.Range (0, Mathf.Clamp(175-controlador.vars.dropeoBonus+controlador.vars.levelActual,50, Mathf.Infinity));
		else if (dificultad == 2)
			A = Random.Range (0, Mathf.Clamp(250-controlador.vars.dropeoBonus+controlador.vars.levelActual,50, Mathf.Infinity));
		else if (dificultad == 3)
			A = Random.Range (0, Mathf.Clamp(400-controlador.vars.dropeoBonus+controlador.vars.levelActual,50, Mathf.Infinity));
		else 
			A = Random.Range (0, Mathf.Clamp(600-controlador.vars.dropeoBonus+controlador.vars.levelActual,50, Mathf.Infinity));

		pildorita_tipo = -1;

		if (A < 1)
			pildorita_tipo = 0;
		else if (A < 4)
			pildorita_tipo = 1;
		else if (A < 6)
			pildorita_tipo = 2;
		else if (A < 8)
			pildorita_tipo = 3;
		else if (A < 11)
			pildorita_tipo = 4;
		else if (A < 30)
			pildorita_tipo = 5;
		else if (A < 40)
			pildorita_tipo = 6;
		else if (A < 44)
			pildorita_tipo = 7;
		else if (A < 50)
			pildorita_tipo = 8;

		//0"Vida",1"Velocidad",2"Xp",3"Sp",4"XL",5"Puntos",6"Más Velocidad",7"Menos Velocidad", 8"Teledirigidas"//max array 8
		if (pildorita_tipo > -1) {
			//Debug.Log ("Pildorita dropeo: " + /*Creador_de_niveles.pildoritas_nombres [*/pildorita_tipo);
			GameObject Pil = Instantiate (
				                 controlador.creador_de_niveles.Pildoritas [pildorita_tipo],
				                 new Vector3 (gameObject.transform.position.x,
					                 gameObject.transform.position.y,
					                 gameObject.transform.position.z),
				                 Quaternion.identity);
			if (Pil.GetComponent<SpriteRenderer> ().sprite.name == "pildoritas_5") {
				if ((50 * controlador.vars.dificultadActual + controlador.vars.scoreBonus + controlador.vars.levelActual + 100) < 99999) {
					Pil.GetComponentInChildren<TextMesh> ().text = "+" + (50 * controlador.vars.dificultadActual + controlador.vars.scoreBonus + controlador.vars.levelActual + 100);
				} else
					Pil.GetComponentInChildren<TextMesh> ().text = "99999";
			}
			controlador._pildoritas.Add (Pil);
		}
	}


	public int Get_bloqueID(){
		int a = id;
		return a;
	}
}
