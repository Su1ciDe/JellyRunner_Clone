using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	public static Camera MainCamera;

	private void Awake()
	{
		MainCamera = Camera.main;
	}

	public void GameSuccess()
	{
		
	}
	
	public void GameFail()
	{
		
	}
}