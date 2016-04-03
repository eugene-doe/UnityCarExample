using UnityEngine;
using System.Collections;

public class CameraTrigger : MonoBehaviour
{
    public CameraSwitcher cameraSwitcher;
    public Camera triggeredCamera;
    public float duration = 1f;

    void Start()
    {
        triggeredCamera.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Car"))
        {
            StartCoroutine(TriggerCameraForSeconds(duration));
        }
    }

    IEnumerator TriggerCameraForSeconds(float duration)
    {
        cameraSwitcher.enabled = false;
        cameraSwitcher.DeactivateAllCameras();
        triggeredCamera.gameObject.SetActive(true);

        yield return new WaitForSeconds(duration);

        triggeredCamera.gameObject.SetActive(false);
        cameraSwitcher.ReactivateCurrentCamera();
        cameraSwitcher.enabled = true;
    }

}
