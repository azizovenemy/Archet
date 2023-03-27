using System;
using UnityEngine;

public class UIWidgetButtonAnimator : MonoBehaviour
{
    public event Action OnAppearAnimationOverEvent;

    [SerializeField] private Animator animator;

    public void PlayHide()
    {
        animator.SetTrigger("disappear");
    }

    private void Handle_AppearAnimationOver()
    {
        OnAppearAnimationOverEvent?.Invoke();
    }
}
