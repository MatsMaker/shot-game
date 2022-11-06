using UnityEngine;

public enum CameraEventType { seeFrowardPressed, seeBackPressed };

public class CameraManager : MonoBehaviour
{
    public Vector3 forwardRotation;
    public Vector3 backRotation;
    Camera mMainCamera;
    [SerializeField] Vector3 seeFrontAnchor;
    [SerializeField] Vector3 seeBackAnchor;
    [SerializeField] Vector3 menuPosition;
    Vector3 target;
    void Start()
    {
        seeFrontAnchor = new Vector3(0, 9, -5);
        seeBackAnchor = new Vector3(0, 9, 12);
        mMainCamera = Camera.main;
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
}
