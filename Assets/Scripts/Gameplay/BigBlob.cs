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
		Size = size / 2f;
		if (isAnimated)
		{
			transform.DOComplete();
			transform.DOScale(Size, .5f).SetEase(Ease.OutElastic);
		}
		else
		{
			transform.localScale = Size * Vector3.one;
		}
	}
}