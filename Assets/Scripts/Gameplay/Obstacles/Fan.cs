using DG.Tweening;
using UnityEngine;

public class Fan : Obstacle
{
	[Space]
	[SerializeField] private float upForce = 100;

	[Header("Motion")]
	[SerializeField] private Transform movingPart;
	[SerializeField] private float rotationTime = 1;

	private readonly int frontFlipAnim = Animator.StringToHash("FrontFlip");

	
	private void Start()
	{
		Motion();
	}

	private void Motion()
	{
		movingPart.DOLocalRotate(-360 * Vector3.forward + 90 * Vector3.right, rotationTime, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
	}

	protected override void ReactToBigBlob()
	{
	}

	protected override void ReactToSmallBlob()
	{
		// Fly
		Player.Instance.PlayerMovement.Rb.AddForce(upForce * Vector3.up, ForceMode.Impulse);
		foreach (SmallBlob smallBlob in Player.Instance.BlobController.SmallBlobs)
			smallBlob.Anim_SetTrigger(frontFlipAnim);
	}
}