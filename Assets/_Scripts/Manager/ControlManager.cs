using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class PlayerControlEvents : UnityEvent<PlayerEventType, Vector3>
{
}

public class CameraControlEvents : UnityEvent<CameraEventType>
{
}

public class ControlManager : MonoBehaviour
{
    public PlayerControlEvents playerEvents;
    public CameraControlEvents cameraEvents;
    Joystick joystick;
    JoyButtonShot joyButtonShot;
    JoyButtonAmo joyButtonAmo;
    JoyButtonTurnCamera joyButtonTurnCamera;
    void Start()
    {
        playerEvents = new PlayerControlEvents();
        cameraEvents = new CameraControlEvents();

        joystick = FindObjectOfType<Joystick>();
        joyButtonShot = GameObject.Find("JoyButton1").GetComponent<JoyButtonShot>();
        joyButtonAmo = GameObject.Find("JoyButton2").GetComponent<JoyButtonAmo>();
        joyButtonTurnCamera = GameObject.Find("JoyButton3").GetComponent<JoyButtonTurnCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        _PlayerControl();
        _CameraControl();
    }

    void _PlayerControl()
    {
        if (joystick.Vertical != 0)
        {
            playerEvents.Invoke(PlayerEventType.Move, Vector3.forward * joystick.Vertical * Time.deltaTime);
        }
        if (joystick.Horizontal != 0)
        {
            playerEvents.Invoke(PlayerEventType.Move, Vector3.right * joystick.Horizontal * Time.deltaTime);
        }
        if (joyButtonShot.Pressed)
        {
            playerEvents.Invoke(PlayerEventType.BootShot, Vector3.forward);
        }
        if (joyButtonAmo.Pressed)
        {
            playerEvents.Invoke(PlayerEventType.MineShot, Vector3.forward);
        }
    }

    void _CameraControl()
    {
        if (joyButtonTurnCamera.Pressed)
        {
            cameraEvents.Invoke(CameraEventType.seeBackPressed);
        }
        else
        {
            cameraEvents.Invoke(CameraEventType.seeFrowardPressed);
        }
    }
}
