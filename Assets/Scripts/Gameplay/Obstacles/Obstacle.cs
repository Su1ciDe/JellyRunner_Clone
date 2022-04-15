using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{
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
	}

	protected abstract void ReactToBigBlob();
	protected abstract void ReactToSmallBlob();
}