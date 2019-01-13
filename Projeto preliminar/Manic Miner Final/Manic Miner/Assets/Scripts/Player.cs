using UnityEngine;
using System.Collections;

public class Player : Character 
{	
	private const int MAX_ITEMS_NUM = 4;
	private const int MAX_VIDAS_NUM = 3;
	private int numPickedItems = 0;
	private bool hasPickedAllItems = false;
	
	private bool isAlive = true;
	private int numLives;
	
	public static bool timeIsUp = false;
	
	//Tratamento do score do jogador...
	private const int POINTS_ITEM = 100;
	private const int POINTS_DEAD = 60;
	public OTTextSprite scoreProgressTxt;
	public static int scoreProgress;
	public GameObject[] finais;
	public GameObject[] gameover;
	
	// Use this for initialization
	public override void Start () 
	{
		scoreProgress = 0;
		numLives = MAX_VIDAS_NUM;
		
		finais = GameObject.FindGameObjectsWithTag("Final");
		foreach (GameObject go in finais) {
		    	go.renderer.enabled = false;
		    }
		gameover = GameObject.FindGameObjectsWithTag("GameOver");
		foreach (GameObject go in gameover) {
		    	go.renderer.enabled = false;
		    }
		base.Start();
		Win = false;
		spawnPos = thisTransform.position;
		GameObject.Find("item_key 1").gameObject.renderer.enabled = false;
		GameObject.Find("item_key 2").gameObject.renderer.enabled = false;
		GameObject.Find("item_key 3").gameObject.renderer.enabled = false;
		GameObject.Find("item_key 4").gameObject.renderer.enabled = false;			
	}
	
	// Update is called once per frame
	public void Update () 
	{
		// these are false unless one of keys is pressed
		isLeft = false;
		isRight = false;
		isJump = false;
		isPass = false;
		
		movingDir = moving.None;
		
		// keyboard input		
		if(Input.GetKey(KeyCode.LeftArrow)) 
		{ 
			isLeft = true; 
			facingDir = facing.Left;
		}
		if (Input.GetKey(KeyCode.RightArrow) && isLeft == false) 
		{ 
			isRight = true; 
			facingDir = facing.Right;
		}
		
		if (Input.GetKeyDown(KeyCode.UpArrow)) 
		{ 
			isJump = true; 
		}
		
		if(Input.GetKeyDown(KeyCode.DownArrow))
		{
			isPass = true;
		}
		
		UpdateMovement();
		if (Win) {
			//Apresentar a cena de gamewin
			Application.LoadLevel("gamewin");		
		} else if (xa.gameOver)
			//Apresentar a cena de gameover
			Application.LoadLevel("gameover");
	
		scoreProgressTxt.text = "Score: " + scoreProgress.ToString();
		
		Transform playerTransform = xa.player.transform;
		// get player position
		Vector3 position = playerTransform.position;
		if(position.y < -30 || position.x < -30 || position.y > 100 || position.x > 100) checkGameOver(numLives);
	}
	
	void OnTriggerEnter(Collider other)
	{	
		isRolante = false;
	
		if (other.gameObject.CompareTag("Ball"))
		{
			//Debug.Log("player has ball!!");
			PickUpBall();
		}
		else if(other.gameObject.CompareTag("Enemy"))
		{
			//Debug.Log("player is dead!!");
			xa.audioManager.PlayBeep2();
			//A atualisar score
			scoreProgress -= POINTS_DEAD;			
			checkGameOver(--numLives);
		}
		else if(other.gameObject.CompareTag("Failing"))
		{
			other.gameObject.animation.Play();
		}
		else if(other.gameObject.CompareTag("PickableItem"))
		{
			//Debug.Log("player picked an item!!");
			xa.audioManager.PlayBeep1();
			//A atualisar o score	
			scoreProgress += POINTS_ITEM;
			picked(++numPickedItems);
			//if(other.gameObject.Equals(GameObject.Find("key 1"))) Debug.Log("encontra!!!");
			if (other.gameObject.Equals(GameObject.Find("key 1")))
			{
				//Debug.Log("key 1!!");
				GameObject.Find("item_key 1").gameObject.renderer.enabled = true;
		 	}		
			else if (other.gameObject.Equals(GameObject.Find("key 2")))
			{
				//Debug.Log("key 2!!");
				GameObject.Find("item_key 2").gameObject.renderer.enabled = true;
		 	}	
			else if (other.gameObject.Equals(GameObject.Find("key 3")))
			{
				//Debug.Log("key 3!!");
				GameObject.Find("item_key 3").gameObject.renderer.enabled = true;
		 	}	
			else if (other.gameObject.Equals(GameObject.Find("key 4")))
			{
				//Debug.Log("key 4!!");
				GameObject.Find("item_key 4").gameObject.renderer.enabled = true;
		 	}	
			other.gameObject.SetActive(false);
			//Destroy(other.gameObject);
			isTurnRight = true;
			cont = 0;
			//Debug.Log("Girar!"); 
			
		}
		else if(other.gameObject.CompareTag("Rolante"))
		{
			isRolante = true;
			//Debug.Log("Rolando!"); 
		
		}
		else if(other.gameObject.CompareTag("Goal") && hasPickedAllItems && !timeIsUp)
		{
			//Neste caso o jogador ganhou!!!
			float airLeft = AirBar.curAir;
			int pointsAdded = (int) Mathf.Round(airLeft) * 10 * numLives;
			//Debug.Log("pontos adicionados " + pointsAdded);
			//A atualizar score
			scoreProgress += pointsAdded;
			xa.gameOver = true;
			Win = true;
			
		}   

		
	}
	
	public void Respawn()
	{
		if(alive == true)
		{
			thisTransform.position = spawnPos;
			hasBall = false;
			rayDistUp = 0.375f;
		}
	}
		
	private void picked(int n)
	{
		if(n == MAX_ITEMS_NUM)
		{
			hasPickedAllItems = true;
			xa.audioManager.PlayWin();
			//Debug.Log("player picked all items!!");
		}
	}
	private void checkGameOver(int numVidas)
	{
		//a diminuir o número de vidas (no ecrã do jogo)
		switch(numVidas)
		{
			case 2: 
				Destroy(GameObject.Find("miner life 3 sprite"));
				StartNextRound();
				break;
			case 1:
				Destroy(GameObject.Find("miner life 2 sprite"));
				StartNextRound();
				break;
			case 0:
				Destroy(GameObject.Find("miner life 1 sprite"));
				xa.gameOver = true;
				//Debug.Log ("GAME OVER!!!");
				break;
		}
	}
	
	void StartNextRound()
	{
		xa.player.Respawn();
		xa.ball.ResetBall();
		xa.gameOver = false;
 	}
}
