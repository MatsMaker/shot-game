using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int playAreaForward = 125;
    public int playAreaBack = -25;
    public float playAreaXRange = 15;

    public PlayerManager player;
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
        cameraManager.setAnchor(player.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x < -playAreaXRange)
        {
            player.transform.position = new Vector3(-playAreaXRange, player.transform.position.y, player.transform.position.z);
        }
        if (player.transform.position.x > playAreaXRange)
        {
            player.transform.position = new Vector3(playAreaXRange, player.transform.position.y, player.transform.position.z);
        }
        if (player.transform.position.z > playAreaForward)
        {
            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, playAreaForward);
        }
        if (player.transform.position.z < playAreaBack)
        {
            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, playAreaBack);
        }

        if (joystick.Vertical != 0) {
            player.Events.Invoke(PlayerEventType.Move, Vector3.forward * joystick.Vertical * Time.deltaTime * player.state.speed);
        }
        if (joystick.Horizontal != 0) {
            player.Events.Invoke(PlayerEventType.Move, Vector3.right * joystick.Horizontal * Time.deltaTime * player.state.speed);
        }
        if (joyButtonShot.Pressed)
        {
            player.Events.Invoke(PlayerEventType.BootShot, Vector3.forward);
        }
        if (joyButtonAmo.Pressed)
        {
            player.Events.Invoke(PlayerEventType.MineShot, Vector3.forward);
        }

        cameraManager.setTarget(player.transform.position);
        if (joyButtonTurnCamera.Pressed)
        {
            cameraManager.toSeeBack();
        } else {
            cameraManager.toSeeForward();
        }
    }
}
