using UnityEngine;
using System.Collections;

public class Brick : MonoBehaviour {

	//public GameObject smoke;
	
	private LevelManager levelManager;
	private int timesHit;
	
	public static int breakableCount = 0;
	public AudioClip crack;
	public Sprite[] hitSprites;
	public bool isBreakable;
	// Use this for initialization
	void Start () {
		isBreakable = (this.tag == "Breakable");
		
		if(isBreakable)
		{
			breakableCount++;
		}
		
		timesHit = 0;
		levelManager = GameObject.FindObjectOfType<LevelManager>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter2D (Collision2D col)
	{
		if (isBreakable)
		{	
			AudioSource.PlayClipAtPoint(crack,transform.position);
			timesHit++;
			int maxHits = hitSprites.Length + 1;
			if(timesHit >= maxHits)
			{
				breakableCount--;
				levelManager.BrickDestroyed();
				//GameObject smokePuff = Instantiate(smoke,gameObject.transform.position,Quaternion.identity) as GameObject;
				//smokePuff.particleSystem.startColor = gameObject.GetComponent<SpriteRenderer>().color;
				Destroy(gameObject);
			}
			else
			{
				LoadSprites();
			}
		}
	}
	
	void LoadSprites()
	{
		int spriteIndex = timesHit - 1;	
		if(hitSprites[spriteIndex])
		{
			this.GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
		}
	}
}