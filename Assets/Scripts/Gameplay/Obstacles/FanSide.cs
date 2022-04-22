using System.Collections;
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

	protected override void ReactToSmallBlob(SmallBlob smallBlob)
	{
		// Fly
		Player.Instance.PlayerMovement.Rb.AddForce(force * transform.right, ForceMode.VelocityChange);
		smallBlob.IsInStack = false;
		smallBlob.Rb.AddForce(new Vector3(force * 2, force , 0), ForceMode.VelocityChange);
		smallBlob.Rb.AddTorque(new Vector3(force, force, force));
		StartCoroutine(KillSmallBlob(smallBlob));
	}

	private IEnumerator KillSmallBlob(SmallBlob smallBlob)
	{
		yield return new WaitForSeconds(1);
		Player.Instance.BlobController.RemoveBlob(smallBlob);
	}

	private void OnDestroy()
	{
		movingPart.DOKill();
	}
}