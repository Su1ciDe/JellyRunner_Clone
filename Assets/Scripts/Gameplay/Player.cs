using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Player : Singleton<Player>
{
	public int Money { get; private set; }

	public PlayerMovement PlayerMovement { get; private set; }
	public PlayerController PlayerController { get; private set; }
	public BlobController BlobController { get; private set; }

	public event UnityAction OnCollectCoin;

	private bool isInvulnerable;
	private readonly float invulnerableTime = 1;

	private void Awake()
	{
		PlayerController = GetComponent<PlayerController>();
		PlayerMovement = GetComponent<PlayerMovement>();
		BlobController = GetComponent<BlobController>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.isTrigger && other.attachedRigidbody && other.attachedRigidbody.TryGetComponent(out Coin coin))
		{
			AddMoney(coin.MoneyValue);
			coin.OnCollect(this);

			OnCollectCoin?.Invoke();
		}

		if (!isInvulnerable && other.attachedRigidbody && other.attachedRigidbody.TryGetComponent(out Obstacle obstacle))
		{
			Debug.Log(other.gameObject.name);
			StartCoroutine(Invulnerable());
			obstacle.React();
		}
	}


	private IEnumerator Invulnerable()
	{
		isInvulnerable = true;
		yield return new WaitForSeconds(invulnerableTime);
		isInvulnerable = false;
	}

	public void AddMoney(int value)
	{
		Money += value;
	}
}