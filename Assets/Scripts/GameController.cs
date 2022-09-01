using UnityEngine;

public class GameController : MonoBehaviour
{

    public PlayerController player;
    protected Joystick joystick;
    protected Joybutton joybutton;
    public EnemyManager enemyManager;
    private float XRange = 15;
    private float horizontalInput;

    private Vector3 clickPosition;
    private float clickAngle = 0;
    public Plane plane = new Plane(Vector3.up, 0);

    // Start is called before the first frame update
    void Start()
    {
        joystick = FindObjectOfType<Joystick>();
        joybutton = FindObjectOfType<Joybutton>();
        horizontalInput = horizontalInput + 1;
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

        // if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        // {
        player.transform.Translate(Vector3.right * joystick.Horizontal * Time.deltaTime * player.speed);
        // }
        // else
        // {
        //     horizontalInput = Input.GetAxis("Horizontal");
        //     player.transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * player.speed);

        // }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            float distance;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (plane.Raycast(ray, out distance))
            {
                clickPosition = ray.GetPoint(distance);
                clickPosition.y = player.transform.position.y;
                clickAngle = calculateDirectionAngle();

                player.shot(clickAngle);
            }
        }
    }

    private float calculateDirectionAngle()
    {
        Vector3 direction = clickPosition - player.transform.position;
        int sign = direction.x >= 0 ? 1 : -1;
        int offset = sign >= 0 ? 0 : 360;
        return Vector3.Angle(player.transform.forward, direction) * sign + offset;
    }
}
