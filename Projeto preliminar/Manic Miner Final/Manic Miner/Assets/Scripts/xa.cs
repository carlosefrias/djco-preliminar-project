using UnityEngine;
using System.Collections;

public class xa : MonoBehaviour 
{
	public static Ball ball;
	public static AudioManager audioManager;
	
	public static Player player;
	
	public static bool gameOver = false;
	
	// set to false if game is configured for CTF
	public static bool letsPlayKeepaway = false; // layers
	
	void Start()
	{
		// cache these so they can be accessed by other scripts
		ball = GameObject.FindWithTag("Ball").GetComponent<Ball>();
		audioManager = gameObject.GetComponent<AudioManager>();		
		player = GameObject.Find("player").GetComponent<Player>();
	}
}
