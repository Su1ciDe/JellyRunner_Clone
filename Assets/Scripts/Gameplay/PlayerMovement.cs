using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public bool CanMove { get; set; } = true;

	public Rigidbody Rb { get; private set; }

	[SerializeField] private float moveSpeed = 500;
	[SerializeField] private float moveSpeedMultiplier = 1;

	private void Awake()
	{
		Rb = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		Move();
	}

	private void Move()
	{
		if (!CanMove) return;
		Rb.velocity = moveSpeed * moveSpeedMultiplier * Time.fixedDeltaTime * Vector3.forward;
	}
}