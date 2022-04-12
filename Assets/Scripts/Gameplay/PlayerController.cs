using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private bool isHolding;

	[SerializeField] private float dragMultiplier;
	private float previousMousePosX;
	private float mouseDelta;

	[Space]
	[SerializeField] private float holdingTimeThreshold = .05f;
	private float holdingTime;

	private PlayerMovement movement => Player.Instance.PlayerMovement;

	private void Update()
	{
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

			if (holdingTime < holdingTimeThreshold)
				Player.Instance.SwitchBlob();

			holdingTime = 0;

			// Reset movement
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
		if (!isHolding || holdingTime < holdingTimeThreshold) return;

		mouseDelta = Input.mousePosition.x - previousMousePosX;
		Vector3 velocity = movement.Rb.velocity;
		velocity.x = mouseDelta * dragMultiplier * Time.fixedDeltaTime;
		movement.Rb.velocity = velocity;

		previousMousePosX = Input.mousePosition.x;
	}
}