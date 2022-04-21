using UnityEngine;

public class SmallBlob : Blob
{
	public Rigidbody Rb { get; private set; }

	private void Awake()
	{
		Rb = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		if (!gameObject.activeSelf) return;
		Rb.velocity = Player.Instance.PlayerMovement.Rb.velocity;
	}
}