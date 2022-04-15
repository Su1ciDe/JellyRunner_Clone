using DG.Tweening;
using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI txtMoney;
	[SerializeField] private Transform imageTarget;

	private void Start()
	{
		OnMoneyChanged();
	}

	private void OnEnable()
	{
		Player.Instance.OnCollectCoin += OnMoneyChanged;
	}

	private void OnDisable()
	{
		Player.Instance.OnCollectCoin -= OnMoneyChanged;
	}

	private void OnMoneyChanged(Vector3 animPos = default)
	{
		Sequence seq = DOTween.Sequence();
		if (!animPos.Equals(default))
		{
			var imgCoin = ObjectPooler.Instance.Spawn("Coin", GameManager.MainCamera.WorldToScreenPoint(animPos), transform);
			seq.Append(imgCoin.transform.DOMove(imageTarget.position, .5f).SetEase(Ease.InBack));
			seq.Append(imageTarget.DOPunchScale(Vector3.one * .9f, .2f, 2, .5f));
			seq.AppendCallback(() => imgCoin.SetActive(false));
		}

		seq.AppendCallback(() => txtMoney.SetText(Player.Money.ToString()));
	}
}