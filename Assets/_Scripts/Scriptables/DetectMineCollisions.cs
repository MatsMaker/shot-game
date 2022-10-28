using UnityEngine;

public class DetectMineCollisions : MonoBehaviour
{
    public GameObject explosionPref;

    void OnTriggerEnter(Collider other)
    {
        Instantiate(explosionPref, transform.position, Quaternion.Euler(new Vector3(0, 0, 0)));
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
