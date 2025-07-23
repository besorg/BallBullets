using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_control : MonoBehaviour {
	public GameObject AudioControl;

	public GameObject[] botones;
	public Collider2D colorR,colorG,colorB;
	public int cuadrado_presed;

	public Transform pos1,pos2,pos3,pos4,pos5;

	public GameObject[] Pildoras;
	public GameObject colores;

	//public int dificultad=3;

	public SpriteRenderer coloresSprite, pongSR;

	public GameObject[] Señalador;
	public GameObject _boton_erase;
	public TextMesh texto_descriptivo;

	public bool clickeables = false;
	public bool activar_clickeables = false;// determina si el efecto inicial se desvanecio permitiendo controlar el clickeable segun movimiento

	public GameObject Fondo;

	void Awake(){

	}

	void Start(){
		AudioControl = GameObject.Find ("Musica");//.GetComponent<Sonido_control>();
		if (FindObjectOfType<TraspasedVars> () != null) {
			FindObjectOfType<TraspasedVars> ().Establecer_inputText ();
			FindObjectOfType<TraspasedVars> ().ChangeName ();
		}
		//GameObject.Find("Vars").GetComponent<TraspasedVars>().Establecer_inputText ();
		//GameObject.Find("Vars").GetComponent<TraspasedVars>().ChangeName ();
		activar_clickeables = true;
	}

	void Update ()
	{
		
		if (Input.GetButtonDown ("Fire1")) {
		}

		if (cuadrado_presed == 1) {
			gameObject.transform.position = new Vector3 (Mathf.Clamp (gameObject.transform.position.x + pos1.position.x / 100, pos2.position.x, pos1.position.x), pos1.position.y, 1);
			colores.transform.position = new Vector3 (Mathf.Clamp (colores.transform.position.x + pos4.position.x / 30, pos5.position.x, pos4.position.x), pos4.position.y, 1);
		} else if (cuadrado_presed == 0) {
			gameObject.transform.position = new Vector3 (Mathf.Clamp (gameObject.transform.position.x - pos1.position.x / 100, pos2.position.x, pos1.position.x), pos1.position.y, 1);
			colores.transform.position = new Vector3 (Mathf.Clamp (colores.transform.position.x - pos4.position.x / 30, pos5.position.x, pos4.position.x), pos4.position.y, 1);
		}

		if (activar_clickeables) {
			//Debug.Log (colores.gameObject.transform.position + "-" + pos4.gameObject.transform.position);
			if (((Vector2)(colores.gameObject.transform.position - pos5.gameObject.transform.position) == Vector2.zero) || ((Vector2)(colores.gameObject.transform.position - pos4.gameObject.transform.position) == Vector2.zero)) {
				clickeables = true;
				//Debug.Log ("Pos correcta!");
			} else
				clickeables = false;
		}
	}


	IEnumerator OcultarPildoras(){
		yield return new WaitForSeconds (1);
		for (int i = 0; i < 6; i++) {
			Pildoras [i].SetActive (false);
		}
	}

	IEnumerator OcultarTextoDescriptivo(){
		yield return new WaitForSeconds (1.5f);
		texto_descriptivo.text = " ";
		_boton_erase.SetActive (false);
		OnClick.block_mouseenter = false;
	}

}

