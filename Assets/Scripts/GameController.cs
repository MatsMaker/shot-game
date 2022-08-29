using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public PlayerController player;
    private float XRange = 15;
    private float horizontalInput;
    private Vector3 clickPosition;
    private float clickAngle = 0;

    public Plane plane = new Plane(Vector3.up, 0);

    // Start is called before the first frame update
    void Start()
    {
        horizontalInput = horizontalInput + 1;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        if (player.transform.position.x < -XRange)
        {
            player.transform.position = new Vector3(-XRange, player.transform.position.y, player.transform.position.z);
        }
        if (player.transform.position.x > XRange)
        {
            player.transform.position = new Vector3(XRange, player.transform.position.y, player.transform.position.z);
        }
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
