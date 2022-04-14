using DG.Tweening;
using UnityEngine;

public class BigBlob : Blob
{
	public int Size { get; set; }

	public void ChangeSize(int size, bool isAnimated = true)
	{
		Size = size;
		if (isAnimated)
		{
			transform.DOComplete();
			transform.DOScale(Size / 2f, .5f).SetEase(Ease.OutElastic);
		}
		else
		{
			transform.localScale = (Size / 2f) * Vector3.one;
		}
	}

}