using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class BlobController : MonoBehaviour
{
	public bool CanSwitchBlob { get; set; } = true;
	public bool IsBigBlob { get; set; } = true;
	public int BlobCount { get; private set; } = 2;

	public List<SmallBlob> SmallBlobs { get; set; } = new List<SmallBlob>();
	public BigBlob BigBlob { get; set; }

	public event UnityAction OnCollectBlob;

	private void Awake()
	{
		BigBlob = GetComponentInChildren<BigBlob>(true);
		SmallBlobs = GetComponentsInChildren<SmallBlob>(true).ToList();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.isTrigger && other.attachedRigidbody && other.attachedRigidbody.TryGetComponent(out CollectableBlob collectableBlob))
		{
			AddBlob();
			collectableBlob.OnCollect(Player.Instance);

			OnCollectBlob?.Invoke();
		}
	}

	public void AddBlob()
	{
		BlobCount++;

		if (IsBigBlob)
		{
			BigBlob.ChangeSize(BlobCount);
		}
		else
		{
		}
	}

	public void RemoveBlob()
	{
		BlobCount--;

		if (IsBigBlob)
		{
			if (BlobCount.Equals(1))
			{
				CanSwitchBlob = false;
				//
			}
		}
		else
		{
		}

		if (BlobCount <= 0)
		{
			GameManager.Instance.GameFail();
		}
	}

	public void SwitchBlob()
	{
		if (!CanSwitchBlob) return;

		IsBigBlob = !IsBigBlob;

		Debug.Log("switch blob");
	}
}