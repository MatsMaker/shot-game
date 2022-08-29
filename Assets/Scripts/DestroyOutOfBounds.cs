using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private float backBound = -20;
    private float frontBound = 125;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z > frontBound) {
            Destroy(gameObject);
        }
        if (transform.position.z < backBound)
        {
            Destroy(gameObject);
            Debug.Log("Game Over!");
        }
    }
}
