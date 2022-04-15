using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : Singleton<ObjectPooler>
{
	[Serializable]
	public class Pool
	{
		[Tooltip("Give a tag to the pool for calling it")]
		public string Tag;
		[Tooltip("The prefab to be pooled")]
		public GameObject Prefab;
		[Tooltip("The size (count) of the pool")]
		public int Size;
	}

	[SerializeField] private List<Pool> pools = new List<Pool>();
	private Dictionary<string, Queue<GameObject>> poolDictionary = new Dictionary<string, Queue<GameObject>>();

	private void Awake()
	{
		InitPool();
	}

	private void InitPool()
	{
		foreach (Pool pool in pools)
			AddToPool(pool.Tag, pool.Prefab, pool.Size);
	}
	
	public GameObject Spawn(string poolTag, Vector3 position)
	{
		GameObject obj = SpawnFromPool(poolTag);

		obj.transform.position = position;
		return obj;
	}
	
	public GameObject Spawn(string poolTag, Vector3 position, Quaternion rotation)
	{
		GameObject obj = SpawnFromPool(poolTag);

		obj.transform.position = position;
		obj.transform.rotation = rotation;
		return obj;
	}
	
	public GameObject Spawn(string poolTag, Vector3 position, Transform parent)
	{
		GameObject obj = SpawnFromPool(poolTag);

		obj.transform.position = position;
		obj.transform.forward = parent.forward;
		obj.transform.SetParent(parent);
		return obj;
	}
	
	private GameObject SpawnFromPool(string poolTag)
	{
		if (!poolDictionary.ContainsKey(poolTag)) return null;

		GameObject obj = poolDictionary[poolTag].Dequeue();
		obj.SetActive(true);
		poolDictionary[poolTag].Enqueue(obj);
		return obj;
	}
	
	public void AddToPool(string poolTag, GameObject prefab, int count)
	{
		if (poolDictionary.ContainsKey(poolTag))
		{
			Debug.LogWarning(gameObject.name + ": \"" + poolTag + "\" Tag has already exists! Skipped.");
			return;
		}

		Queue<GameObject> queue = new Queue<GameObject>();
		for (int i = 0; i < count; i++)
		{
			GameObject obj = Instantiate(prefab, transform);
			obj.SetActive(false);
			queue.Enqueue(obj);
		}

		poolDictionary.Add(poolTag, queue);
	}
}