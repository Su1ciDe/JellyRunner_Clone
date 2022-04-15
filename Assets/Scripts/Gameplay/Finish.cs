using UnityEngine;

public class Finish : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.isTrigger && other.attachedRigidbody && other.attachedRigidbody.TryGetComponent(out Player player))
		{
			player.FinishLine();
		}
	}
}