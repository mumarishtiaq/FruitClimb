
[System.Serializable]
public class GameSettingsEntity
{
    public bool isMusicOn;
    public float musicVolume;
    public bool isSFXOn;
    public MovementControlType controlType;
    public bool isGFXOn;


    public GameSettingsEntity()
    {
        isMusicOn = true;
        musicVolume = 0.5f;
        isSFXOn = true;
        controlType = MovementControlType.WASD_KEYS;
        isGFXOn = true;
    }
}

public enum MovementControlType
{
    NONE = 0,
    WASD_KEYS = 1,
    JOYSTICK = 2,
    BUTTONS = 3
}

