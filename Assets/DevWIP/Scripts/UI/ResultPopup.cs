using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultPopup : MonoBehaviour
{
    [Header("------Success Popup------")]
    [Space]
    [SerializeField] private Transform _successPopup;
    [SerializeField] private List<Transform> _earnedStars;
    [SerializeField] private GameObject _background;
    [SerializeField] private Transform _colorWheel;
    [SerializeField] private Button _homeBtnSuccess;
    [SerializeField] private Button _restartBtnSuccess;
    [SerializeField] private Button _nextBtnSuccess;
    [SerializeField] private CanvasGroup _coinsCanvasGroupSuccess;
    [Space(20)]
    public int starsEarned;

    [Space]
    [Header("------Failed Popup------")]
    [Space]
    [SerializeField] private Transform _failedPopup;
    [SerializeField] private List<Transform> _skulls;
    [SerializeField] private Button _homeBtnFailed;
    [SerializeField] private Button _restartBtnFailed;
    [SerializeField] private Button _nextBtnFailed;
    [SerializeField] private CanvasGroup _coinsCanvasGroupFailed;

    private void Start()
    {
        Hidepopups();
    }
    [ContextMenu("LevelSuccessPopupAnimation")]
    private void LevelSuccessPopupAnimation()
    {
        DOTween.KillAll();
        _background.SetActive(true);
        _colorWheel.gameObject.SetActive(true);

        //color wheel animation
        _colorWheel.DOLocalRotate(Vector3.back, 0.01f, RotateMode.FastBeyond360)
           .SetLoops(-1, LoopType.Incremental);


        _successPopup.TweenScale(new Vector3(1.5f, 1.5f, 1.5f), duration: 2f, delay: .5f, easeCurve: Ease.OutElastic, action: () => StarsAnimation(starsEarned));

        _successPopup.TweenScale(Vector3.one, duration: 1.5f, delay: 1.2f, easeCurve: Ease.InOutCubic, action: ()=>ButtonsAnimations(_homeBtnSuccess.transform,_restartBtnSuccess.transform,_nextBtnSuccess.transform));

        _coinsCanvasGroupSuccess.TweenAlpha(1, .5f,delay : 3f);
    }
    private void ButtonsAnimations(Transform btn1, Transform btn2, Transform btn3)
    {
        btn1.TweenScale(Vector3.one, duration: 2f, delay: .5f, easeCurve: Ease.OutElastic);
        btn2.TweenScale(Vector3.one, duration: 2f, delay: .6f, easeCurve: Ease.OutElastic);
        btn3.TweenScale(Vector3.one, duration: 2f, delay: .7f, easeCurve: Ease.OutElastic);
    }
    void StarsAnimation(int starsEarned)
    {
        var star1 = _earnedStars[0];
        var star2 = _earnedStars[1];
        var star3 = _earnedStars[2];

        star1.TweenScale(Vector3.one, 2, easeCurve: Ease.OutElastic);

        if (starsEarned >= 2)
        {
            star2.TweenScale(Vector3.one, 2,delay : .4f, easeCurve: Ease.OutElastic);

            if (starsEarned == 3)
                star3.TweenScale(Vector3.one, 2, delay: .8f, easeCurve: Ease.OutElastic);
        }
    }

    [ContextMenu("Hidepopups")]
    private void Hidepopups()
    {
        //----------------success popup----------------
        _successPopup.TweenScale(Vector3.zero);
        _background.SetActive(false);
        _colorWheel.gameObject.SetActive(false);

        foreach (var star in _earnedStars)
        {
            star.TweenScale(Vector3.zero);
        }

        _homeBtnSuccess.transform.TweenScale(Vector3.zero);
        _restartBtnSuccess.transform.TweenScale(Vector3.zero);
        _nextBtnSuccess.transform.TweenScale(Vector3.zero);
        
        _coinsCanvasGroupSuccess.TweenAlpha(0);

        //----------------Failed popup----------------
        _failedPopup.TweenScale(Vector3.zero);
       

        foreach (var skull in _skulls)
        {
            skull.TweenScale(Vector3.zero);
        }

        _homeBtnFailed.transform.TweenScale(Vector3.zero);
        _restartBtnFailed.transform.TweenScale(Vector3.zero);
        _nextBtnFailed.transform.TweenScale(Vector3.zero);

        _coinsCanvasGroupFailed.TweenAlpha(0);
    }







    [ContextMenu("LevelFailedPopupAnimation")]
    private void LevelFailedPopupAnimation()
    {
        DOTween.KillAll();
        _background.SetActive(true);

        _background.GetComponent<Image>().DOFade(.5f, .5f);


        _failedPopup.TweenScale(new Vector3(1.5f, 1.5f, 1.5f), duration: 2f, delay: .5f, easeCurve: Ease.OutElastic, action: SkullsAnimation);

        _failedPopup.TweenScale(Vector3.one, duration: 1.5f, delay: 1.2f, easeCurve: Ease.InOutCubic, action: () => ButtonsAnimations(_homeBtnFailed.transform, _restartBtnFailed.transform, _nextBtnFailed.transform));

        _coinsCanvasGroupFailed.TweenAlpha(1, .5f, delay: 3f);
    }

    void SkullsAnimation()
    {
        var skull1 = _skulls[0];
        var skull2 = _skulls[1];
        var skull3 = _skulls[2];

        skull2.TweenScale(Vector3.one, 2, easeCurve: Ease.OutElastic);

        skull1.TweenScale(Vector3.one, 2, delay: .1f, easeCurve: Ease.OutElastic);

        skull3.TweenScale(Vector3.one, 2, delay: .2f, easeCurve: Ease.OutElastic);

    }
}
