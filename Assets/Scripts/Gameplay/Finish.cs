using System.Collections;
using UnityEngine;

public class Finish : MonoBehaviour
{
	private TrailRenderer trail;
	private Coroutine drawTrail;

	private void Awake()
	{
		trail = GetComponentInChildren<TrailRenderer>();
	}

	private void OnEnable()
	{
		LevelManager.OnLevelSuccess += StopDrawTrail;
	}

	private void OnDisable()
	{
		LevelManager.OnLevelSuccess -= StopDrawTrail;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.attachedRigidbody && other.attachedRigidbody.TryGetComponent(out Player player) && !player.IsFinished)
		{
			player.FinishLine();
			StartDrawTrail();
		}
	}

	private void StartDrawTrail()
	{
		drawTrail = StartCoroutine(DrawTrail());
	}

	private void StopDrawTrail()
	{
		trail.emitting = false;
	}

	private IEnumerator DrawTrail()
	{
		WaitForFixedUpdate wait = new WaitForFixedUpdate();

		while (trail.emitting)
		{
			trail.transform.position = Player.Instance.transform.position + .02f * Vector3.up;

			yield return wait;
		}
	}
}