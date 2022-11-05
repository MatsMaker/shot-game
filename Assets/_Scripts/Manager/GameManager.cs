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
        controlMng.playerEvents.AddListener(_playerControlListener);
        controlMng.cameraEvents.AddListener(_cameraControlListener);

        enemyManager.startSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case GameState.Play:
                _playAreaLimits();
                _updateCameraPosition();
                break;
            default:
                break;
        }
    }

    protected void _updateCameraPosition() {
        cameraManager.setTarget(playerMng.transform.position);
    }

    protected void _playerControlListener(PlayerEventType type, Vector3 bias) {
        playerMng.events.Invoke(type, bias);
    }

    protected void _cameraControlListener(CameraEventType type) {
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

    protected void _playAreaLimits() {
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
}
