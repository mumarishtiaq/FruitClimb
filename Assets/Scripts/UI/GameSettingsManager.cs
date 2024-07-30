using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettingsManager : MonoBehaviour
{
    [SerializeField] private GameSettingsEntity _currentSettings;

    private void Awake()
    {
        _currentSettings = new GameSettingsEntity();
    }
    public void OnMusicToggle(bool isOn, float musicVolume)
    {
        Debug.Log($"Music is {isOn}, volume is {musicVolume}");

        _currentSettings.isMusicOn = isOn;
    } 
    
    public void OnUpdateMusicVolume( float musicVolume)
    {
        Debug.Log($"volume is {musicVolume}");
        _currentSettings.musicVolume = musicVolume;
    } 
    
    public void OnSFXToggle(bool isOn)
    {
        Debug.Log($"SFX is {isOn}");

        _currentSettings.isSFXOn = isOn;
    }
    
    public void OnEffectsToggle(bool isOn)
    {
        Debug.Log($"Effects is {isOn}");
        _currentSettings.isGFXOn = isOn;
    }
    
    public void OnSaveClicked()
    {
        Debug.Log($"Save clicked!");
    }
    
    public void OnRestoreSettings()
    {
        Debug.Log($"Restore Settings!");
    }
}
