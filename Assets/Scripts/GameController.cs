using UnityEngine;

public class GameController : MonoBehaviour
{
    public int playAreaForward = 125;
    public int playAreaBack = -25;
    public float playAreaXRange = 15;

    public PlayerController player;
    protected Joystick joystick;
    protected JoyButtonShot joyButtonShot;
    protected JoyButtonAmo joyButtonAmo;
    protected Camera mMainCamera;
    protected Vector3 diffPlayerCamera;
    public EnemyManager enemyManager;
    // Start is called before the first frame update
    void Start()
    {
        joystick = FindObjectOfType<Joystick>();
        joyButtonShot = GameObject.Find("JoyButton1").GetComponent<JoyButtonShot>();
        joyButtonAmo = GameObject.Find("JoyButton2").GetComponent<JoyButtonAmo>();

        mMainCamera = Camera.main;
        Vector3 cameraPosition = Camera.main.transform.position;
        diffPlayerCamera = new Vector3(cameraPosition.x, cameraPosition.y, cameraPosition.z) - player.transform.position;
        enemyManager.startSpawn();
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

        player.transform.Translate(Vector3.forward * joystick.Vertical * Time.deltaTime * player.speed);
        player.transform.Translate(Vector3.right * joystick.Horizontal * Time.deltaTime * player.speed);
        mMainCamera.transform.position = player.transform.position + diffPlayerCamera;

        if (joyButtonAmo.Pressed)
        {
            player.mineShot();
        }

        if (joyButtonShot.Pressed)
        {
            player.bootsShot(Vector3.forward);
        }
    }
}
