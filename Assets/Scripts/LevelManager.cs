using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour 
{
	public void LoadLevel(string name)
	{
		Debug.Log ("level load requested");
		Brick.breakableCount = 0;
		Application.LoadLevel(name);
	}
	
	public void QuitLevel(string name)
	{
		Debug.Log ("Game Exited");
		Application.Quit();
	}
	
	
	public void LoadNextLevel()
	{
		Brick.breakableCount = 0;
		Application.LoadLevel(Application.loadedLevel + 1);
	} 
	
	public void BrickDestroyed()
	{
		if(Brick.breakableCount <= 0)
		{
			LoadNextLevel();
		}
	}
}
