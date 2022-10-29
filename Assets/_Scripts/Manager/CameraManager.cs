using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Vector3 forwardRotation;
    public Vector3 backRotation;
    protected Camera mMainCamera;
    protected float anchorZ;
    protected Vector3 target;
    void Start()
    {
        mMainCamera = Camera.main;
    }

    public void setAnchor(Vector3 targetAnchor) {
        Vector3 cameraPosition = Camera.main.transform.position;
        anchorZ = cameraPosition.z - targetAnchor.z;
    }

    public void setTarget(Vector3 newTarget) {
        target = newTarget;
    }

    public void toSeeForward() {
        mMainCamera.transform.rotation = Quaternion.Euler(forwardRotation);
        mMainCamera.transform.position = new Vector3(target.x, mMainCamera.transform.position.y, target.z + anchorZ);
    }

    public void toSeeBack() {
        mMainCamera.transform.rotation = Quaternion.Euler(backRotation);
        mMainCamera.transform.position = new Vector3(target.x, mMainCamera.transform.position.y, target.z - anchorZ);
    }
}
