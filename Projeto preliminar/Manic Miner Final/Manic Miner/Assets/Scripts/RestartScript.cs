using UnityEngine;
using System.Collections;

public class RestartScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Space)){
			//A repor as variáveis globais
			xa.gameOver = false;
			AirBar.curAir = 100;
			//A apresentar a cena do nível de jogo
			Application.LoadLevel("game");
		}
	}
}
