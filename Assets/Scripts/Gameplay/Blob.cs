using UnityEngine;

public abstract class Blob : MonoBehaviour
{
	protected Animator Animator;

	public virtual void Awake()
	{
		Animator = GetComponentInChildren<Animator>(true);
	}

	public void Anim_SetBool(int parameter, bool value)
	{
		Animator.SetBool(parameter, value);
	}

	public void Anim_SetTrigger(int parameter)
	{
		Animator.SetTrigger(parameter);
	}
}