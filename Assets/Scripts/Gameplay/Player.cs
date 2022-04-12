using UnityEngine;

public class Player : Singleton<Player>
{
	public PlayerMovement PlayerMovement { get; private set; }
	public PlayerController PlayerController { get; private set; }

	private void Awake()
	{
		PlayerController = GetComponent<PlayerController>();
		PlayerMovement = GetComponent<PlayerMovement>();
	}

	public void SwitchBlob()
	{
		Debug.Log("switch blob");
	}
}
