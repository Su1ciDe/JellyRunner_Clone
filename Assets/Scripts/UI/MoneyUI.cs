using DG.Tweening;
using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI txtMoney;

	private void Start()
	{
		OnMoneyChanged(null);
	}

	private void OnEnable()
	{
		Player.Instance.OnCollectCoin += OnMoneyChanged;
	}

	private void OnDisable()
	{
		Player.Instance.OnCollectCoin -= OnMoneyChanged;
	}

	private void OnMoneyChanged(Coin coin)
	{
		Sequence seq = DOTween.Sequence();
		if (coin)
		{
			var imgCoin = ObjectPooler.Instance.Spawn("Coin", GameManager.MainCamera.WorldToScreenPoint(coin.transform.position), transform);
			seq.Append(imgCoin.transform.DOMove(txtMoney.transform.position, .5f).SetEase(Ease.InBack));
			seq.AppendCallback(() => imgCoin.SetActive(false));
		}

		seq.AppendCallback(() => txtMoney.SetText(Player.Money.ToString()));
	}
}