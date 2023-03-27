using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWidgetButton : MonoBehaviour
{
    [SerializeField] private UIWidgetButtonAnimator animator;
    [SerializeField] private float lifeTime = 3f;

    private void OnEnable()
    {
        animator.OnAppearAnimationOverEvent += OnAppearAnimationOver;
    }

    private void OnDisable()
    {
        animator.OnAppearAnimationOverEvent -= OnAppearAnimationOver;
    }

    private void OnAppearAnimationOver()
    {
        Coroutines.StartRoutine(LifeRoutine());
    }
    
    private IEnumerator LifeRoutine()
    {
        yield return new WaitForSeconds(lifeTime);
        animator.PlayHide();
    }
}
