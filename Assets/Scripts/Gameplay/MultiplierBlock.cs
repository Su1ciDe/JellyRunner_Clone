using DG.Tweening;
using TMPro;
using UnityEngine;

public class MultiplierBlock : MonoBehaviour
{
	[SerializeField] private int multiplier = 1;
	private TextMeshPro txtMultiplier;

	private void Awake()
	{
		txtMultiplier = GetComponentInChildren<TextMeshPro>();
		txtMultiplier.SetText("x" + multiplier);
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.attachedRigidbody && other.attachedRigidbody.TryGetComponent(out Player player))
		{
			DOTween.Kill("FinishShrinking");
			player.AddMoney(player.CollectedMoney * multiplier, player.transform.position);
			LevelManager.Instance.GameSuccess();
		}
	}
}