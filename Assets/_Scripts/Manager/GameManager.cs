using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int playAreaForward = 125;
    public int playAreaBack = -25;
    public float playAreaXRange = 15;

    public PlayerManager playerMng;
    public CameraManager cameraManager;
    protected Joystick joystick;
    protected JoyButtonShot joyButtonShot;
    protected JoyButtonAmo joyButtonAmo;
    protected JoyButtonTurnCamera joyButtonTurnCamera;
    public EnemyManager enemyManager;
    // Start is called before the first frame update
    void Start()
    {
        joystick = FindObjectOfType<Joystick>();
        joyButtonShot = GameObject.Find("JoyButton1").GetComponent<JoyButtonShot>();
        joyButtonAmo = GameObject.Find("JoyButton2").GetComponent<JoyButtonAmo>();
        joyButtonTurnCamera = GameObject.Find("JoyButton3").GetComponent<JoyButtonTurnCamera>();

        enemyManager.startSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        // _limitsMovePlayer(); // TODO restore limits move player
        _playerControl();
        _updateCameraPosition();
    }

    protected void _playerControl() {
        if (joystick.Vertical != 0) {
            playerMng.Events.Invoke(PlayerEventType.Move, Vector3.forward * joystick.Vertical * Time.deltaTime * playerMng.state.speed);
        }
        if (joystick.Horizontal != 0) {
            playerMng.Events.Invoke(PlayerEventType.Move, Vector3.right * joystick.Horizontal * Time.deltaTime * playerMng.state.speed);
        }
        if (joyButtonShot.Pressed)
        {
            playerMng.Events.Invoke(PlayerEventType.BootShot, Vector3.forward);
        }
        if (joyButtonAmo.Pressed)
        {
            playerMng.Events.Invoke(PlayerEventType.MineShot, Vector3.forward);
        }
    }

    protected void _updateCameraPosition() {
        cameraManager.setTarget(playerMng.transform.position);
        if (joyButtonTurnCamera.Pressed)
        {
            cameraManager.toSeeBack();
        } else {
            cameraManager.toSeeForward();
        }
    }

    protected void _limitsMovePlayer() {
        if (playerMng.transform.position.x < -playAreaXRange)
        {
            playerMng.transform.position = new Vector3(-playAreaXRange, playerMng.transform.position.y, playerMng.transform.position.z);
        }
        if (playerMng.transform.position.x > playAreaXRange)
        {
            playerMng.transform.position = new Vector3(playAreaXRange, playerMng.transform.position.y, playerMng.transform.position.z);
        }
        if (playerMng.transform.position.z > playAreaForward)
        {
            playerMng.transform.position = new Vector3(playerMng.transform.position.x, playerMng.transform.position.y, playAreaForward);
        }
        if (playerMng.transform.position.z < playAreaBack)
        {
            playerMng.transform.position = new Vector3(playerMng.transform.position.x, playerMng.transform.position.y, playAreaBack);
        }
    }
}
