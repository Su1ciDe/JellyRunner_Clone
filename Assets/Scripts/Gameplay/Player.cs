using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class Player : Singleton<Player>
{
	public bool IsFinished { get; set; }
	public static int Money
	{
		get => PlayerPrefs.GetInt("Money", 0);
		set => PlayerPrefs.SetInt("Money", value);
	}
	public int CollectedMoney { get; private set; } = 0;

	public PlayerMovement PlayerMovement { get; private set; }
	public PlayerController PlayerController { get; private set; }
	public BlobController BlobController { get; private set; }

	public static event UnityAction<Vector3> OnCollectCoin;

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
			AddMoney(coin.MoneyValue, coin.transform.position);
			coin.OnCollect(this);
		}
	}

	private void OnEnable()
	{
		LevelManager.OnLevelStart += OnLevelStart;
		LevelManager.OnLevelSuccess += OnLevelSuccess;
		LevelManager.OnLevelFail += OnLevelFail;
	}

	private void OnDisable()
	{
		LevelManager.OnLevelStart -= OnLevelStart;
		LevelManager.OnLevelSuccess -= OnLevelSuccess;
		LevelManager.OnLevelFail -= OnLevelFail;
	}

	private void OnLevelStart()
	{
		PlayerController.CanPlay = true;
		PlayerMovement.CanMove = true;
		BlobController.BigBlob.Anim_SetBool(BlobController.RunAnim, true);
	}

	private void OnLevelSuccess()
	{
		PlayerController.CanPlay = false;
		PlayerMovement.CanMove = false;
		PlayerMovement.Rb.velocity = Vector3.zero;
		BlobController.BigBlob.Anim_SetBool(BlobController.RunAnim, false);
		foreach (SmallBlob smallBlob in BlobController.SmallBlobs)
			smallBlob.Anim_SetBool(BlobController.RunAnim, false);
	}

	private void OnLevelFail()
	{
		PlayerController.CanPlay = false;
		PlayerMovement.CanMove = false;
		PlayerMovement.Rb.velocity = Vector3.zero;
		BlobController.BigBlob.Anim_SetBool(BlobController.RunAnim, false);
		foreach (SmallBlob smallBlob in BlobController.SmallBlobs)
			smallBlob.Anim_SetBool(BlobController.RunAnim, false);
	}

	public void AddMoney(int value, Vector3 pos = default)
	{
		CollectedMoney += value;
		Money += value;
		OnCollectCoin?.Invoke(pos);
	}

	public void PassFinishLine()
	{
		IsFinished = true;

		if (!BlobController.IsBigBlob)
			BlobController.SwitchBlob(false);
		BlobController.CanSwitchBlob = false;

		BlobController.BigBlob.transform.DOScale(0.01f, BlobController.BlobCount * 1.5f).SetId("FinishShrinking").OnComplete(() => LevelManager.Instance.GameSuccess());
	}
}