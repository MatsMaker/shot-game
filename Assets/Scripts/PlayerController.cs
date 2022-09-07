using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{

    public Text textElement;
    public Text textElement1;
    public float speed = 10.0f;
    public GameObject bootsPrefab;
    private float YProjectileShot = 2;
    public int maxBoots = 30;
    public int countBoots = 30;
    public float cooldownBoots = 0.3f;
    public int reloadBootsTime = 5;
    public bool isReadyToBootsShot = true;


    public GameObject minePrefab;
    public int countMine = 5;
    public int maxMine = 5;
    public int reloadMineTime = 15;
    public float cooldownMine = 1f;
    public bool isReadyToMineShot = true;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        textElement.text = "Boots: " + countBoots;
        textElement1.text = "Mines: " + countMine;
    }

    private IEnumerator updateBootsAmo()
    {
        if (countBoots > 0)
        {
            countBoots--;
            isReadyToBootsShot = false;
            yield return new WaitForSeconds(cooldownBoots);
            isReadyToBootsShot = countBoots > 0;
        }

        if (countBoots <= 0)
        {
            yield return new WaitForSeconds(reloadBootsTime);
            countBoots = maxBoots;
            isReadyToBootsShot = countBoots > 0;
        }
    }

    public void bootsShot(Vector3 angle)
    {
        if (isReadyToBootsShot)
        {
            GameObject projectile = Instantiate(bootsPrefab, transform.position, transform.rotation);
            projectile.transform.position = new Vector3(projectile.transform.position.x, YProjectileShot, projectile.transform.position.z);
            projectile.transform.rotation = Quaternion.Euler(angle);
            StartCoroutine(updateBootsAmo());
        }
    }

    private IEnumerator updateMineAmo()
    {
        if (countMine > 0)
        {
            countMine--;
            isReadyToMineShot = false;
            yield return new WaitForSeconds(cooldownMine);
            isReadyToMineShot = countMine > 0;
        }

        if (countMine <= 0)
        {
            yield return new WaitForSeconds(reloadMineTime);
            countMine = maxMine;
            isReadyToMineShot = countMine > 0;
        }
    }

    public void mineShot()
    {
        if (isReadyToMineShot)
        {
            GameObject projectile = Instantiate(minePrefab, transform.position, Quaternion.Euler(minePrefab.transform.position.x, minePrefab.transform.position.y + 1, minePrefab.transform.position.z));
            projectile.transform.position = new Vector3(projectile.transform.position.x, transform.position.y, projectile.transform.position.z);
            StartCoroutine(updateMineAmo());
        }
    }
}
