using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	private Paddle paddle;
	
	private bool hasStarted = false;
	private Vector3 paddleToBall; 

	// Use this for initialization
	void Start () 
	{
		paddle = GameObject.FindObjectOfType<Paddle>();
		paddleToBall = this.transform.position - paddle.transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{	
		if(!hasStarted)
		{
			this.transform.position = paddle.transform.position + paddleToBall;
			
			if(Input.GetMouseButtonDown(0))
			{
				hasStarted = true;
				this.GetComponent<Rigidbody2D>().velocity = new Vector2(2f,10f);
			}
		}	
	}
	
	void OnCollisionEnter2D(Collision2D collision)
	{	
		Vector2 tweak = new Vector2(Random.Range (-0.1f,0.1f),Random.Range(-0.1f,0.1f));
		
		if(hasStarted)
		{
			if(collision.gameObject == paddle.gameObject || !collision.gameObject.GetComponent<Brick>().isBreakable && !(collision.gameObject.tag == "wall"))
			{
				GetComponent<AudioSource>().Play();
			}
			GetComponent<Rigidbody2D>().velocity += tweak;
		}
	}	
}
