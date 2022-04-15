using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class BlobController : MonoBehaviour
{
	public bool CanSwitchBlob { get; set; } = true;
	public bool IsBigBlob { get; set; } = true;
	public int BlobCount { get; private set; } = 2;

	public List<SmallBlob> SmallBlobs { get; set; } = new List<SmallBlob>();
	public BigBlob BigBlob { get; set; }

	public readonly int RunAnim = Animator.StringToHash("Running");

	public static event UnityAction OnCollectBlob;

	private void Awake()
	{
		BigBlob = GetComponentInChildren<BigBlob>(true);
		SmallBlobs = GetComponentsInChildren<SmallBlob>(true).ToList();
	}

	private void Start()
	{
		ArrangeSmallBlobs();
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

	public void AddBlob(int count = 1)
	{
		BlobCount += count;

		BigBlob.ChangeSize(BlobCount);

		if (IsBigBlob)
		{
			AddSmallBlob(false);
		}
		else
		{
			AddSmallBlob();
			ArrangeSmallBlobs();

			BigBlob.ChangeSize(BlobCount, false);
		}

		if (BlobCount > 1)
			CanSwitchBlob = true;
	}

	public void RemoveBlob(int count = 1)
	{
		count = count > BlobCount ? BlobCount : count;
		BlobCount -= count;

		BigBlob.ChangeSize(BlobCount);
		for (int i = 0; i < count; i++)
		{
			var smallBlob = SmallBlobs[SmallBlobs.Count - 1];
			SmallBlobs.Remove(smallBlob);
			Destroy(smallBlob.gameObject);
		}

		if (IsBigBlob)
		{
			if (BlobCount.Equals(1))
			{
				CanSwitchBlob = false;
			}
		}
		else
		{
			ArrangeSmallBlobs();
		}

		if (BlobCount <= 0)
		{
			LevelManager.Instance.GameFail();
		}
	}

	private void AddSmallBlob(bool isActive = true)
	{
		var newSmallBlob = Instantiate(SmallBlobs[0], SmallBlobs[0].transform.parent);
		newSmallBlob.gameObject.SetActive(isActive);
		newSmallBlob.Anim_SetBool(RunAnim, isActive);
		SmallBlobs.Add(newSmallBlob);
	}

	public void SwitchBlob()
	{
		if (!CanSwitchBlob) return;

		IsBigBlob = !IsBigBlob;
		if (IsBigBlob)
		{
			BigBlob.gameObject.SetActive(true);
			BigBlob.Anim_SetBool(RunAnim, true);

			foreach (SmallBlob smallBlob in SmallBlobs)
			{
				smallBlob.gameObject.SetActive(false);
				smallBlob.Anim_SetBool(RunAnim, false);
			}
		}
		else
		{
			BigBlob.Anim_SetBool(RunAnim, false);
			BigBlob.gameObject.SetActive(false);

			foreach (SmallBlob smallBlob in SmallBlobs)
			{
				smallBlob.gameObject.SetActive(true);
				smallBlob.Anim_SetBool(RunAnim, true);
			}

			ArrangeSmallBlobs();
		}
	}

	// Arrange small blobs around a circle
	public void ArrangeSmallBlobs(float radius = 1)
	{
		int count = SmallBlobs.Count;
		for (int i = 0; i < count; i++)
		{
			float radians = 2 * Mathf.PI / count * i;
			float vertical = Mathf.Sin(radians);
			float horizontal = Mathf.Cos(radians);

			Vector3 spawnDir = new Vector3(horizontal, 0, vertical);
			Vector3 spawnPos = spawnDir * radius;

			SmallBlobs[i].transform.localPosition = spawnPos;
		}
	}
}