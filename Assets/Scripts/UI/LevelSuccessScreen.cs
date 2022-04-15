using UnityEngine;

public class LevelSuccessScreen : MonoBehaviour
{
	public void NextLevel()
	{
		LevelManager.Instance.LoadLevel();
		SetActive(false);
	}

	public void SetActive(bool isActive)
	{
		gameObject.SetActive(isActive);
	}
}