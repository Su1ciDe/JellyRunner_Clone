using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{
	public bool HasReacted { get; set; }

	public bool DoesDamageToBigBlob;
	public bool DoesDamageToSmallBlob;
	public int Damage = 1;

	public virtual void React()
	{
		if (Player.Instance.BlobController.IsBigBlob)
		{
			ReactToBigBlob();
			if (DoesDamageToBigBlob)
				Player.Instance.BlobController.RemoveBlob(Damage);
		}
		else
		{
			ReactToSmallBlob();
			if (DoesDamageToSmallBlob)
				Player.Instance.BlobController.RemoveBlob(Damage);
		}

		HasReacted = true;
	}

	protected abstract void ReactToBigBlob();
	protected abstract void ReactToSmallBlob();

	private void OnTriggerStay(Collider other)
	{
		if (other.attachedRigidbody && other.attachedRigidbody.TryGetComponent(out Player player) && !HasReacted)
		{
			React();
		}
	}
}