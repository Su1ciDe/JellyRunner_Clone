using UnityEngine;

public class Player : Singleton<Player>
{
	
	public PlayerController PlayerController { get; private set; }

	private void Awake()
	{
		PlayerController = GetComponent<PlayerController>();
	}
}
