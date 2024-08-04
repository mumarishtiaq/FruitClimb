
[System.Serializable]
public class GameSettingsEntity
{
    public SoundSettings soundSettings;
    public ControlSettings controlSettings;
    public GFXSettings gfxSettings;
    


    public GameSettingsEntity()
    {
        soundSettings = new SoundSettings();
        controlSettings = new ControlSettings();
        gfxSettings = new GFXSettings();
    }

    public GameSettingsEntity Clone()
    {
        return new GameSettingsEntity
        {
            soundSettings = this.soundSettings.Clone(),
            controlSettings = this.controlSettings.Clone(),
            gfxSettings = this.gfxSettings.Clone()
        };
    }
}

[System.Serializable]
public class SoundSettings
{
    public bool isMusicOn;
    public float musicVolume;
    public bool isSFXOn;
    public bool isVibrationOn;

    public SoundSettings()
    {
        isMusicOn = true;
        musicVolume = 0.5f;
        isSFXOn = true;
        isVibrationOn = true;
    }

    public SoundSettings Clone()
    {
        return new SoundSettings
        {
            isMusicOn = this.isMusicOn,
            musicVolume = this.musicVolume,
            isSFXOn = this.isSFXOn,
            isVibrationOn = this.isVibrationOn
        };
    }
}

[System.Serializable]
public class ControlSettings
{
    public MovementControlType controlType;

    public ControlSettings()
    {
        controlType = MovementControlType.WASD_KEYS;
    }

    public ControlSettings Clone()
    {
        return new ControlSettings
        {
            controlType = this.controlType
           
        };
    }
}

[System.Serializable]
public class GFXSettings
{
    public bool isEffectsOn;

    public GFXSettings()
    {
        isEffectsOn = true;
    }

    public GFXSettings Clone()
    {
        return new GFXSettings
        {
            isEffectsOn = this.isEffectsOn
        };
    }
}


public enum MovementControlType
{
    NONE = 0,
    WASD_KEYS = 1,
    JOYSTICK = 2,
    BUTTONS = 3
}

