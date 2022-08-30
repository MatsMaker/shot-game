using UnityEngine;
using System.Collections;
public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    public GameObject projectilePrefab;
    private float YProjectileShot = 2;
    public int maxAmo = 30;
    public int countAmo = 30;
    public bool isReadyToShot = true;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    private IEnumerator updateAmo()
    {
        if (countAmo > 0)
        {
            countAmo--;
            isReadyToShot = countAmo > 0;
        }

        if (countAmo <= 0)
        {
            print("Start reload amo");
            yield return new WaitForSeconds(5);
            print("Reloaded amo");
            countAmo = maxAmo;
            isReadyToShot = countAmo > 0;
        }
    }

    public void shot(float angle = 0)
    {
        if (isReadyToShot)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);
            projectile.transform.position = new Vector3(projectile.transform.position.x, YProjectileShot, projectile.transform.position.z);
            projectile.transform.rotation = Quaternion.Euler(0, angle, 0);
            StartCoroutine(updateAmo());
        }
    }
}
