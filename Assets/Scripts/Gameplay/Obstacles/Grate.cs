using UnityEngine;

public class Grate : Obstacle
{
	[Space]
	[SerializeField] private BoxCollider grateCollider;

	protected override void ReactToBigBlob()
	{
		grateCollider.enabled = true;
	}

	protected override void ReactToSmallBlob(SmallBlob smallBlob)
	{
		grateCollider.enabled = false;
	}
}