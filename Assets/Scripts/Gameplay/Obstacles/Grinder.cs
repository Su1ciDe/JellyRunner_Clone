using DG.Tweening;
using UnityEngine;

public class Grinder : Obstacle
{
	[Header("Motion")]
	[SerializeField] private Transform movingPart;
	[SerializeField] private float rotationTime = 1;

	private void Start()
	{
		Motion();
	}

	private void Motion()
	{
		movingPart.DOLocalRotate(360 * Vector3.left, rotationTime, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
	}

	protected override void ReactToBigBlob()
	{
	}

	protected override void ReactToSmallBlob()
	{
	}

	private void OnDestroy()
	{
		movingPart.DOKill();
	}
}