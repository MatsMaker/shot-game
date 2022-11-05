using UnityEngine;

public enum CameraEventType { seeFrowardPressed, seeBackPressed };

public class CameraManager : MonoBehaviour
{
    public Vector3 forwardRotation;
    public Vector3 backRotation;
    protected Camera mMainCamera;
    [SerializeField] protected Vector3 seeFrontAnchor;
    [SerializeField] protected Vector3 seeBackAnchor;
    [SerializeField] protected Vector3 menuPosition;
    protected Vector3 target;
    void Start()
    {
        seeFrontAnchor  = new Vector3(0, 9, -5);
        seeBackAnchor = new Vector3(0, 9, 12);
        mMainCamera = Camera.main;
    }

    public void setTarget(Vector3 newTarget) {
        target = newTarget + seeFrontAnchor;
    }

    public void toSeeForward() {
        mMainCamera.transform.rotation = Quaternion.Euler(forwardRotation);
        mMainCamera.transform.position = target + seeFrontAnchor;
    }

    public void toSeeBack() {
        mMainCamera.transform.rotation = Quaternion.Euler(backRotation);
        mMainCamera.transform.position = target + seeBackAnchor;
    }
}
