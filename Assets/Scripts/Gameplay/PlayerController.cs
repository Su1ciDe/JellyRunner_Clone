using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public bool CanPlay { get; set; }
	private bool isHolding;

	[SerializeField] private float dragMultiplier;
	private float previousMousePosX;
	private float mouseDelta;

	[Space]
	[SerializeField] private float holdingTimeThreshold = .1f;
	private float holdingTime;

	private PlayerMovement movement => Player.Instance.PlayerMovement;

	private void Update()
	{
		if (!CanPlay) return;
		if (Input.GetMouseButton(0))
		{
			if (isHolding)
				holdingTime += Time.deltaTime;
		}

		if (Input.GetMouseButtonDown(0))
		{
			isHolding = true;
			previousMousePosX = Input.mousePosition.x;
		}

		if (Input.GetMouseButtonUp(0))
		{
			isHolding = false;

			// Tap
			if (holdingTime < holdingTimeThreshold)
				Player.Instance.BlobController.SwitchBlob();

			holdingTime = 0;

			// Stop left/right movement on release
			Vector3 velocity = movement.Rb.velocity;
			velocity.x = 0;
			movement.Rb.velocity = velocity;
		}
	}

	private void FixedUpdate()
	{
		Drag();
	}

	private void Drag()
	{
		if(!CanPlay) return;
		if (!isHolding || holdingTime < holdingTimeThreshold / 2f) return;

		mouseDelta = Input.mousePosition.x - previousMousePosX;
		Vector3 velocity = movement.Rb.velocity;
		velocity.x = mouseDelta * dragMultiplier * Time.fixedDeltaTime;
		movement.Rb.velocity = velocity;

		previousMousePosX = Input.mousePosition.x;
	}
}