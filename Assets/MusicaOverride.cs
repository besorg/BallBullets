using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicaOverride : MonoBehaviour {
	Dropdown dropdownMusicas;
	Sonido_control musicaReproductor;

	public Text LicenceText, LicenceLinkText;
	public Image linkButton0, linkButton1;

	void Start () {
		dropdownMusicas = gameObject.GetComponent<Dropdown> ();
		musicaReproductor = GameObject.Find ("Musica").GetComponent<Sonido_control>();

		LicenceText.text = "";
		LicenceLinkText.text = "";
		linkButton0.enabled = false;
		linkButton1.enabled = false;
	}

	void Update () {
		
	}

	public void OpenURL(string link){
		Application.OpenURL (link);
	}

	public void Point(int mod){
		if (dropdownMusicas.value + mod < 0)
			dropdownMusicas.value = dropdownMusicas.options.Count - 1;
		else if (dropdownMusicas.value + mod > dropdownMusicas.options.Count - 1)
			dropdownMusicas.value = 0;
		else dropdownMusicas.value+=mod;
		ReproducirMusica ();
		dropdownMusicas.captionText.text = dropdownMusicas.options[dropdownMusicas.value].text;
	}

	public void ReproducirMusica(){
		if (dropdownMusicas.value == 0) {
			musicaReproductor.CargarTemaPrincipal ();
			LicenceText.text = "";
			LicenceLinkText.text = "";
			linkButton0.enabled = false;
			linkButton1.enabled = false;
		} else {
			musicaReproductor.CargarMusicaSiguiente (dropdownMusicas.value-1);
			LicenceText.text = "\"" + musicaReproductor.Musicas[dropdownMusicas.value-1].name + "\" - Kevin MacLeod\n \nLicensed under Creative Commons:\nBy Attribution 3.0 License\n ";
			LicenceLinkText.text = "\nincompetech.com\n\n\nhttp://creativecommons.org/licenses/by/3.0/";
			linkButton0.enabled = true;
			linkButton1.enabled = true;
			//https://www.patreon.com/join/BeSO
			//https://www.twitch.tv/bsa_
			//https://www.youtube.com/channel/UCbTYudlOI4uUr0L4Lhko8Tw
		}
	}
}
