using DG.Tweening;
using UnityEngine;

public class BigBlob : MonoBehaviour
{
	public int Size { get; set; }

	public void ChangeSize(int size)
	{
		Size = size;
		transform.DOComplete();
		transform.DOScale(Size / 2f, .5f).SetEase(Ease.OutElastic);
	}
}