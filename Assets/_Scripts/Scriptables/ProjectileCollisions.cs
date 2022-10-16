using UnityEngine;

public class ProjectileCollisions : MonoBehaviour
{

    public GameObject grave;
    void OnTriggerEnter(Collider other)
    {
        Vector3 gravePos = other.gameObject.transform.position;
        Instantiate(grave, gravePos, Quaternion.Euler(new Vector3(0, 0, 0)));
        Destroy(gameObject);
        Destroy(other.gameObject);
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
