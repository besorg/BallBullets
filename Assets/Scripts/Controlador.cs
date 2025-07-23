using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controlador : MonoBehaviour {
	public bool _pause = false;
	public bool _playing = false;
	public bool _debug = true;
	public bool _fullBlocked = true;//bloquea los movimientos todos en el juego
	public GameObject[] botones;
	public GameObject _pong;
	public List<GameObject> _bola;
	public List<GameObject> _bloques;
	public List<GameObject> _pildoritas;
	public float _maxPongSpeed = 8f, _PongSpeed = 0f, _PongAceleracion = .15f;

	public float _bolaMaxSpeed = 8,_bolaMinSpeed = 4, _bolaSpeed = 8;//Bolamaxspeed determina si la velocidad debe bajar o subir con cada choque,_bolaSpeed se refiere a la velocidad standard de todas las bolas en general
	public float _bolaMinXspeed = 1.5f, _bolaMinYspeed = 3;

	public Vector2 _bolaDireccion;
	public int _pong_dir=0;//0 null, 1 adelante, -1 atras, para el control con touch pads en OnClickDown funcion
	public bool _click_direccion_press=false;//variable de si está presionado un boton con el click
	public bool escena_cargada;//variable que fondo cambia para poder usar el teclado... :v lo único que se me ocurrió al final xD

	public GameObject varGO;//vars game object que contiene los archivos y la informacion traspasada
	public TraspasedVars SaveTraspasedVars;// script perteneciente a varGO que tiene la funcion save & load
	public SaveData vars;// archivo de guardado para modificar y para cargar perteneciente a varGO y a TraspasedVars
	public GameObject fondo;
	public TextMesh fondo_textMesh;
	public GameObject fondo_textoGO;
	public FondoScript fondo_script;
	public GameObject Musica;
	public AudioSource Musica_AudioSourse;
	public AudioClip Musica_AudioClip;
	public Sonido_control Musica_sonido_control;
	public GameObject androidControls;
	public Creador_de_niveles creador_de_niveles;
	public Vector3 pildora_velocidad;
	public bool _succion = false;
	public Camera camara;
	public GameObject canvasOpciones;

	public GameObject textoInformativoGO;
	public TextMesh textoInformativo;
	public GameObject Interface,botonXP,botonDirigidas,botonBolas,botonBolas2;

	public int _pongEnergyUsada;
	public float _estallido;

	IEnumerator texto_IEnumerator;

	public GameObject OneClickControlGO;
	public GameObject OneClickCanvasGO;
	public GameObject OneClickOcultadorDelMensajeInicial;


	private bool _creacion_de_primera_bola = false;

	public void AddScore(int valor){
		vars.puntajeActual += valor;
		if (vars.puntajeActual > 999999999) {
			vars.puntajeActual = 999999999;
			vars.puntajeSiguienteVida = 0;
			vars.puntajeSiguienteVidaMaximo = 50000000;
		}
		vars.puntajeSiguienteVida -= valor;
		if (vars.puntajeSiguienteVida <= 0) {
			if (vars.levelActual >= 10000) {
				vars.puntajeSiguienteVida = 50000000;
				vars.levelActual = 10000;
				vars.dificultadActual = 200;
				vars.vidasActual = 99999;
			} else {
				vars.puntajeSiguienteVida += 10000 + 5000 * vars.dificultadActual + (500 * vars.levelActual * vars.dificultadActual) + (1000 * vars.puntajeSiguienteVidaMaximo);
				if (vars.puntajeSiguienteVidaMaximo < 10000)
					vars.puntajeSiguienteVidaMaximo++;
			}
			vars.vidasActual++;
			vars.puntosLibres++;
			if(TraspasedVars.Lenguaje=="Spanish")SetTextoDescripcion ("¡Vida extra!\nVidas "+vars.vidasActual,2);
			else SetTextoDescripcion ("Extra life!\nLifes "+vars.vidasActual,2);
		}
	}

	public float _SetTextoDescriptionDuracion;
	public void SetTextoDescripcion(string texto, float duracion){
		textoInformativo.text = texto+"";
		_SetTextoDescriptionDuracion = duracion;
		StopCoroutine ("TextoDescripcion");
		StartCoroutine ("TextoDescripcion");
	}

	IEnumerator TextoDescripcion(){
		yield return new WaitForSeconds (_SetTextoDescriptionDuracion);
		textoInformativo.text=" ";
	}

	void Awake(){
		if (Application.isEditor&&GameObject.FindGameObjectWithTag ("var")==null) {
			SceneManager.LoadScene (0);
			return;
		}

		OneClickOcultadorDelMensajeInicial.SetActive (false);
		canvasOpciones = GameObject.Find ("CanvasOpciones");
		//Debug.Log (_bolaMinXspeed + " - " + _bolaMinYspeed );
		androidControls = GameObject.Find ("InterfaceAndroid");	
		Interface = GameObject.Find ("Interface");
		botonXP= GameObject.Find ("boton XP");
		botonDirigidas= GameObject.Find ("boton dirigidas");
		botonBolas= GameObject.Find ("boton bola");
		botonBolas2= GameObject.Find ("boton bola2");
		textoInformativoGO = GameObject.Find ("TextoInformativo");
		textoInformativo = GameObject.Find ("TextoInformativo").GetComponent<TextMesh>();
		textoInformativo.text = " ";
		//androidControls.SetActive (false);
		//Interface.SetActive (false);
		//OneClickCanvasGO.SetActive (false);
		/*if (Application.isMobilePlatform||_debug) {
			StartCoroutine("Activar_controles");
		}*/
		Musica = GameObject.Find ("Musica");
		//Musica_boton = GameObject.Find ("Boton_musica");
		Musica_AudioSourse = Musica.GetComponent<AudioSource> ();
		Musica_AudioClip = Musica.GetComponent<AudioClip> ();
		Musica_sonido_control = Musica.GetComponent<Sonido_control> ();

		camara = Camera.main;

		//botones = new GameObject[2];//0 flecha izquierda, 1 flecha derecha
		_playing = false;// si esta jugando o en algun menu
		_pause = false;// si se apretó pause o si se esta en aplicacion sin foco}

		varGO = GameObject.FindGameObjectWithTag ("var");
		SaveTraspasedVars = varGO.GetComponent<TraspasedVars> ();
		vars = SaveTraspasedVars.vars;

		if (!SaveTraspasedVars.firstWalkThought) {
			//StartCoroutine ("Set_playing");
		} else
			SaveTraspasedVars.firstWalkThought = false;

		_pong = Instantiate (creador_de_niveles._pongPrefab,GameObject.Find("pongPos").transform.position ,Quaternion.identity,gameObject.transform);
		vars.pong_color.a = 1;
		_pong.GetComponent<SpriteRenderer> ().color = SaveTraspasedVars.vars.pong_color;

		//temporal variables reiniciadas
		if (vars.vidasActual > 0 && vars.puntajeActual > 0) {
			//Si se siguen teniendo vidas en la partida anterior
			//Debug.Log ("1- difactual = "+vars.dificultadActual+", difseleccionada ="+vars.dificultadActual);
			_bolaDireccion.Set (.2f, 1);
			_bolaSpeed = vars.velocidadPelota;
		} else {
			//Si se tiene que comenzar todo de nuevo desde el nivel 1
			vars.vidasActual = 3;
			//vars.dificultadActual = vars.dificultadActual;
			Debug.Log ("2- difactual = "+vars.dificultadActual+", difseleccionada ="+vars.dificultadActual);
			vars.explosivasActual = 0;
			vars.largoActual = 0;
			vars.levelActual = 1;
			vars.perforantesActual = 0;
			vars.perseguidorasActual = 0;
			vars.explosivasActual = 0;
			vars.puntajeActual = 0;
			vars.aceleracionAlMaximoActual = 0;
			vars.perforantesRecuperadasContraBordes = 0;
			vars.velocidadMaxPong = 8;
			_bolaDireccion.Set (.2f, 1);
			vars.aceleracionAlMaximoActual = 0;
			vars.aceleracionBonus = 0;
			vars.dificultadActual = vars.dificultadMenuPrincipal;
			vars.dirigidasBonus = 0;
			vars.dropeoBonus = 0;
			vars.energyBonus = 0;
			vars.explosivasBonus = 0;
			vars.largoBonus = 0;
			vars.largoTamañoBonus = 0;
			vars.puntajeSiguienteVida = 10000 + 5000 * vars.dificultadActual;
			vars.puntosLibres = 0;
			vars.scoreBonus = 0;
			vars.tiempoDePartida = 0;
			vars.velocidadAtopeBonus = 0;
			vars.velocidadMaximaBonus = 0;

			_bolaSpeed = vars.dificultadActual+2;

			vars.vecesJugadas++;
			vars.dificultadTotal += vars.dificultadActual;
		}
		if (_bolaSpeed > _bolaMaxSpeed)
			_bolaSpeed = _bolaMaxSpeed;
		if (_bolaSpeed < 3)
			_bolaSpeed = 3;
		pildora_velocidad = new Vector3 (0,-2-(vars.dificultadActual)/2,0);
		_PongAceleracion = .15f + vars.aceleracionBonus;
		_maxPongSpeed = 6 + vars.velocidadMaximaBonus;
	}



	void Start(){
		_playing = false;
		_estallido = 0; // segundos inutilizados por estallido

		//if (Application.isMobilePlatform||true) {
		if (Application.isMobilePlatform)Input.backButtonLeavesApp = true;

		creador_de_niveles.CrearBloques ();

		OneClickCanvasGO.SetActive (false);
		//OneClickControlGO.SetActive (false);
		_Activar_controles_time = 1;
		/*
		if (vars.ocultar_ui) {
			//OneClickCanvasGO.SetActive (true);
			if (vars.ocultar_textoTutorial_UIoculta) {
				//_Activar_controles_time = 1;
				//OneClickOcultadorDelMensajeInicial.SetActive (false);
			} else {
				//OneClickOcultadorDelMensajeInicial.SetActive (true);
				_Activar_controles_time = 60;
				SetTextoDescripcion ("Toca la pantalla en la parte inferior para mover el pong de izquierda a derecha\nToca en la parte superior para acceder a un menu circular invisible para\nusar determinadas funciones\n\nPresiona y mueve hacia determinado lugar hará cumplirá la función de los\nbotones...\nUn toque: Intentará crear una bola común\nHacia la derecha: Creará una bola común\nHacia la izquierda: Usará la succión o escudo de energia para recuperar la bola\nHacia abajo: Creará una bola dirigida (Requiere <->)\nHacia arriba: Creará una bola Explosiva (Requiere XP)\n\n\n\n\n\n\n\n\n\n", _Activar_controles_time);
				_pong.SetActive (false);
				foreach (GameObject a in _bloques) {
					a.SetActive (false);
				}
			}
		}
		*/

		StartCoroutine("Activar_controles");
		//}
	}

	float segundos = 0;
	void Update () {
		_playing = true;
		_pause = false;
		
		//Debug.Log (Mathf.RoundToInt(Mathf.Infinity));
		//Debug.Log ("Camera.x MAX = " + Camera.main.ScreenToWorldPoint (new Vector3(UnityEngine.Screen.width,0)));
		if (Mathf.CeilToInt(segundos) != Mathf.CeilToInt(Time.realtimeSinceStartup)) {
			//Debug.Log ("Segundos = "+segundos);
			segundos = Time.realtimeSinceStartup;
		}

		if (Input.GetButtonDown ("Cancel")) {
			Application.Quit ();
		}

		if (_playing) {
			if (Input.GetButtonDown ("Pause") && escena_cargada) {
				if (!_pause) {
					_pause = true;
					canvasOpciones.GetComponent<CanvasOpcionesScript> ().AbrirCerrarMenuOpciones ();
					canvasOpciones.GetComponent<Canvas> ().enabled = true;
					/*fondo_script.ResetCounter ();
					fondo_script.SetText ("Pausado...");
					fondo.SetActive (true);*/
					foreach (var item in _bola) {
						item.GetComponent<BolaControlador> ().GuardarVelocidad ();
						item.GetComponent<Rigidbody> ().isKinematic = true;
					}
					foreach (var item in _pildoritas) {
						item.GetComponent<PildoraControlador> ().GuardarVelocidad ();
						item.GetComponent<Rigidbody> ().isKinematic = true;
					}
				} else {
					_pause = false;
					canvasOpciones.GetComponent<Canvas> ().enabled = false;
					/*fondo_script.ResetCounter ();
					fondo.SetActive (false);*/
					foreach (var item in _bola) {
						item.GetComponent<BolaControlador> ().EstablecerVelocidad ();
						item.GetComponent<Rigidbody> ().isKinematic = false;
					}
					foreach (var item in _pildoritas) {
						item.GetComponent<PildoraControlador> ().EstablecerVelocidad ();
						item.GetComponent<Rigidbody> ().isKinematic = false;
					}
				}
			}
		}

		if (_playing && !_pause) {
		//_pong_dir accion = 0 significa que no hara nada, -1 atras, 1 adelante...

		if (Input.GetKeyDown (KeyCode.F)) {
			GameObject bloque = _bloques [0];
			_bloques.Clear ();
			bloque.GetComponent<BloqueControlador > ().Get_damage (999);
		}

		if ((Application.isMobilePlatform&&!vars.ocultar_ui)|| _debug) {
			if (vars.perseguidorasActual == 0) {
				botonDirigidas.SetActive (false);
			} else
				botonDirigidas.SetActive (true);

			if (vars.explosivasActual == 0) {
				botonXP.SetActive (false);
			} else
				botonXP.SetActive (true);
			
			if (vars.vidasActual == 0) {
				botonBolas.SetActive (false);
				botonBolas2.SetActive (false);
			} else
				botonBolas.SetActive (true);
				botonBolas2.SetActive (true);
		}



		//if (!Application.isMobilePlatform) {

		//}

			if (!Application.isMobilePlatform && !_debug) {
				/*if (Input.GetButtonUp ("Fire3")) {
					_succion = false;
					_pong.GetComponent<PongController> ().PS_succion.SetActive (false);
				}
				if (Input.GetButtonDown ("Fire3")) {
					_succion = true;
					Musica_sonido_control.Crear_Sonido (1, 0, SaveTraspasedVars.GetVolumen (), false);
					_pong.GetComponent<PongController> ().PS_succion.SetActive (true);
				}
				if (Input.GetButtonDown ("Fire1")&&_creacion_de_primera_bola==true) {
					Crear_Bola (0);
					SetTextoDescripcion ("Vidas restantes: " + vars.vidasActual, 2);
				}*/

				if (!_creacion_de_primera_bola)
					_creacion_de_primera_bola = true;

				if (Input.GetAxisRaw ("Horizontal") > 0) {
					_pong_dir = 1;
				}

				if (Input.GetAxisRaw ("Horizontal") < 0) {
					_pong_dir = -1;
				}

				if (Input.GetAxisRaw ("Horizontal") == 0&&!_click_direccion_press) {
					_pong_dir = 0;
				}
			}

			if (_pong_dir == 0) {
				_PongSpeed = _PongSpeed * .9f;
				if (_PongSpeed < 1 && _PongSpeed > -1)
					_PongSpeed = 0;
				_pong.transform.Translate (_PongSpeed * Time.deltaTime, 0, 0);
			}

			if (_pong_dir != 0) {
				if (_pong_dir == -1 || _pong_dir == 1) {
					if (_pong_dir == -1)
						_PongSpeed = _PongSpeed - _PongAceleracion;
					if (_pong_dir == 1)
						_PongSpeed = _PongSpeed + _PongAceleracion;
					if (_PongSpeed > _maxPongSpeed)
						_PongSpeed = _maxPongSpeed;
					if (_PongSpeed < -_maxPongSpeed)
						_PongSpeed = -_maxPongSpeed;
					_pong.transform.Translate (_PongSpeed * Time.deltaTime, 0, 0);
					//Debug.Log (_PongSpeed + " velocidad actual");
				}
			}
			_pong.transform.position = new Vector3 (Mathf.Clamp (_pong.transform.position.x,
				0+_pong.transform.GetComponent<Collider>().bounds.size.x/2, 
				Camera.main.ScreenToWorldPoint (new Vector3(UnityEngine.Screen.width,0)).x-_pong.transform.GetComponent<Collider>().bounds.size.x/2),
				_pong.transform.position.y, 0);

			if (_succion&&_estallido<=0) {
				_pongEnergyUsada++;
				if (TraspasedVars.Lenguaje == "Spanish")SetTextoDescripcion ("Energia\nRestante: " + (100+vars.energyBonus-_pongEnergyUsada), 2);
				else SetTextoDescripcion ("Energy\nRemains: " + (100+vars.energyBonus-_pongEnergyUsada), 2);
				if (_pongEnergyUsada >= 100 + vars.energyBonus) {
					_estallido = 5;
					//_succion = false;
				}
			} else if (_estallido == 0) {
				_pongEnergyUsada--;
				if (_pongEnergyUsada <= 0) {
					_pongEnergyUsada = 0;
					//SetTextoDescripcion ("¡Energia al máximo!\nRestante: " + 100+vars.energyBonus, 2);
				}
			}
			_pongEnergyUsada = Mathf.Clamp (_pongEnergyUsada, 0, 100+vars.energyBonus);

			//gasto de variables:
			if (vars.largoActual > 0) {
				vars.largoActual -= Time.deltaTime;
				//Vector3 ASD = Vector3.Lerp(_pong.transform.localScale, Vector3.one+new Vector3(vars.largoTamañoBonus,vars.largoTamañoBonus,0),.1f);
				_pong.transform.localScale = Vector3.Lerp (_pong.transform.localScale, new Vector3 (3+vars.largoTamañoBonus, 2+vars.largoTamañoBonus/4, 0), .1f);
				if (vars.largoActual < 4&&GameObject.Find("electricFX(Clone)")==null) {
					Musica_sonido_control.Crear_Sonido (1, 1, 1, true);
				}
			} else {
				vars.largoActual = 0;
				_pong.transform.localScale = Vector3.Lerp(_pong.transform.localScale, Vector3.one+Vector3.one,.2f);
			}

			if (vars.aceleracionAlMaximoActual > 0) {
				vars.aceleracionAlMaximoActual -= Time.deltaTime;
				_PongAceleracion = .75f; 
			} else {
				vars.aceleracionAlMaximoActual = 0;
				_PongAceleracion = .15f + vars.aceleracionBonus;
			}


			if (_estallido > 0) {
				_pongEnergyUsada = 100 + vars.energyBonus;
				if (TraspasedVars.Lenguaje == "Spanish")SetTextoDescripcion ("¡Fallo de energia!\nDuración: " + Mathf.FloorToInt(_estallido), 2);
				else SetTextoDescripcion ("Energy failure!\nDuration: " + Mathf.FloorToInt(_estallido), 2);
			}
			if (_succion == false && _estallido > 0) {
				_estallido -= Time.deltaTime;
				if (_estallido < 0)
					_estallido = 0;
			} else if (_succion && _estallido > 0) {
				_estallido += 1;
				_succion = false;
				if (vars.dificultadActual >= 5) {//hardcore... explosion te destruye...
					vars.vidasActual--;
					SaveTraspasedVars.save();
					SceneManager.LoadScene ("Reinicio");
				}
				if (_estallido > 10) {
					vars.vidasActual--;
					vars.escudoEstallido++;
					_estallido -= 5;
					if (TraspasedVars.Lenguaje == "Spanish")SetTextoDescripcion ("¡Explosión interna!\nVidas: " + vars.vidasActual, 2);
					else SetTextoDescripcion ("Internal explosion!\nLifes: " + vars.vidasActual, 2);
					if (_bola.Count == 0 && vars.vidasActual == 0) {
						_playing = false;
						StartCoroutine ("ChequearVidas");
					}
				} else {
					if (TraspasedVars.Lenguaje == "Spanish")SetTextoDescripcion ("¡Colapso de energia!\nDuración: " + Mathf.FloorToInt(_estallido), 2);
					else SetTextoDescripcion ("Energy collapse!\nDuration: " + Mathf.FloorToInt(_estallido), 2);
				}
			} 
		}
	}

	IEnumerator ChequearVidas(){
		yield return new WaitForSeconds (2);
		Save_progress ();
	}

	public void Crear_Bola(int index){
		if (vars.vidasActual > 0 && !_pause && _bola.Count < 50 && ((index==0)||(index==1&&vars.perseguidorasActual>0)||(index==2&&vars.explosivasActual>0))) {
			if(vars.dificultadActual > 0)vars.vidasActual--;
			GameObject nuevabola = Instantiate (creador_de_niveles._bolaPrefab[index],
				new Vector3 (_pong.transform.position.x, _pong.transform.position.y+.5f),
				Quaternion.identity,
				_pong.transform);

			if (index == 1) {
				vars.perseguidorasUsadas++;
				vars.perseguidorasActual--;
				if (TraspasedVars.Lenguaje == "Spanish")SetTextoDescripcion ("Persegidora desplegada\nRestantes: "+vars.perseguidorasActual,2);
				else SetTextoDescripcion ("Seeker launched\nRemains: "+vars.perseguidorasActual,2);
			}
			if (index == 2) {
				vars.explosivasUsadas++;
				vars.explosivasActual--;
				if (TraspasedVars.Lenguaje == "Spanish")SetTextoDescripcion ("XP lanzada\nRestantes: "+vars.explosivasActual,2);
				else SetTextoDescripcion ("XP thrown\nRemains: "+vars.explosivasActual,2);
			}
			if (vars.velocidadPelota > _bolaMaxSpeed)
				vars.velocidadPelota = _bolaMaxSpeed;
			if (vars.velocidadPelota < _bolaMinSpeed)
				vars.velocidadPelota = _bolaMinSpeed;
			nuevabola.GetComponent<Rigidbody> ().velocity = new Vector3 (_bolaMinXspeed, vars.velocidadPelota);
			nuevabola.GetComponent<Rigidbody> ().velocity.Normalize ();
			//nuevabola.GetComponent<SpriteRenderer> ().color.a = 1;
			nuevabola.transform.parent = null;
			nuevabola.GetComponent<SpriteRenderer> ().color = vars.pong_color;
			nuevabola.transform.localScale = Vector3.one + Vector3.one;
			_bola.Add (nuevabola);
		}
	}

	public void Crear_Bola_Hacia(int index, Vector2 hacia_posicion){
		if (vars.vidasActual > 0 && !_pause && _bola.Count < 50 && ((index==0)||(index==1&&vars.perseguidorasActual>0)||(index==2&&vars.explosivasActual>0))) {
			if(vars.dificultadActual > 0)vars.vidasActual--;
			GameObject nuevabola = Instantiate (creador_de_niveles._bolaPrefab[index],
				new Vector3 (_pong.transform.position.x, _pong.transform.position.y+.5f),
				Quaternion.identity,
				_pong.transform);

			if (index == 1) {
				vars.perseguidorasUsadas++;
				vars.perseguidorasActual--;
				if (TraspasedVars.Lenguaje == "Spanish")SetTextoDescripcion ("Persegidora desplegada\nRestantes: "+vars.perseguidorasActual,2);
				else SetTextoDescripcion ("Seeker launched\nRemains: "+vars.perseguidorasActual,2);
			}
			if (index == 2) {
				vars.explosivasUsadas++;
				vars.explosivasActual--;
				if (TraspasedVars.Lenguaje == "Spanish")SetTextoDescripcion ("XP lanzada\nRestantes: "+vars.explosivasActual,2);
				else SetTextoDescripcion ("XP thrown\nRemains: "+vars.explosivasActual,2);
				nuevabola.GetComponent<SpriteRenderer> ().color = Color.red;
			}
			if (vars.velocidadPelota > _bolaMaxSpeed)
				vars.velocidadPelota = _bolaMaxSpeed;
			if (vars.velocidadPelota < _bolaMinSpeed)
				vars.velocidadPelota = _bolaMinSpeed;
			//Vector2 angulo = hacia_posicion/UnityEngine.Screen.width;
			//FindObjectOfType<OneButtonControl> ().SetTexto(" "+ hacia_posicion + " vectores " );
			//
		nuevabola.GetComponent<Rigidbody> ().velocity = hacia_posicion*vars.velocidadPelota;
			nuevabola.GetComponent<Rigidbody> ().velocity = new Vector3 (_bolaMinXspeed*Mathf.Sign(hacia_posicion.x), vars.velocidadPelota);
			//FindObjectOfType<OneButtonControl> ().SetTexto(" "+ nuevabola.GetComponent<Rigidbody> ().velocity + " velocidad " );
			nuevabola.GetComponent<Rigidbody> ().velocity.Normalize ();
			if(index!=2)nuevabola.GetComponent<SpriteRenderer> ().color = vars.pong_color;
			//nuevabola.GetComponent<SpriteRenderer> ().color.a = 1;
			nuevabola.transform.parent = null;
			//nuevabola.transform.localScale = Vector3.one+Vector3.one;
			nuevabola.transform.localScale = Vector3.one+Vector3.one;
			_bola.Add (nuevabola);
		}
	}


	public void Save_progress(){
		if (this.vars != null) {
			Debug.Log ("vars no es nulo");
			if (this.vars.vidasActual <= 0) {
				this.vars.vidasActual = 0;
				this.SaveTraspasedVars.save ();
				Debug.Log ("vars se salva con vidas 0");
				Debug.Log ("¡JUEGO TERMINADO!");
				//fondo_textMesh = SaveTraspasedVars.fondo_textMesh;
				fondo_script = SaveTraspasedVars.fondo_script;
				fondo = SaveTraspasedVars.fondo;

				//fondo_textMesh.text="Juego\nTerminado";
				_playing = false;
				_pause = false;
				//fondo.SetActive (true);
				//fondo_script.Boton_Musica.SetActive (false);
				//if (androidControls.activeInHierarchy)
				//	androidControls.SetActive (false);
				StartCoroutine ("Reinicio");
				//falta guardar el proceso de todos los demás atributos :v
			} else {
				this.SaveTraspasedVars.save ();
				Debug.Log ("vars se salva con vidas > 0");
			}
		}
		//Crear objeto para reproducir sonido de derrota... :v falta el sonido jajajajaja
	}

	IEnumerator Reinicio(){
		yield return new WaitForSeconds (0.1f);
		SceneManager.LoadScene ("Menu Principal");

	}

	public float _Activar_controles_time = 2;
	IEnumerator Activar_controles(){
		yield return new WaitForSeconds (_Activar_controles_time);
		//Activar_controles_funtion();
		androidControls.SetActive (true);
	}

	/*
	public void Activar_controles_defunq(){
		StopCoroutine ("Activar_controles");
	}

	public void Activar_controles_funtion(){
		Debug.Log ("iniciando...");
		if (vars.ocultar_ui) {
			//OneClickControlGO.SetActive (true);
			OneClickCanvasGO.SetActive (true);
			foreach (GameObject a in _bloques) {
				a.SetActive (true);
			}
			_pong.SetActive (true);
			_playing = true;
			OneClickOcultadorDelMensajeInicial.SetActive (false);
		}
		if (!vars.ocultar_ui) {
			//androidControls.SetActive (true);
			_playing = true;
		}
	}
	*/

	IEnumerator Set_playing(){
		yield return new WaitForSeconds (2);
		//_playing = true;
	}
}