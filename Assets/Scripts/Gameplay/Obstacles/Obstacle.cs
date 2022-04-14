using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{
	public bool DoesDamageToBigBlob;
	public bool DoesDamageToSmallBlob;

	public virtual void React()
	{
		if (Player.Instance.BlobController.IsBigBlob)
		{
			ReactToBigBlob();
			if (DoesDamageToBigBlob)
				Player.Instance.BlobController.RemoveBlob();
		}
		else
		{
			ReactToSmallBlob();
			if (DoesDamageToSmallBlob)
				Player.Instance.BlobController.RemoveBlob();
		}
	}

	protected abstract void ReactToBigBlob();
	protected abstract void ReactToSmallBlob();
}