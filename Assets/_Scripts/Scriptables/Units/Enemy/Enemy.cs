using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameManager gameManager;
    EnemyProps props;
    [SerializeField]
    GameObject _enemyPrefab;
    public EnemyEvents enemyEventsRef;
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        props = ScriptableObject.CreateInstance<EnemyProps>();
    }
    void Update()
    {
        _OutArea();
    }
    public void DestroyObjectDelayed()
    {
        Destroy(gameObject);
    }

    public void Hit(float hit)
    {
        this.props.health -= hit;
        if (this.props.health <= 0)
        {
            _Dead();
        }
    }
    void _OutArea()
    {
        if (transform.position.z > gameManager.playAreaForward)
        {
            enemyEventsRef.Invoke(EnemyEventTypes.OutArea, this);
        }
        if (transform.position.z < gameManager.playAreaBack)
        {
            enemyEventsRef.Invoke(EnemyEventTypes.OutArea, this);
        }
    }

    void _Dead()
    {
        DestroyObjectDelayed();
    }
}
