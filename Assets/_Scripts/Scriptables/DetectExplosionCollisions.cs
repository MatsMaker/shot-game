using UnityEngine;
using System.Collections;

public class DetectExplosionCollisions : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
        StartCoroutine(ExplosionDestroy());
    }

    IEnumerator ExplosionDestroy() {
        yield return new WaitForSeconds(0.12f);
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
