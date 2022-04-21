using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
	public LevelsSO LevelSO;

	public static int CurrentLevel
	{
		get => PlayerPrefs.GetInt("CurrentLevel", 0);
		set => PlayerPrefs.SetInt("CurrentLevel", value);
	}
	public int CurrentLevelIndex => CurrentLevel % LevelSO.Scenes.Count;

	public static event UnityAction OnLevelStart;
	public static event UnityAction OnLevelSuccess;
	public static event UnityAction OnLevelFail;

	public void StartLevel()
	{
		OnLevelStart?.Invoke();
	}

	public void GameSuccess()
	{
		CurrentLevel++;
		OnLevelSuccess?.Invoke();
	}

	public void GameFail()
	{
		OnLevelFail?.Invoke();
	}

	public void LoadLevel()
	{
		SceneManager.LoadScene(LevelSO.Scenes[CurrentLevelIndex].name);
	}
}