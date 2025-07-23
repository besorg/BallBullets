using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BolaControlador : MonoBehaviour { 
	public Rigidbody _rigid;
	public Collider _collider;
	public GameObject _bola;
	private Controlador controlador;
	private Vector3 velocidadGuardada;
	private TraspasedVars vars;
	public int potencia;
	public bool laSuccionan=false;
	public GameObject XP;
	bool _perseguidora=false, _fuePerseguidora= false;
	public GameObject _perseguidora_objetivo;
	private Vector3 _bolaDireccion;
	public float _bolaSpeed;

	void Start () {
		controlador = GameObject.Find("C_GO").GetComponent<Controlador>();
		potencia = controlador.vars.perforantesActual+1;
		_bolaSpeed = controlador.vars.velocidadPelota;
		velocidadGuardada = _rigid.velocity;
		vars = GameObject.Find ("Vars").GetComponent<TraspasedVars>();
		Debug.Log (name);
		if (tag == "X") {
			_perseguidora=true;
			_fuePerseguidora = true;
		}
		StartCoroutine ("PlayingOn");
	}

	IEnumerator PlayingOn(){
		yield return new WaitForSeconds (3);
		controlador._playing = true;
	}

	void OnTriggerEnter(Collider other){
		if (other.tag.Contains ("Bloque")) {
			if (other.GetComponent<BloqueControlador> ()) {
				BloqueControlador _bloquee = other.GetComponent<BloqueControlador> ();
				int _id = _bloquee.Get_bloqueID ();

				if (tag == "X"&&potencia<=0) {
					_perseguidora = false;
					_collider.isTrigger = false;
					_rigid.isKinematic = false;

					//_rigid.velocity = new Vector3 (3,3,0);
					//_rigid.velocity.Normalize ();
				}
				if (tag == "XP" && controlador._bloques.Contains(other.gameObject)) {
					//GetComponent<ParticleSystem> ().Play ();
					vars.vars.vidasActual++;
					controlador._bola.Remove (gameObject);
					Instantiate (controlador.creador_de_niveles._bolaPrefab[3],transform.position ,Quaternion.identity);

					_rigid.isKinematic = false;
					//GetComponent<SpriteRenderer > ().enabled = false;
					Destroy(gameObject);

				}
				if (_bloquee != null) {
					if (Creador_de_niveles.Get_grid_vida (_bloquee.Get_bloqueID ()) > 0) {
						potencia = _bloquee.Get_damage (potencia);
						if (potencia < 1) {
							//si es menor o cero significa que el bloque tiene más vida (rebota)
							//si es mayor significa más potencia (traspasa)
							other.GetComponent<BoxCollider> ().isTrigger = false;
						}
					}
				}
			}
		}
	}

	void OnCollisionExit(Collision collision){
		if (collision.collider.tag.Contains ("Bloque")) {
			if (collision.collider.GetComponent<BloqueControlador> ()) {
				BloqueControlador _bloquee = collision.collider.GetComponent<BloqueControlador> ();
				int _id = _bloquee.Get_bloqueID ();
				if (_bloquee != null) {
					collision.collider.GetComponent<BoxCollider> ().isTrigger = true;
				}
			}
		}
	}

	void OnCollisionEnter(Collision collision){
		//velocidadGuardada = _rigid.velocity;
		//Debug.Log("velocidadGuardada: " + velocidadGuardada);
		if (collision.collider.tag.Contains ("Player")) {
			/*potencia = controlador.vars.perforantesActual;//es el daño causado al chocar a un bloque
			if (controlador._succion || (!_perseguidora && tag == "X")) {
				transform.SetParent (transform);
				_rigid.velocity = new Vector3 (0, 0, 0);
				laSuccionan = true;
			}*/
			Bola_absorcion ();
		}
		if (collision.collider.name =="Cube1"||collision.collider.name =="Cube") {
			if (controlador.vars.perforantesRecuperadasContraBordes > 0) {
				int b = Mathf.Clamp (potencia + controlador.vars.perforantesRecuperadasContraBordes, 0, controlador.vars.perforantesActual);
				int a = potencia - b;
				potencia = b;
				if(TraspasedVars.Lenguaje=="Spanish")controlador.SetTextoDescripcion ("Recuperación de\npotencia, bola: " + potencia, 2);
				else controlador.SetTextoDescripcion ("Potency recovered\nball's power: " + potencia, 2);
			}
			//potencia += controlador.vars.perforantesRecuperadasContraBordes;//es el daño causado al chocar a un bloque

			//if (potencia > controlador.vars.perforantesActual)
			//	potencia = controlador.vars.perforantesActual;
			if(collision.collider.name =="Cube1")_rigid.velocity = new Vector3 (-Mathf.Abs(_rigid.velocity.x), _rigid.velocity.y, 0);
			if(collision.collider.name =="Cube")_rigid.velocity = new Vector3 (Mathf.Abs(_rigid.velocity.x), _rigid.velocity.y, 0);
		}
	}

	void Bola_absorcion(){
		if (controlador._succion || (!_perseguidora && tag == "X")) {
			transform.SetParent (transform);
			_rigid.velocity = new Vector3 (0, 0, 0);
			laSuccionan = true;
		}
	}

	void OnTriggerStay(Collider collision){
		if (collision.gameObject.tag.Contains ("Player")) {
			Bola_absorcion ();
		}

		if (collision.gameObject.tag.Contains ("Bloque")) {
			if (collision.gameObject.GetComponent<BloqueControlador> ()) {
				BloqueControlador _bloquee = collision.gameObject.GetComponent<BloqueControlador> ();
				int _id = _bloquee.Get_bloqueID ();// GetComponent<BloqueControlador>().Get_bloqueID();
				if (_bloquee != null) {
				}
				if (_perseguidora)
				if (_perseguidora_objetivo == collision.gameObject)
				if (Creador_de_niveles.Get_grid_vida (_bloquee.Get_bloqueID ()) > 0 && potencia <= 0) {
					_perseguidora = false;
					//laSuccionan = true;
				}
			}
		}
	}
	
	void OnCollisionStay(Collision collision){
		if (collision.collider.tag.Contains ("Bloque")) {
			if (collision.collider.GetComponent<BloqueControlador> ()) {
				BloqueControlador _bloquee = collision.collider.GetComponent<BloqueControlador> ();
				int _id = _bloquee.Get_bloqueID ();// GetComponent<BloqueControlador>().Get_bloqueID();
				if (_bloquee != null) {
				}
			}
		}
	}

	void Update () {
		if (controlador._pause == false) {
			if(transform.position.y<-1||transform.position.y>16){
				controlador._bola.Remove (gameObject);
				if (controlador._bola.Count == 0 && controlador.vars.vidasActual == 0 || controlador.vars.dificultadActual >= 5) {
					controlador.Save_progress();
					if (controlador.vars.dificultadActual >= 5) {
						SceneManager.LoadScene ("Reinicio");
						return;
					}
				}
				Destroy (gameObject);
				if(TraspasedVars.Lenguaje=="Spanish")controlador.SetTextoDescripcion ("¡Bola perdida!\nRestantes: "+controlador.vars.vidasActual,2);
				else controlador.SetTextoDescripcion ("Ball loss!\nRemains: "+controlador.vars.vidasActual,2);
			}

			if (laSuccionan) {
				//controlador.SetTextoDescripcion (string.Concat("Absorviendo\nEnergia: ",(controlador._pongEnergyUsada*100/(controlador.vars.energyBonus+100)),"%"),2);
				transform.localScale = new Vector3 (transform.localScale.x * .8f, transform.localScale.y * .8f, transform.localScale.z * .8f);
				//transform.position = new Vector3 (transform.parent.position.x*.8f,transform.parent.position.y*.8f,transform.parent.position.z*.8f);
				if (transform.localScale.x < .1) {
					controlador._bola.Remove (gameObject);
					vars.vars.vidasActual++;
					vars.vars.bolasAbsorvidas++;
					if(TraspasedVars.Lenguaje=="Spanish")controlador.SetTextoDescripcion ("Bolas recuperadas\nTotal: "+vars.vars.bolasAbsorvidas,2);
					else controlador.SetTextoDescripcion ("Ball sucked\nTotal: "+vars.vars.bolasAbsorvidas,2);
					Destroy (gameObject);
				}
			}

			if (!_perseguidora&&(tag!="X"/*||_fuePerseguidora*/)) {
				/*
				if (_rigid.velocity.x >= -controlador._bolaMinXspeed && _rigid.velocity.x <= controlador._bolaMinXspeed) {
					if (Random.value <= controlador._bolaMinXspeed)
						_rigid.velocity = new Vector3 (-controlador._bolaMinXspeed, _rigid.velocity.y, 0);
					else
						_rigid.velocity = new Vector3 (controlador._bolaMinXspeed, _rigid.velocity.y, 0);
				}

				if (_rigid.velocity.x < controlador._bolaMinXspeed && _rigid.velocity.x > 0)
					_rigid.velocity = new Vector3 (controlador._bolaMinXspeed, _rigid.velocity.y, 0);
				if (_rigid.velocity.x > -controlador._bolaMinXspeed && _rigid.velocity.x < 0)
					_rigid.velocity = new Vector3 (-controlador._bolaMinXspeed, _rigid.velocity.y, 0);

				if (_rigid.velocity.y >= -controlador._bolaMinYspeed && _rigid.velocity.y <= controlador._bolaMinYspeed) {
					//int random = 
					if (Random.value <= 0.5f)
						_rigid.velocity = new Vector3 (_rigid.velocity.x, -controlador._bolaMinYspeed, 0);
					else
						_rigid.velocity = new Vector3 (_rigid.velocity.x, controlador._bolaMinYspeed, 0);
				}
				if (_rigid.velocity.y < controlador._bolaMinYspeed && _rigid.velocity.y > 0)
					_rigid.velocity = new Vector3 (_rigid.velocity.x, controlador._bolaMinYspeed, 0);
				if (_rigid.velocity.y > -controlador._bolaMinYspeed && _rigid.velocity.y < 0)
					_rigid.velocity = new Vector3 (_rigid.velocity.x, -controlador._bolaMinYspeed, 0);
					*/


				{
					//Debug.Log (_bolaSpeed + " - _bolaSpeed ");
					//_bolaSpeed = 8;
					_bolaSpeed = controlador.vars.velocidadPelota;
					if (false&&_fuePerseguidora) {
						_bolaSpeed *= .3f;
						transform.Rotate (0, 0, 5);
					}
					if (_rigid.velocity.magnitude < _bolaSpeed) {
						/*_rigid.velocity = new Vector3 (
							Mathf.Sign(_rigid.velocity.x)*Mathf.Clamp(Mathf.Abs(_rigid.velocity.x * 1.1f), controlador._bolaMinXspeed, controlador._bolaMaxSpeed),
							Mathf.Sign(_rigid.velocity.y)*Mathf.Clamp(Mathf.Abs(_rigid.velocity.y * 1.1f), controlador._bolaMinYspeed, controlador._bolaMaxSpeed),
							0);*/
						_rigid.velocity = new Vector3 (
							Mathf.Sign(_rigid.velocity.x)*Mathf.Clamp(Mathf.Abs(_rigid.velocity.normalized.x*_bolaSpeed),controlador._bolaMinXspeed,controlador._bolaMaxSpeed),
							Mathf.Sign(_rigid.velocity.y)*Mathf.Clamp(Mathf.Abs(_rigid.velocity.normalized.y*_bolaSpeed),controlador._bolaMinYspeed,controlador._bolaMaxSpeed)
						    );
					}
					if (_rigid.velocity.magnitude > _bolaSpeed) {
						/*_rigid.velocity = new Vector3 (
							Mathf.Sign(_rigid.velocity.x)*Mathf.Clamp(Mathf.Abs(_rigid.velocity.x / 1.1f), controlador._bolaMinXspeed, controlador._bolaMaxSpeed),
							Mathf.Sign(_rigid.velocity.y)*Mathf.Clamp(Mathf.Abs(_rigid.velocity.y / 1.1f), controlador._bolaMinYspeed, controlador._bolaMaxSpeed),
							0);*/
						_rigid.velocity = new Vector3 (
							Mathf.Sign(_rigid.velocity.x)*Mathf.Clamp(Mathf.Abs(_rigid.velocity.normalized.x*_bolaSpeed),controlador._bolaMinXspeed,controlador._bolaMaxSpeed),
							Mathf.Sign(_rigid.velocity.y)*Mathf.Clamp(Mathf.Abs(_rigid.velocity.normalized.y*_bolaSpeed),controlador._bolaMinYspeed,controlador._bolaMaxSpeed)
						);
					}

					//Debug.Log (_rigid.velocity.x + " _rigid.velocity.x - " + _rigid.velocity.y + " _rigid.velocity.y");
					//Debug.Log (_rigid.velocity + " - magnitude " + _rigid.velocity.magnitude);
					_rigid.velocity.Normalize ();
					if (_rigid.velocity.x < .1f && _rigid.velocity.x > -.1f) {
						if (Random.value <= .5f) {
							_rigid.velocity = new Vector3(.1f,_rigid.velocity.y);
						} else {
							_rigid.velocity = new Vector3(-.1f,_rigid.velocity.y);
						}
					}
				}
				//Debug.Log ("VELOCIDAD PILDORAS "+vars.vars.velocidadPelota);
				//Debug.Log ("velocidad media = "+ _rigid.velocity.magnitude + " - velocidad x = " +_rigid.velocity.x+" - velocidad y = "+_rigid.velocity.y);
			}

			if (tag == "X"&&(_perseguidora||_fuePerseguidora)) {
					//_perseguidora = false;) {
				transform.Rotate (0, 0, 5);

				float dist = 9999;
				//_bolaDireccion = _rigid.velocity.normalized;

				//Vector3 vel = _rigid.velocity;
				//if(_bolaSpeed<4)_bolaSpeed=4;
				if (_perseguidora) {
					foreach (var bloque in controlador._bloques) {
						if (dist >= Vector3.Distance (bloque.transform.position, gameObject.transform.position)) {
							dist = Vector3.Distance (bloque.transform.position, gameObject.transform.position);
							_perseguidora_objetivo = bloque;
						}
					}
					_rigid.isKinematic = true;
					if (_perseguidora_objetivo == null)
						return;
					_rigid.position = Vector3.MoveTowards (transform.position, _perseguidora_objetivo.GetComponent<SpriteRenderer > ().bounds.center, Time.deltaTime*controlador.vars.velocidadPelota);
					if (Vector2.Distance (_rigid.position, _perseguidora_objetivo.transform.position) < .05f) {
						_perseguidora = false;
						//gameObject.tag = "bola perdida";
					}
				}
				else {
					_rigid.isKinematic = true;
					_rigid.position = Vector3.MoveTowards (transform.position, controlador._pong.GetComponent<SpriteRenderer > ().bounds.center, Time.deltaTime*controlador.vars.velocidadPelota);
					//gameObject.tag = "bola perdida";

					if (Vector2.Distance (_rigid.position, controlador._pong.GetComponent<SpriteRenderer > ().bounds.center) < .6f)
						laSuccionan = true;
				}

				//Debug.Log (Vector3.Angle(transform.position, transform.position+_perseguidora_objetivo.transform.position)+"°");
				//_rigid.rotation.eulerAngles = Vector3.Angle (_rigid.rotation.eulerAngles ,_perseguidora_objetivo.transform.position);

				/*
				if (box1 == null) {
					box1 = GameObject.CreatePrimitive (PrimitiveType.Cube);
					box1.GetComponent<Collider> ().enabled = false;
					box2 = GameObject.CreatePrimitive (PrimitiveType.Cube);
					box2.GetComponent<Collider> ().enabled = false;
				}
				box1.transform.position = gameObject.transform.position;
				box1.transform.position = transform.position+_perseguidora_objetivo.transform.position;
				*/
				//_rigid.velocity = new Vector3 (1, 0, 0);
				//_bolaDireccion = _rigid.velocity.normalized;
				 
				//Debug.Log ((_bolaDireccion.x)+" - "+(_bolaDireccion.y));
				// FORMULA
				// 1 radian = 180/Pi
				// 1 radian = 57.29 grados... 1 grado = Pi/180 grados = .0174
				//Debug.Log ((Mathf.Cos (180/Mathf.Rad2Deg))+" - "+Mathf.Sin (180/Mathf.Rad2Deg));
				//cos(180/Rad2Deg) --> funciona...
				//Vector3 A = new Vector3(Mathf.Cos(valor/Mathf.Rad2Deg),Mathf.Sin(valor/Mathf.Rad2Deg),0);
				//Vector3 B = new Vector3 (controlador._bolaSpeed*_bolaDireccion.x, controlador._bolaSpeed*_bolaDireccion.y, 0);
				//Vector3 C = (A + B);
				//_rigid.velocity = A.normalized;
			}

		}
	}

	public void EstablecerVelocidad(){
		_rigid.velocity = velocidadGuardada;// si se establece
	}

	public void GuardarVelocidad(){
		velocidadGuardada = _rigid.velocity;//Si se guarda
	}
}
