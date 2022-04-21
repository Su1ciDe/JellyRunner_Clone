using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{
	public bool HasReacted { get; set; }

	public bool DoesDamageToBigBlob;
	public bool DoesDamageToSmallBlob;

	public virtual void React(SmallBlob smallBlob)
	{
		if (Player.Instance.BlobController.IsBigBlob)
		{
			ReactToBigBlob();
			if (DoesDamageToBigBlob)
				Player.Instance.BlobController.RemoveBlob();
		}
		else
		{
			ReactToSmallBlob(smallBlob);
			if (DoesDamageToSmallBlob)
				Player.Instance.BlobController.RemoveBlob(smallBlob);
		}
	}

	protected abstract void ReactToBigBlob();
	protected abstract void ReactToSmallBlob(SmallBlob smallBlob);

	private void OnTriggerStay(Collider other)
	{
		if (other.isTrigger && other.attachedRigidbody && other.attachedRigidbody.TryGetComponent(out Blob blob) && !HasReacted)
		{
			HasReacted = true;
			React(blob.TryGetComponent(out SmallBlob smallBlob) ? smallBlob : null);
		}
	}
}