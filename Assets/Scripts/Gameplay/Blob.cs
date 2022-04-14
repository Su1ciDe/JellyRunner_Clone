using UnityEngine;

public abstract class Blob : MonoBehaviour
{
	[SerializeField] protected Animator Animator;
	
	public void Anim_SetBool(int parameter, bool value)
	{
		Animator.SetBool(parameter, value);
	}

	public void Anim_SetTrigger(int parameter)
	{
		Animator.SetTrigger(parameter);
	}
}