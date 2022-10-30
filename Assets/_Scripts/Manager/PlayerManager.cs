using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;


public enum PlayerEventType { Move, BootShot, MineShot };
[System.Serializable]
public class Events : UnityEvent<PlayerEventType, Vector3>
{
}

public class PlayerManager : MonoBehaviour
{

    public Text textElement;
    public Text textElement1;
    public SoldierState state;
    public BootsAmo bootsAmo;
    public GameObject bootsPrefab;

    public MineAmo minesAmo;
    public GameObject minePrefab;

    public Events Events;

    // Start is called before the first frame update
    void Start()
    {
        Events  = new Events();
        Events.AddListener(EventListener);
    }

    // Update is called once per frame
    void Update()
    {
        textElement.text = "Boots: " + bootsAmo.countBoots;
        textElement1.text = "Mines: " + minesAmo.countMine;
    }

    private void EventListener(PlayerEventType eventType, Vector3 bias) {
        switch (eventType)
        {
            case PlayerEventType.Move:
                moveTo(bias);
                break;
            case PlayerEventType.BootShot:
                bootsShot(bias);
                break;
            case PlayerEventType.MineShot:
                mineShot();
                break;
            default:
                break;
        }
    }

    void moveTo(Vector3 bias) {
        transform.Translate(bias);
    }

    private IEnumerator updateBootsAmo()
    {
        if (bootsAmo.countBoots > 0)
        {
            bootsAmo.countBoots--;
            bootsAmo.isReadyToBootsShot = false;
            yield return new WaitForSeconds(bootsAmo.cooldownBoots);
            bootsAmo.isReadyToBootsShot = bootsAmo.countBoots > 0;
        }

        if (bootsAmo.countBoots <= 0)
        {
            yield return new WaitForSeconds(bootsAmo.reloadBootsTime);
            bootsAmo.countBoots = bootsAmo.maxBoots;
            bootsAmo.isReadyToBootsShot = bootsAmo.countBoots > 0;
        }
    }

    void bootsShot(Vector3 angle)
    {
        if (bootsAmo.isReadyToBootsShot)
        {
            GameObject projectile = Instantiate(bootsPrefab, transform.position, transform.rotation);
            projectile.transform.position = new Vector3(projectile.transform.position.x, state.YProjectileShot, projectile.transform.position.z);
            projectile.transform.rotation = Quaternion.Euler(angle);
            StartCoroutine(updateBootsAmo());
        }
    }

    private IEnumerator updateMineAmo()
    {
        if (minesAmo.countMine > 0)
        {
            minesAmo.countMine--;
            minesAmo.isReadyToMineShot = false;
            yield return new WaitForSeconds(minesAmo.cooldownMine);
            minesAmo.isReadyToMineShot = minesAmo.countMine > 0;
        }

        if (minesAmo.countMine <= 0)
        {
            yield return new WaitForSeconds(minesAmo.reloadMineTime);
            minesAmo.countMine = minesAmo.maxMine;
            minesAmo.isReadyToMineShot = minesAmo.countMine > 0;
        }
    }

    void mineShot()
    {
        if (minesAmo.isReadyToMineShot)
        {
            GameObject projectile = Instantiate(minePrefab, transform.position, Quaternion.Euler(minePrefab.transform.position.x, minePrefab.transform.position.y + 1, minePrefab.transform.position.z));
            projectile.transform.position = new Vector3(projectile.transform.position.x, transform.position.y, projectile.transform.position.z);
            StartCoroutine(updateMineAmo());
        }
    }

}
