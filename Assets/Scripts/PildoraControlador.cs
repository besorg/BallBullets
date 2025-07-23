using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PildoraControlador : MonoBehaviour { 
	public Rigidbody _rigid;
	public Collider _collider;
	private Controlador controlador;
	private Vector3 velocidadGuardada;
	private TraspasedVars vars;
	private SpriteRenderer _spriterenderer;
	bool animationPos=false;// true or false depending on animation goes or return... :v size -10 to size +10
	float animationYsize=10;
	public TextMesh texto;

	void Start () {
		controlador = GameObject.Find("C_GO").GetComponent<Controlador>();
		_rigid = GetComponent<Rigidbody> ();
		_collider = GetComponent<Collider> ();
		_spriterenderer = GetComponent<SpriteRenderer> ();
		_rigid.velocity = controlador.pildora_velocidad;
		velocidadGuardada = _rigid.velocity;
		vars = GameObject.Find ("Vars").GetComponent<TraspasedVars>();
		texto = GetComponentInChildren<TextMesh>();
	}

	public void Die(){
		Destroy (gameObject);
	}

	public void GetPildorita(){
		if (controlador != null)
		if (controlador._pong != null) {
			OnTriggerEnter (controlador._pong.GetComponent<Collider> ());
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.tag.Contains ("Player")) {
			if (_spriterenderer.sprite.name == "pildoritas_0") {
				vars.vars.vidasActual++;
				if(TraspasedVars.Lenguaje=="Spanish")controlador.SetTextoDescripcion ("¡Vida Extra!\nVidas: " + controlador.vars.vidasActual, 2);
				else controlador.SetTextoDescripcion ("Extra life!\nLifes: " + controlador.vars.vidasActual, 2);
			} else if (_spriterenderer.sprite.name == "pildoritas_1") {
				vars.vars.aceleracionAlMaximoActual += (30 + vars.vars.velocidadAtopeBonus);
				if (vars.vars.aceleracionAlMaximoActual > 120)
					vars.vars.aceleracionAlMaximoActual = 120;
				if(TraspasedVars.Lenguaje=="Spanish")controlador.SetTextoDescripcion ("¡Velocidad máxima!\nDuración: " + Mathf.FloorToInt (controlador.vars.aceleracionAlMaximoActual) + " seg.", 2);
				else controlador.SetTextoDescripcion ("Max speed!\nDuration: " + Mathf.FloorToInt (controlador.vars.aceleracionAlMaximoActual) + " sec.", 2);
					
			} else if (_spriterenderer.sprite.name == "pildoritas_2") {
				vars.vars.explosivasActual += vars.vars.explosivasBonus + 5;
				if (vars.vars.explosivasActual > 20)
					vars.vars.explosivasActual = 20;
				if(TraspasedVars.Lenguaje=="Spanish")controlador.SetTextoDescripcion ("Explosivas\nTotal: " + controlador.vars.explosivasActual, 2);
				else controlador.SetTextoDescripcion ("Explosives\nTotal: " + controlador.vars.explosivasActual, 2);
			}
			else if (_spriterenderer.sprite.name == "pildoritas_3") {
				vars.vars.perforantesActual++;
				if (vars.vars.perforantesActual > 20)
					vars.vars.perforantesActual = 20;
				if(TraspasedVars.Lenguaje=="Spanish")controlador.SetTextoDescripcion ("Penetración\nPotencia: " + controlador.vars.perforantesActual, 2);
				else controlador.SetTextoDescripcion ("Penetrations\nPower: " + controlador.vars.perforantesActual, 2);
			}
			else if (_spriterenderer.sprite.name == "pildoritas_4") {
				vars.vars.largoActual += vars.vars.largoBonus + 30;
				if (vars.vars.largoActual > 300)
					vars.vars.largoActual = 300;
				if(TraspasedVars.Lenguaje=="Spanish")controlador.SetTextoDescripcion ("¡Pong XL!\nDuración: " + Mathf.FloorToInt (controlador.vars.largoActual) + " seg.", 2);
				else controlador.SetTextoDescripcion ("¡Pong XL!\nDuration: " + Mathf.FloorToInt (controlador.vars.largoActual) + " sec.", 2);
			} else if (_spriterenderer.sprite.name == "pildoritas_5") {
				if ((50 * vars.vars.dificultadActual + vars.vars.scoreBonus + vars.vars.levelActual + 100) < 99999) {
					controlador.AddScore (50 * vars.vars.dificultadActual + vars.vars.scoreBonus + vars.vars.levelActual + 100);
				} else
					controlador.AddScore (99999);
				if(TraspasedVars.Lenguaje=="Spanish")controlador.SetTextoDescripcion ("Puntuación:\n " + Mathf.FloorToInt (controlador.vars.puntajeActual), 2);
				else controlador.SetTextoDescripcion ("Score:\n " + Mathf.FloorToInt (controlador.vars.puntajeActual), 2);
			} else if (_spriterenderer.sprite.name == "pildoritas_6") {
				vars.vars.velocidadPelota += .2f;
				if (vars.vars.velocidadPelota > controlador._bolaMaxSpeed)
					vars.vars.velocidadPelota = controlador._bolaMaxSpeed;
				if(TraspasedVars.Lenguaje=="Spanish")controlador.SetTextoDescripcion ("Velocidad\nBola: " + (Mathf.RoundToInt ((controlador.vars.velocidadPelota-3)*5)-4), 2);
				else controlador.SetTextoDescripcion ("Velocity\nBall: " + (Mathf.RoundToInt ((controlador.vars.velocidadPelota-3)*5)-4), 2);
			} else if (_spriterenderer.sprite.name == "pildoritas_7") {
				vars.vars.velocidadPelota -= .2f;
				if (vars.vars.velocidadPelota < controlador._bolaMinSpeed)
					vars.vars.velocidadPelota = controlador._bolaMinSpeed;
				if(TraspasedVars.Lenguaje=="Spanish")controlador.SetTextoDescripcion ("Velocidad\nBola: " + (Mathf.RoundToInt ((controlador.vars.velocidadPelota-3)*5)-4), 2);
				else controlador.SetTextoDescripcion ("Velocidad\nBola: " + (Mathf.RoundToInt ((controlador.vars.velocidadPelota-3)*5)-4), 2);
			}
			else {
				vars.vars.perseguidorasActual += (5 + vars.vars.dirigidasBonus);
				if (vars.vars.perseguidorasActual > 100)
					vars.vars.perseguidorasActual = 100;
				if(TraspasedVars.Lenguaje=="Spanish")controlador.SetTextoDescripcion ("Perseguidoras:\n " + controlador.vars.perseguidorasActual, 2);
				else controlador.SetTextoDescripcion ("Seekers:\n " + controlador.vars.perseguidorasActual, 2);
			}
			controlador.Musica_sonido_control.Crear_Sonido (3, 1, 1, false);
			Destroy (gameObject);
		}
	}


	void Update () {
		if (controlador._pause == false) {
			if (transform.position.y < -.5f) {

				controlador._pildoritas.Remove (gameObject);
				Destroy (gameObject);

				return;
			}
			if (_spriterenderer.sprite.name != "pildoritas_5") {
				transform.position = new Vector3 (transform.position.x,transform.position.y,-transform.position.y/10000);
				if (animationPos) {
					if (animationYsize > -5)
						animationYsize -= Time.deltaTime * 20;
					animationYsize -= Time.deltaTime * 10;
					if (animationYsize <= -10)
						animationPos = false;
				} else {
					if (animationYsize < 5)
						animationYsize += Time.deltaTime * 20;
					animationYsize += Time.deltaTime * 10;
					if (animationYsize >= 10)
						animationPos = true;
				}

				_spriterenderer.transform.localScale = new Vector3 (
					gameObject.transform.localScale.x,
					Mathf.Clamp (animationYsize, -10, 10),
					gameObject.transform.localScale.z); 
			} else {
				transform.position = new Vector3 (transform.position.x,transform.position.y,-transform.position.y/100000+transform.position.x/1000000);
			}
		}
	}

	void OnBecameInvisible(){
		controlador._pildoritas.Remove (gameObject);
		Destroy (gameObject);
	}

	public void EstablecerVelocidad(){
		_rigid.velocity = velocidadGuardada;// si se establece
	}

	public void GuardarVelocidad(){
		velocidadGuardada = _rigid.velocity;//Si se guarda
	}
}
