using UnityEngine;
using UnityEngine.EventSystems;

public class TapToStart : MonoBehaviour
{
	private EventTrigger tapToStartEventTrigger;

	private void Awake()
	{
		tapToStartEventTrigger = GetComponent<EventTrigger>();
		EventTrigger.Entry entry = new EventTrigger.Entry();
		entry.eventID = EventTriggerType.PointerDown;
		entry.callback.AddListener(eventData => StartLevel());
		tapToStartEventTrigger.triggers.Add(entry);
	}

	public void StartLevel()
	{
		LevelManager.Instance.StartLevel();
		SetActiveUI(false);
	}

	public void SetActiveUI(bool isActive)
	{
		gameObject.SetActive(isActive);
	}
}