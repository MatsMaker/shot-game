using UnityEngine;
using System.Collections;

public enum GameState
{
    Start,
    Play,
    Pause,
    Reset,
    GameOver,
}

public class GameManager : MonoBehaviour
{
    GameState state;
    public float playAreaForward = 125;
    public float playAreaBack = -25;
    public float playAreaXRange = 15;

    public PlayerManager playerMng;
    public ControlManager controlMng;
    public CameraManager cameraMng;
    public EnemyManager enemyMng;
    void Start()
    {
        _StartGame();
        state = GameState.Play;
    }
    void Update()
    {
        switch (state)
        {
            case GameState.Play:
                _PlayAreaLimits();
                _UpdateCameraPosition();
                break;
            case GameState.GameOver:
                _GameOver();
                StartCoroutine(_AutoResetGame());
                break;
            case GameState.Reset:
                _ResetGame();
                break;
            case GameState.Pause:
            default:
                cameraMng.EndGameCamera();
                break;
        }
    }
    void _UpdateCameraPosition()
    {
        cameraMng.SetTarget(playerMng.transform.position);
    }
    void _EnemyMngListener(EnemyEventTypes type, Enemy enemy)
    {
        switch (type)
        {
            case EnemyEventTypes.OutArea:
                if (state == GameState.Play)
                {
                    state = GameState.GameOver;
                }
                break;
            case EnemyEventTypes.Died:
                enemyMng.DestroyEnemy(enemy);
                break;
            default:
                break;
        }
    }
    void _PlayerControlListener(PlayerEventType type, Vector3 bias)
    {
        if (state == GameState.Play)
        {
            playerMng.events.Invoke(type, bias);
        }
    }
    void _CameraControlListener(CameraEventType type)
    {
        switch (type)
        {
            case CameraEventType.seeBackPressed:
                cameraMng.SeeBack();
                break;
            case CameraEventType.seeFrowardPressed:
                cameraMng.SeeForward();
                break;
            default:
                break;
        }
    }
    void _PlayAreaLimits()
    {
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
    void _StartGame()
    {
        controlMng.ToggleControlPanel(true);
        controlMng.playerEvents.AddListener(_PlayerControlListener);
        controlMng.cameraEvents.AddListener(_CameraControlListener);
        enemyMng.events.AddListener(_EnemyMngListener);
        enemyMng.StartSpawn();
        cameraMng.SeeForward();
        state = GameState.Play;
    }
    void _ResetGame()
    {
        enemyMng.DestroyAllEnemy();
        enemyMng.StartSpawn();
        controlMng.ToggleControlPanel(true);
        state = GameState.Play;
    }
    void _GameOver()
    {
        enemyMng.DestroyAllEnemy();

        controlMng.ToggleControlPanel(false);
        enemyMng.StopSpawn();
        Debug.Log("The enemy passed.\n Game Over!");
        state = GameState.Pause;
    }
    IEnumerator _AutoResetGame()
    {
        yield return new WaitForSeconds(5);
        state = GameState.Reset;
    }
}
