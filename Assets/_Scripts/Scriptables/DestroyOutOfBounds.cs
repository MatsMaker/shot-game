using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    GameManager GameManager;

    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z > GameManager.playAreaForward)
        {
            Destroy(gameObject);
        }
        if (transform.position.z < GameManager.playAreaBack)
        {
            Destroy(gameObject);
            Debug.Log("The enemy passed.\n Game Over!");
        }
    }
}
