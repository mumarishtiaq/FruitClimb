using System;
using System.Collections;
using System.Collections.Generic;
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

    [Space(10)]
    [Header("---Sounds---")]
    [Space(5)]
    [SerializeField] private Toggle _musicToggle;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Toggle _sfxToggle;

    [Space(10)]
    [Header("---Controls---")]
    [Space(5)]
    [SerializeField] private ToggleGroup _controlsToggleGroup;

    [Space(10)]
    [Header("---GFX---")]
    [Space(5)]
    [SerializeField] private Toggle _effectsToggle; 
    
    
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

        onComplete?.Invoke();
    }
    private void AddListners()
    {
        _settingsBtn.onClick.AddListener(() => ToggleSettingPanel(true));
        _closeBtn.onClick.AddListener(() => ToggleSettingPanel(false));

        _musicToggle.onValueChanged.AddListener(isOn => _gameSettingsManager.OnMusicToggle(isOn, _musicSlider.value));
        _musicToggle.onValueChanged.AddListener(isOn => SetInteractable(_musicSlider, isOn));

        _musicSlider.onValueChanged.AddListener(_gameSettingsManager.OnUpdateMusicVolume);

        //SFX toggle
        _sfxToggle.onValueChanged.AddListener(_gameSettingsManager.OnSFXToggle);

        //effects toggle
        _effectsToggle.onValueChanged.AddListener(_gameSettingsManager.OnEffectsToggle);

        //save onclick
        _saveBtn.onClick.AddListener(() => _gameSettingsManager.OnSaveClicked());
        
        //save onclick
        _restoreSettingsBtn.onClick.AddListener(() => _gameSettingsManager.OnRestoreSettings());


    }

    private void RemoveListners()
    {
        _settingsBtn.onClick.RemoveAllListeners();
        _closeBtn.onClick.RemoveAllListeners();

        _musicToggle.onValueChanged.RemoveAllListeners();

        _musicSlider.onValueChanged.RemoveAllListeners();
        _sfxToggle.onValueChanged.RemoveAllListeners();
        _effectsToggle.onValueChanged.RemoveAllListeners();

        _saveBtn.onClick.RemoveAllListeners();
        _restoreSettingsBtn.onClick.RemoveAllListeners();
    }

    private void ToggleSettingPanel(bool status)
    {
        _settingsPanel.SetActive(status);
    }

    


    private void SetInteractable(Selectable selectable, bool isInteractable = false)
    {
        selectable.interactable = isInteractable;
    }
}
