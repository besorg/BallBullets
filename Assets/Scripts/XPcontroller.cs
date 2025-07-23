using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPcontroller : MonoBehaviour {
	float _duration=.4f, _lifetime=0;
	float _fade=0;
	Controlador controlador;
	SpriteRenderer _sprite;
	Rigidbody _rigid;
	Collider _collider;

	void Start () {
		controlador = GameObject.Find ("C_GO").GetComponent<Controlador >();
		_rigid  = GetComponent<Rigidbody>();
		_collider  = GetComponent<Collider>();
		_sprite = GetComponent<SpriteRenderer > ();
	}

	void OnTriggerEnter(Collider other){
		if (other.tag.Contains ("Bloque")) {
			if (other.GetComponent<BloqueControlador> ()) {
				BloqueControlador _bloquee = other.GetComponent<BloqueControlador> ();
				int _id = _bloquee.Get_bloqueID ();
				_bloquee.Get_damage (Creador_de_niveles.Get_grid_vida (_id));
				Destroy (_bloquee.gameObject);
				controlador.vars.explosivasEscombrosDestruidos++;

				if(TraspasedVars.Lenguaje=="Spanish")controlador.SetTextoDescripcion ("Escombros destruidos\ncon XP, Total: "+controlador.vars.explosivasEscombrosDestruidos,2);
				else controlador.SetTextoDescripcion ("Destroyed debris\nwith XP, Total: "+controlador.vars.explosivasEscombrosDestruidos,2);
			}
		}
	}

	void Update () {
		if (!controlador._pause) {
			gameObject.transform.localScale = new Vector3 (.1f + 3*_fade, .1f + 3*_fade, 1);
			_collider.transform.localScale=new Vector3 (.1f + 5*_fade, .1f + 5*_fade, 1);
			_lifetime+=Time.deltaTime;
			Debug.Log (_lifetime + " " +_duration + " " +_fade );
			if(_lifetime>_duration/4){
				_fade = (_lifetime / _duration);
				if (_fade >= 1) {
					controlador.vars.explosivasUsadas++;
					Destroy (gameObject);
					if(TraspasedVars.Lenguaje=="Spanish")controlador.SetTextoDescripcion ("XP usadas\nTotal: "+controlador.vars.explosivasUsadas, 2);
					else controlador.SetTextoDescripcion ("XP used\nTotal: "+controlador.vars.explosivasUsadas, 2);
				}
				else {
					_sprite.color = new Color(_sprite.color.r,_sprite.color.g,_sprite.color.b,1-_fade);
				}
			}
		}	
	}
}
