using DG.Tweening;
using UnityEngine;

public class Pendulum : Obstacle
{
	[Header("Motion")]
	[SerializeField] private float swingingAngle = 30;
	[SerializeField] private float swingingTime = 1;

	private void Start()
	{
		Motion();
	}

	protected override void ReactToBigBlob()
	{
	}

	protected override void ReactToSmallBlob()
	{
	}

	private void Motion()
	{
		transform.eulerAngles = -swingingAngle * Vector3.forward;
		transform.DORotate(swingingAngle * Vector3.forward, swingingTime).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
	}
}