using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI txtMoney;

	private void OnEnable()
	{
		Player.Instance.OnCollectCoin += OnMoneyChanged;
	}

	private void OnDisable()
	{
		Player.Instance.OnCollectCoin -= OnMoneyChanged;
	}

	private void OnMoneyChanged()
	{
		txtMoney.SetText(Player.Money.ToString());
	}
}