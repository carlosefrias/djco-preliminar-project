using UnityEngine;
using System.Collections;

//Classe responsável pela criação de uma barra de tempo
public class AirBar : MonoBehaviour {
	
	private float maxAir = 100;
	public static float curAir = 100;
	private float airMissingSpeed = 2;
	private float airBarCurLength;
	
	private Texture2D airTexture;
	private GUIStyle airStyle;
	
	// Use this for initialization
	void Start () {
		//A inicializar a barra de tempo
		airBarCurLength = Screen.width / 2;
		airTexture = new Texture2D(1,1);
		airTexture.SetPixel(0,0, new Color(255,255,255));
		airStyle = new GUIStyle();
		airTexture.Apply();
		airStyle.normal.background = airTexture;
	}
	
	// Update is called once per frame
	void Update () 
	{
		//A atualizar a barra de tempo
		adjustCurrentAir(airMissingSpeed * Time.deltaTime);
	}
	void OnGUI()
	{	

		//A apresentar a barra de tempo
		GUI.Box(new Rect(0, 0, airBarCurLength, 20), "Air: "+(int) curAir + "/" + (int) maxAir, airStyle);
		
	}
	private void adjustCurrentAir(float adjust)
	{
		curAir -= adjust;
		
		//impedir que o ar corrente seja negativo
		if(curAir < 0) 
		{
			curAir = 0;
			Player.timeIsUp = true;
			//Terminou o jogo
			xa.gameOver = true;
			
		}
		else if(curAir > maxAir) curAir = maxAir;
		
		airBarCurLength = (Screen.width/2) * (curAir/maxAir);
	}
}
