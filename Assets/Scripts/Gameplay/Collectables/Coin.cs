using DG.Tweening;
using UnityEngine;

public class Coin : Collectable
{
	public int MoneyValue = 1;

	[Space]
	[SerializeField] private Transform coinT;

	private void Start()
	{
		coinT.DOLocalRotate(360 * Vector3.up, 2.5f, RotateMode.FastBeyond360).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart).SetDelay(Random.Range(0f, 1f));
	}

	public override void OnCollect(Player collectorPlayer)
	{
		var particle = ObjectPooler.Instance.Spawn("CoinCollect", transform.position).GetComponent<ParticleSystem>();
		particle.Play();
		Destroy(gameObject);
	}
}