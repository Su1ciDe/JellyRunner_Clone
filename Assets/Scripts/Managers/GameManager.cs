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
		Debug.Log("game success");
	}

	public void GameFail()
	{
		Debug.Log("game fail");
	}
}