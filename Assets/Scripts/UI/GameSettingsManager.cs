using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameSettingsManager : MonoBehaviour
{
    [SerializeField] private GameSettingsEntity _currentSettings;
    [SerializeField] private GameSettingsEntity _savedSettings;
    public UnityEvent<GameSettingsEntity> NotifyUI;

    private void Awake()
    {
        _currentSettings = JSONExtraction.FetchGameSettingsFromJSON();
        _savedSettings = _currentSettings.Clone(); // Clone the settings to keep the initial state
        NotifyUI?.Invoke(_currentSettings);
    }
    public void OnMusicToggle(bool isOn)
    {
        _currentSettings.soundSettings.isMusicOn = isOn;
    } 
    
    public void OnUpdateMusicVolume( float musicVolume)
    {
        _currentSettings.soundSettings.musicVolume = musicVolume;
    } 
    
    public void OnSFXToggle(bool isOn)
    {
        _currentSettings.soundSettings.isSFXOn = isOn;
    }
    
    public void OnVibrationToggle(bool isOn)
    {
        _currentSettings.soundSettings.isVibrationOn = isOn;
    }

    public void OnMovementControlsChange(MovementControlType controlType, bool isOn)
    {
        if(isOn)
        {
            _currentSettings.controlSettings.controlType = controlType;
        }
    }
    
    public void OnEffectsToggle(bool isOn)
    {
        _currentSettings.gfxSettings.isEffectsOn= isOn;
    }
    
    public void OnSaveClicked()
    {
        Debug.Log($"Save clicked!");
        JSONExtraction.SaveSettingsIntoJSON(_currentSettings);

    }
    
    public void OnRestoreSettings()
    {
        Debug.Log($"Restore Settings!");
    }

    public void TestSwitch(bool isOn)
    {
        Debug.Log($"Test switch {isOn}");
    }
}
