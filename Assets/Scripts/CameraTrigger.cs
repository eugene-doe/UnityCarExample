using UnityEngine;
using System.Collections;

public class CameraTrigger : MonoBehaviour
{
    public CameraSwitcher cameraSwitcher;
    public Camera triggeredCamera;

    private Collider car;

    void Start()
    {
        triggeredCamera.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Car"))
        {
            SwitchToTriggeredCamera(true);
            car = collider;
        }
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider == car)
        {
            SwitchToTriggeredCamera(false);
            car = null;
        }
    }

    void SwitchToTriggeredCamera(bool triggerEntered)
    {
        if (triggerEntered)
        {
            cameraSwitcher.DeactivateAllCameras();
        }
        else
        {
            cameraSwitcher.ReactivateCurrentCamera();
        }

        cameraSwitcher.enabled = !triggerEntered;
        triggeredCamera.gameObject.SetActive(triggerEntered);
    }
}
