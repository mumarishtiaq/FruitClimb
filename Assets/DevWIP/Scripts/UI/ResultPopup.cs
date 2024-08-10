using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultPopup : MonoBehaviour
{
    [SerializeField] private Transform _successPopup;
    [SerializeField] private List<Transform> _earnedStars;
    [SerializeField] private GameObject _background;
    [SerializeField] private Transform _colorWheel;


    [SerializeField] private GameObject _failedPopup;

    private void Start()
    {
        HideLevelSuccess();
    }
    [ContextMenu("LevelSuccessPopupAnimation")]
    private void LevelSuccessPopupAnimation()
    {
        _background.SetActive(true);
        _colorWheel.gameObject.SetActive(true);

        //color wheel animation
        _colorWheel.DOLocalRotate(Vector3.back, 0.01f, RotateMode.FastBeyond360)
           .SetLoops(-1, LoopType.Incremental);

        //ControlScale(_successPopup, true, duration);


        _successPopup.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 2f).
            SetDelay(.5f).
            SetEase(Ease.OutElastic).
            OnComplete(() => StarsAnim(starsEarned));

        _successPopup.DOScale(Vector3.one, 1.5f).
            SetDelay(1.2f).
            SetEase(Ease.InOutCubic);

       
    }


    [Space(20)]
    public int starsEarned;
    void StarsAnim(int starsEarned)
    {
        _earnedStars[0].DOScale(Vector3.one, 2f).
            SetEase(Ease.OutElastic);

        if (starsEarned >= 2)
        {
          _earnedStars[1].DOScale(Vector3.one, 2f).
        SetDelay(.4f).
        SetEase(Ease.OutElastic);

            if (starsEarned == 3)
            {
                _earnedStars[2].DOScale(Vector3.one, 2f).
                SetDelay(.8f).
                SetEase(Ease.OutElastic);
            }
        }

    }

    [ContextMenu("HideLevelSuccess")]
    private void HideLevelSuccess()
    {
        ControlScale(_successPopup, false);
        _background.SetActive(false);
        _colorWheel.gameObject.SetActive(false);

        foreach (var star in _earnedStars)
        {
            star.DOScale(Vector3.zero, 0);
        }
        //DOTween.KillAll();
    }

    private void ControlScale(Transform t, bool status, float duration = 0, Ease easeCurve = Ease.OutBack)
    {
        var scale = status ? Vector2.one : Vector2.zero;
        t.DOScale(scale, duration).SetEase(easeCurve);
    }
}
