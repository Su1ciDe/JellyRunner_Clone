using DG.Tweening;
using UnityEngine;

public class Wall : Obstacle
{
	[Space]
	[SerializeField] private Transform wall;
	[SerializeField] private float explosionForce = 100;
	[SerializeField] private float explosionRadius = 20;
	private Rigidbody[] fractureRbs;

	private void Start()
	{
		fractureRbs = new Rigidbody[wall.transform.childCount];
		for (int i = 0; i < wall.transform.childCount; i++)
			fractureRbs[i] = wall.transform.GetChild(i).GetComponent<Rigidbody>();
	}

	protected override void ReactToBigBlob()
	{
		foreach (Rigidbody fractureRb in fractureRbs)
		{
			fractureRb.isKinematic = false;
			fractureRb.AddExplosionForce(Random.Range(explosionForce / 2f, explosionForce), Player.Instance.transform.position + 1 * Vector3.up, Random.Range(explosionRadius / 2f, explosionRadius));
			fractureRb.transform.DOScale(0.01f, 2).SetDelay(5).SetEase(Ease.OutExpo).OnComplete(() => Destroy(fractureRb.gameObject));
		}
	}

	protected override void ReactToSmallBlob()
	{
		Player.Instance.BlobController.RemoveBlob(Player.Instance.BlobController.BlobCount);
	}
}