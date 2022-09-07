using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z > gameController.playAreaForward) {
            Destroy(gameObject);
        }
        if (transform.position.z < gameController.playAreaBack)
        {
            Destroy(gameObject);
            Debug.Log("Game Over!");
        }
    }
}
