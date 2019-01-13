using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour 
{
	public ParticleSystem particleVFX;
	
	private Transform thisTransform;
	private Rigidbody thisRigidbody;
	
	protected float fireRate = 0.05f;//0.1F;
    protected float nextFire = 0.0F;
	
	private bool hasBall = false;
	private bool scoringPoints = false;
	
	private Color orange = new Color(0.94f,0.59f,0f);
	private Color blue = new Color(0f,0.69f,94f);
	private Color green = new Color(0.76f,1f,0f);
	
	void Awake()
	{
		thisTransform = transform;
		thisRigidbody = rigidbody;
	}
	
	// Use this for initialization
	IEnumerator Start () 
	{
		yield return new WaitForSeconds(0.1f);
		ResetBall();
	}
	
	public void PickUp(Transform trans)
	{
		thisRigidbody.isKinematic = true;
		thisRigidbody.useGravity = false;
		thisTransform.position = new Vector3(trans.position.x, trans.position.y + 0.65f, trans.position.z);
		thisTransform.parent = trans;
		xa.audioManager.PlayPickup();
		hasBall = true;
		scoringPoints = false;
		particleVFX.startColor = blue;
	}
	
	public void ResetBall()
	{
		thisTransform.parent = null;
		thisRigidbody.isKinematic = false;
		thisRigidbody.useGravity = true;
		thisTransform.position = new Vector3(0,3.5f,0);
		hasBall = false;
		scoringPoints = false;
		particleVFX.startColor = green;		
	}
	
	public void Pass(float velX)
	{
		thisTransform.parent = null;
		thisRigidbody.isKinematic = false;
		thisRigidbody.useGravity = true;
		rigidbody.velocity = new Vector3(velX, 10, 0);
		xa.audioManager.PlayPass();
		hasBall = false;
		particleVFX.startColor = green;
	}
	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.CompareTag("Enemy"))
		{
			//Debug.Log("ball hit enemy!!");
			xa.audioManager.PlayBeep1();
			Destroy(other.gameObject);
			//A atualizar o score do jogador
			Player.scoreProgress -= 300;
		}
	}
}
