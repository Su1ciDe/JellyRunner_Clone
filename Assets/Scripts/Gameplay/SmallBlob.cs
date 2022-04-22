using UnityEngine;

public class SmallBlob : Blob
{
	private void Awake()
	{
		Rb = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		if (!gameObject.activeSelf) return;
		if (!IsInStack) return;
		Rb.velocity = Player.Instance.PlayerMovement.Rb.velocity;
	}
}