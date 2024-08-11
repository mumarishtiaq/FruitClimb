using DG.Tweening;
using UnityEngine;

public static class  TweenExtensions
{
    public static void TweenScale(this Transform t, Vector3 targetScale, float duration = 0, float delay = 0, Ease easeCurve = Ease.Unset, TweenCallback action = null)
    {
        t.DOScale(targetScale, duration).
           SetDelay(delay).
           SetEase(easeCurve).
           OnComplete(action);
    }

    public static void TweenAlpha(this CanvasGroup cg, float endValue, float duration = 0, float delay = 0, Ease easeCurve = Ease.Unset, TweenCallback action = null)
    {
        // Animate the alpha value of the CanvasGroup
        cg.DOFade(endValue, duration).
           SetDelay(delay).
           SetEase(easeCurve).
           OnComplete(action);
    }
}
