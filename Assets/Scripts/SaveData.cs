using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

[Serializable]
public class SaveData{
	//PARTIDA GENERAL
	public string Nombre; // nombre del usuario
	public int dificultadTotal; // sandbox no se guarda, salvo que se completen todos los niveles en sandbox
	public int vecesJugadas; // el numero de veces que se ha iniciado el juego
	public int dificultadPromedio;// el numero de dificultades elegidas, sandbox no suma, facilucho suma 1, facil suma 2, normal 3, dificil 4, hardcore 5
	public int highScore; // el record del jugador en puntaje multiplicado dificultad
	public int levelsPlayedTotal;// el numero total de niveles superados entre todas las partidas jugadas
	public int nivelAlcanzadoMedio; // el numero de nivel que se suele llegar cada vez que se juega
	public int bloquesRotos; //el numero de bloques destruidos, no de vidas sacadas sino de destruidos
	public int bolasPerdidas; //el numero total de bolas que se fueron del juego y se perdieron incluyendo el modo hardcore a todas a la vez
	public int bolasAbsorvidas; //el numero total de bolas que se succionaron dentro del pong
	public int perseguidorasUsadas; // numero de perseguidoras que rompieron un bloque
	public int explosivasUsadas; // el numero de bloques que se hicieron añicos con las explosivas
	public int explosivasEscombrosDestruidos; // el numero de bloques con 0 puntos de vida que estallaron con las explosiones
	public int perforantesUsadas; // el numero de bloques que fueron traspasados por la pelota
	public int pildorasVidas; // el numero total de vidas agarradas
	public float tiempoEnAcelerado; // el tiempo que se paso el pong acelerado
	public float tiempoEnLargo; // el tiempo que se paso largo
	//public float velocidadMedia; // la velocidad promedio de la pelota
	public int escudoEstallido; // veces muerto el pong al sobrecargar el escudo

	//MENU PRINCIPAL
	public int dificultadMenuPrincipal; // La dificultad de la partida actual si no se tienen vidas para continuar
	//PARTIDA ACTUAL
	public Color pong_color; // el ultimo color elegido para jugar el juego
	public int levelActual; // el ultimo nivel jugado
	public int vidasActual; // el numero de pelotas por usar
	public int puntosLibres; // la cantidad de puntos a distribuir en la presente mision
	//0 sandbox, 1 facilucho, 2 facil, 3 normal, 4 dificil, 5 hardcore
	public int dificultadActual; // La dificultad de la partida actual
	public int perforantesActual; //el valor de pelotas perforantes
	public int explosivasActual;
	public int perseguidorasActual;
	public float largoActual; // largo en segundos que quedan
	public float aceleracionAlMaximoActual; // velocidad al máximo en segundos que quedan (no la velocidad real del pong si no de la pildorita)
	public int puntajeActual; // el puntaje obtenido hasta el momento
	public int puntajeSiguienteVida; // el puntaje que falta para ganar una vida
	public int puntajeSiguienteVidaMaximo; // el puntaje necesario para ganar una vida, total sin restas... es el valor que se asigna y se usa para calcular la proxima vida
	public float velocidadMaxPong; //el valor máximo que el pong puede alcanzar
	public float velocidadPelota; //el valor base de la bola al ser invocada
	public float tiempoDePartida; // la cantidad de segundos jugados desde que comenzo la partida
	public int escudoTiempo; // el tiempo usado el escudo para absorver pelotas
	//  PARTIDA ACTUAL BONUS OBTENIDOS...
	public float aceleracionBonus; // aumenta la velocidad inicial de movimiento y la velocidad de llegar al maximo
	public int explosivasBonus;
	public int dirigidasBonus;
	public float velocidadMaximaBonus; // aumenta la velocidad máxima que puede alcanzar el pong
	public float velocidadAtopeBonus; // en segundos para la pildora de velocidad
	public int dropeoBonus; // en chances segun la dificultad y demás --> falta instaurar y crearla
	public float largoBonus; // en tiempo
	public float largoTamañoBonus;
	public int scoreBonus; // aumenta el puntaje obtenido al romper bloques y agarrar pildoras de puntos
	public int energyBonus; // aumenta el limite de energia para usar el escudo antes de estallar
	public int perforantesRecuperadasContraBordes; //al chocar contra un borde recupera poder de penetración


	//OPCIONES DEL JUEGO E INTERFACE
	public float musicaVol;
	public float soundVol;

	public bool ocultar_ui; // determina si se juega en modo "sin UI", en PC usando teclados predefinidos y en celular con los "taps" o "toques" en pantalla
	public bool ocultar_textoTutorial_UIoculta;//  determina si el texto que dura 30 segundos iniciales se mostrará o no... si es "false" se muestra
}
