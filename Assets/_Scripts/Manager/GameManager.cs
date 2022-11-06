using UnityEngine;

public enum GameState {
    Play,
    Stop,
    GameOver,
}

public class GameManager : MonoBehaviour
{
    protected GameState state;
    public float playAreaForward = 125;
    public float playAreaBack = -25;
    public float playAreaXRange = 15;

    public PlayerManager playerMng;
    public ControlManager controlMng;
    public CameraManager cameraManager;
    public EnemyManager enemyManager;
    // Start is called before the first frame update
    void Start()
    {
        state = GameState.Play;
        controlMng.playerEvents.AddListener(_PlayerControlListener);
        controlMng.cameraEvents.AddListener(_CameraControlListener);
        enemyManager.events.AddListener(_EnemyMngListener);
        enemyManager.StartSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case GameState.Play:
                _PlayAreaLimits();
                _UpdateCameraPosition();
                break;
            default:
                break;
        }
    }

    protected void _UpdateCameraPosition() {
        cameraManager.setTarget(playerMng.transform.position);
    }

    void _EnemyMngListener(EnemyEventTypes type, Enemy enemy) {
        switch (type)
        {
            case EnemyEventTypes.OutArea:
                _GameOver();
                break;
            case EnemyEventTypes.Died:
                enemyManager.DestroyEnemy(enemy);
                break;
            default:
                break;
        }
    }

    protected void _PlayerControlListener(PlayerEventType type, Vector3 bias) {
        if (state == GameState.Play)
        {
            playerMng.events.Invoke(type, bias);
        }
    }

    protected void _CameraControlListener(CameraEventType type) {
        switch (type)
        {
            case CameraEventType.seeBackPressed:
                cameraManager.toSeeBack();
                break;
            case CameraEventType.seeFrowardPressed:
                cameraManager.toSeeForward();
                break;
            default:
                break;
        }
    }

    protected void _PlayAreaLimits() {
        if (playerMng.transform.position.x < -playAreaXRange)
        {
            playerMng.transform.position = new Vector3(-playAreaXRange, playerMng.transform.position.y, playerMng.transform.position.z);
        }
        if (playerMng.transform.position.x > playAreaXRange)
        {
            playerMng.transform.position = new Vector3(playAreaXRange, playerMng.transform.position.y, playerMng.transform.position.z);
        }
        if (playerMng.transform.position.z > playAreaForward)
        {
            playerMng.transform.position = new Vector3(playerMng.transform.position.x, playerMng.transform.position.y, playAreaForward);
        }
        if (playerMng.transform.position.z < playAreaBack)
        {
            playerMng.transform.position = new Vector3(playerMng.transform.position.x, playerMng.transform.position.y, playAreaBack);
        }
    }

    void _GameOver() {
        state = GameState.Stop;
        Debug.Log("The enemy passed.\n Game Over!");
    }
}
