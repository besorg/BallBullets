using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SonidoAndMusicaControl : MonoBehaviour {
	//public Rigidbody _rigid;
	//public Collider _collider;
	public GameObject[] _botones;
	public string _tipo;
	private Controlador controlador;
	private TraspasedVars vars;
	public Slider slider;

	void Awake () {
		//controlador = GameObject.Find ("C_GO").GetComponent<Controlador >();
		vars = GameObject.Find ("Vars").GetComponent<TraspasedVars >();
	}

	public void SliderSet(float valor){
		float x = valor;

		if(x>1)x=1;
		if(x<0)x=0;

		for (int i = 0; i < _botones.Length; i++) {
			if (i <Mathf.Round(x*10)) {
				_botones [i].GetComponent<CanvasRenderer > ().GetComponent<Image>().color = Color.white;
			}else _botones [i].GetComponent<CanvasRenderer > ().GetComponent<Image>().color = Color.red;
		}
	}

	public void SliderChange(){
		//Vector3 v = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,0,0));
		float x /*= (v.x-_collider.bounds.min.x)/(_collider.bounds.max.x-_collider.bounds.min.x)*/;

		x = slider.value;
		if(x>1)x=1;
		if(x<0)x=0;

		if (_tipo == "sonido") {
			if (x < .05f)
				x = 0;
			vars.vars.soundVol = x;
		}

		else if (_tipo == "musica") {
			if (x < .05f)
				x = 0;
			vars.vars.musicaVol = x;
			GameObject.Find ("Musica").GetComponent<Sonido_control>().music_Level = x;
			GameObject.Find ("Musica").GetComponent<AudioSource>().volume  = x;
		}
		SliderSet (x);
		vars.save ();
	}
}
