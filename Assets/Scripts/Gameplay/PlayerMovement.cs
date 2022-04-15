using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public bool CanMove { get; set; }

	public Rigidbody Rb { get; private set; }

	[SerializeField] private float moveSpeed = 500;
	[SerializeField] private float moveSpeedMultiplier = 1;

	private Vector3 velocity;

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
		velocity = Rb.velocity;
		velocity.z = moveSpeed * moveSpeedMultiplier * Time.fixedDeltaTime;
		Rb.velocity = velocity;
	}
}