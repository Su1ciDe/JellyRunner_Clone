using DG.Tweening;
using UnityEngine;

public class BigBlob : Blob
{
	public float Size { get; private set; }
	public Rigidbody Rb { get; private set; }

	private void Awake()
	{
		Rb = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		if (!gameObject.activeSelf) return;
		Rb.velocity = Player.Instance.PlayerMovement.Rb.velocity;
	}


	public void ChangeSize(int size, bool isAnimated = true)
	{
		var model = transform.GetChild(0);
		Size = size / 2f;
		if (isAnimated)
		{
			model.DOComplete();
			model.DOScale(Size, .5f).SetEase(Ease.OutElastic);
		}
		else
		{
			model.localScale = Size * Vector3.one;
		}
	}
}