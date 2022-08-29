using UnityEngine;
public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    public GameObject activeWeapon;
    private float YProjectileShot = 2;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void shot(float angle = 0)
    {
        GameObject projectile = Instantiate(activeWeapon, transform.position, activeWeapon.transform.rotation);
        projectile.transform.position = new Vector3(projectile.transform.position.x, YProjectileShot, projectile.transform.position.z);
        projectile.transform.rotation = Quaternion.Euler(0, angle, 0);
    }
}
