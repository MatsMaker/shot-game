using UnityEngine;

public class ClickAngle : MonoBehaviour
{
    public PlayerManager player;
    private Vector3 clickPosition;
    private float clickAngle = 0;
    public Plane plane = new Plane(Vector3.up, 0);
    private float horizontalInput;

    // Start is called before the first frame update
    void Start()
    {
        horizontalInput = horizontalInput + 1;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        player.transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * player.speed);
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            float distance;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (plane.Raycast(ray, out distance))
            {
                clickPosition = ray.GetPoint(distance);
                clickPosition.y = player.transform.position.y;
                clickAngle = calculateDirectionAngle();

                player.bootsShot(new Vector3(0, clickAngle, 0));
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
