using UnityEngine;

public class CollectableBlob : Collectable
{
	private Animator animator;
	private readonly int jumpingIdle = Animator.StringToHash("JumpingIdle");

	private void Awake()
	{
		animator = GetComponentInChildren<Animator>();
		animator.SetBool(jumpingIdle, true);
	}

	public override void OnCollect(Player collectorPlayer)
	{
		// particle etc.

		Destroy(gameObject);
	}
}