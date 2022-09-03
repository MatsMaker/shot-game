using UnityEngine;

public class GameController : MonoBehaviour
{
    public PlayerController player;
    protected Joystick joystick;
    protected Joybutton joybutton;
    protected Camera mMainCamera;
    protected Vector3 diffPlayerCamera;
    public EnemyManager enemyManager;
    private float XRange = 15;
    // Start is called before the first frame update
    void Start()
    {
        joystick = FindObjectOfType<Joystick>();
        joybutton = FindObjectOfType<Joybutton>();
        mMainCamera = Camera.main;
        Vector3 cameraPosition = Camera.main.transform.position;
        diffPlayerCamera = new Vector3(cameraPosition.x, cameraPosition.y, cameraPosition.z) - player.transform.position;
        enemyManager.startSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position.x < -XRange)
        {
            player.transform.position = new Vector3(-XRange, player.transform.position.y, player.transform.position.z);
        }
        if (player.transform.position.x > XRange)
        {
            player.transform.position = new Vector3(XRange, player.transform.position.y, player.transform.position.z);
        }

        player.transform.Translate(Vector3.forward * joystick.Vertical * Time.deltaTime * player.speed);
        player.transform.Translate(Vector3.right * joystick.Horizontal * Time.deltaTime * player.speed);
        mMainCamera.transform.position = player.transform.position + diffPlayerCamera;


        if (joybutton.Pressed)
        {
            player.shot(Vector3.forward);
        }
    }
}
