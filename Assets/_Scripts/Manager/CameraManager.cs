using UnityEngine;

public enum CameraEventType { seeFrowardPressed, seeBackPressed };

public class CameraManager : MonoBehaviour
{
    public Vector3 forwardRotation;
    public Vector3 backRotation;
    Camera mMainCamera;
    [SerializeField] Vector3 seeFrontAnchor;
    [SerializeField] Vector3 seeBackAnchor;
    [SerializeField] Vector3 menuAnchor;
    [SerializeField] Vector3 menuRotation;
    Vector3 target;
    void Start()
    {
        seeFrontAnchor = new Vector3(0, 9, -5);
        seeBackAnchor = new Vector3(0, 9, 12);
        mMainCamera = Camera.main;

        menuAnchor = new Vector3(-18.4f, 45.2f, -21.4f);
        menuRotation = new Vector3(50f, 25f, -10.8f);
    }
    public void SetTarget(Vector3 newTarget)
    {
        target = newTarget + seeFrontAnchor;
    }
    public void SeeForward()
    {
        mMainCamera.transform.rotation = Quaternion.Euler(forwardRotation);
        mMainCamera.transform.position = target + seeFrontAnchor;
    }
    public void SeeBack()
    {
        mMainCamera.transform.rotation = Quaternion.Euler(backRotation);
        mMainCamera.transform.position = target + seeBackAnchor;
    }
    public void EndGameCamera()
    {
        mMainCamera.transform.rotation = Quaternion.Euler(menuRotation);
        mMainCamera.transform.position = menuAnchor;
    }
}
