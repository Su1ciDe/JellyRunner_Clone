using DG.Tweening;
using UnityEngine;

public class FanSide : Obstacle
{
	[Space]
	[SerializeField] private float force = 100;

	[Header("Motion")]
	[SerializeField] private Transform movingPart;
	[SerializeField] private float rotationTime = 1;

	private void Start()
	{
		Motion();
	}

	private void Motion()
	{
		movingPart.DOLocalRotate(360 * Vector3.right, rotationTime, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
	}

	protected override void ReactToBigBlob()
	{
	}

	protected override void ReactToSmallBlob()
	{
		// Fly
		Player.Instance.PlayerMovement.Rb.AddForce(force * transform.right, ForceMode.Impulse);
	}
}