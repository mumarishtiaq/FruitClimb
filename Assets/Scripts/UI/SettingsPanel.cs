using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    #region Variables
    [SerializeField] private bool _isAnyThingChanged = false;
    [Space(10)]
    [Header("---General---")]
    [Space(5)]
    [SerializeField] private GameObject _settingsPanel;
    [SerializeField] private Button _settingsBtn;
    [SerializeField] private Button _closeBtn;
    [SerializeField] private GameObject _overlay;

    [Space(10)]
    [Header("---Sounds---")]
    [Space(5)]
    [SerializeField] private SwitchRegulator _musicToggle;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private SwitchRegulator _sfxToggle;
    [SerializeField] private SwitchRegulator _vibrationToggle;

    [Space(10)]
    [Header("---Controls---")]
    [Space(5)]
    [SerializeField] private ToggleGroup _controlsToggleGroup;
    [SerializeField] private List<Toggle> _controlToggles;

    [Space(10)]
    [Header("---GFX---")]
    [Space(5)]
    [SerializeField] private SwitchRegulator _effectsToggle; 
    
    
    [Space(10)]
    [Header("---Save and Restore---")]
    [Space(5)]
    [SerializeField] private Button _saveBtn;
    [SerializeField] private Button _restoreSettingsBtn;

    [Space(10)]
    [Header("---Game Settings Manager---")]
    [Space(5)]
    [SerializeField] private GameSettingsManager _gameSettingsManager;


    #endregion Variables

    private bool IsAnythingChanged { set => SetInteractable(_saveBtn,value); }

    private void OnEnable()
    {
        ResolveReferences(() =>
        {
            ToggleSettingPanel(false);
            AddListners();
            IsAnythingChanged = false;
        });
    }
    private void OnDisable()
    {
        RemoveListners();
    }
    private void ResolveReferences(Action onComplete = null)
    {
        if (!_gameSettingsManager)
            _gameSettingsManager = FindObjectOfType<GameSettingsManager>();

        if (_controlsToggleGroup != null)
            _controlToggles = _controlsToggleGroup.GetComponentsInChildren<Toggle>().ToList();

        onComplete?.Invoke();
    }
    private void AddListners()
    {
        //settings and close btn
        _settingsBtn.onClick.AddListener(() => ToggleSettingPanel(true,.4f));
        _closeBtn.onClick.AddListener(() => ToggleSettingPanel(false,.3f));

        //music toggle and slider
        _musicToggle.onValueChanged.AddListener(_gameSettingsManager.OnMusicToggle);
        _musicToggle.onValueChanged.AddListener(isOn => SetInteractable(_musicSlider, isOn));
        _musicSlider.onValueChanged.AddListener(_gameSettingsManager.OnUpdateMusicVolume);

        //SFX toggle
        _sfxToggle.onValueChanged.AddListener(_gameSettingsManager.OnSFXToggle);

        //vibration toggle
        _vibrationToggle.onValueChanged.AddListener(_gameSettingsManager.OnVibrationToggle);

        //movement Control Toggles
        for (int i = 0; i < _controlToggles.Count; i++)
        {
            var toggle = _controlToggles[i];
            var controlType = (MovementControlType)(i+1);
            toggle.onValueChanged.AddListener((isOn) => _gameSettingsManager.OnMovementControlsChange(controlType,isOn));
            toggle.onValueChanged.AddListener((isOn) => ChangeAlpha(toggle,isOn));
            ChangeAlpha(toggle, toggle.isOn);
        }

        //effects toggle
        _effectsToggle.onValueChanged.AddListener(_gameSettingsManager.OnEffectsToggle);

        //save onclick
        _saveBtn.onClick.AddListener(() => _gameSettingsManager.OnSaveClicked());
        
        //save onclick
        _restoreSettingsBtn.onClick.AddListener(() => _gameSettingsManager.OnRestoreSettings());


        //Notify UI to update on saved settings fetched
        _gameSettingsManager.NotifyUI.AddListener(UpdateUI_asPer_settingsFetched);
    }

    private void RemoveListners()
    {
        _settingsBtn.onClick.RemoveAllListeners();
        _closeBtn.onClick.RemoveAllListeners();

        _musicToggle.onValueChanged.RemoveAllListeners();

        _musicSlider.onValueChanged.RemoveAllListeners();
        _sfxToggle.onValueChanged.RemoveAllListeners();
        _vibrationToggle.onValueChanged.RemoveAllListeners();

        _controlToggles.ForEach(toggle => toggle.onValueChanged.RemoveAllListeners());

        _effectsToggle.onValueChanged.RemoveAllListeners();

        for (int i = 0; i < _controlToggles.Count; i++)
        {
            var toggle = _controlToggles[i];
            toggle.onValueChanged.RemoveAllListeners();
        }
            _saveBtn.onClick.RemoveAllListeners();
        _restoreSettingsBtn.onClick.RemoveAllListeners();

        _gameSettingsManager.NotifyUI.RemoveAllListeners();

    }
    private void ToggleSettingPanel(bool status,float duration = 0)
    {
        var scale = status ? Vector2.one : Vector2.zero;
        var easeCurve = status ? Ease.OutBack: Ease.InBack;
        _settingsPanel.transform.DOScale(scale, duration).SetEase(easeCurve);

        var alpha = status ? .7f: 0f;
        //_overlay.DOFade(alpha, .2f);
        _overlay.SetActive(status);
    }

    private void ChangeAlpha(Toggle toggle,bool isOn)
    {
        var image = toggle.GetComponentInChildren<Image>();

        var alpha = isOn ? 1f : .5f;

        var color = new Color(1, 1, 1, alpha);
        image.color = color;
    }

    private void SetInteractable(Selectable selectable, bool isInteractable = false)
    {
        selectable.interactable = isInteractable;
    }

    private void UpdateUI_asPer_settingsFetched(GameSettingsEntity settings)
    {
        // Update Music Settings
        _musicToggle.isOn = settings.soundSettings.isMusicOn;
        _musicSlider.value = settings.soundSettings.musicVolume;

        // Update SFX
        _sfxToggle.isOn = settings.soundSettings.isSFXOn;

        // Update Vibration
        _vibrationToggle.isOn = settings.soundSettings.isVibrationOn;

        // Update Movement Controls
        foreach (var toggle in _controlToggles)
        {
            MovementControlType controlType = (MovementControlType)(_controlToggles.IndexOf(toggle) + 1);
            toggle.isOn = settings.controlSettings.controlType == controlType;
        }

        // Update GFX Settings
        _effectsToggle.isOn = settings.gfxSettings.isEffectsOn;
    }
}
