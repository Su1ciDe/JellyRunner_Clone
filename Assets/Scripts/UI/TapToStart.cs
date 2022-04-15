using UnityEngine;

public class TapToStart : MonoBehaviour
{
	public void StartLevel()
	{
		LevelManager.Instance.StartLevel();
		SetActive(false);
	}

	public void SetActive(bool isActive)
	{
		gameObject.SetActive(isActive);
	}
}