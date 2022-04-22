using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Fan : Obstacle
{
	[Space]
	[SerializeField] private float force = 250;
	[SerializeField] private float secondForceTime = 2;

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

	protected override void ReactToSmallBlob(SmallBlob smallBlob)
	{
		StartCoroutine(Fly(0));
		StartCoroutine(Fly(secondForceTime));
	}

	private IEnumerator Fly(float wait)
	{
		yield return new WaitForSeconds(wait);
		Player.Instance.PlayerMovement.Rb.AddForce(new Vector3(0, force, force * 2f), ForceMode.VelocityChange);
		foreach (SmallBlob _smallBlob in Player.Instance.BlobController.SmallBlobs)
			_smallBlob.Anim_SetTrigger(frontFlipAnim);
	}

	private void OnDestroy()
	{
		movingPart.DOKill();
	}
}