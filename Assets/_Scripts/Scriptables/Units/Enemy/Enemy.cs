using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameManager GameManager;
    public EnemyEvents enemyEventsRef;
    void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    void Update()
    {
        _OutArea();
    }
    public void DestroyObjectDelayed()
    {
        Destroy(gameObject);
    }
    void _OutArea()
    {
        if (transform.position.z > GameManager.playAreaForward)
        {
            enemyEventsRef.Invoke(EnemyEventTypes.OutArea, this);
        }
        if (transform.position.z < GameManager.playAreaBack)
        {
            enemyEventsRef.Invoke(EnemyEventTypes.OutArea, this);
        }
    }
}
