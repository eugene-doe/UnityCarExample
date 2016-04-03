using UnityEngine;
using System;
using System.Collections;
using System.Linq;

public class CameraSwitcher : MonoBehaviour
{
    public Camera[] cameras;

    private Camera currentCamera;

    void Start()
    {
        // Activate the first camera and deactivate all others
        ActivateCamera(0);
    }

    // Update is called once per frame
    void Update()
    {
        // GetButtonDown() returns true only on the frame when the button is pressed.
        // If we wanted the condition to remain true as long as the button is held,
        // we would use GetButton(). The buttons and axes are configured in Edit ->
        // Project Settings -> Input.
        if (Input.GetButtonDown("Camera"))
        {
            SwitchCamera();
        }
    }

    void SwitchCamera()
    {
        // Some cameras may be children of disabled game objects. We want to exclude them.
        var usableCameras = cameras.Where(c => c.transform.parent.gameObject.activeInHierarchy).ToArray();
        var usableIndexes = usableCameras.Select(c => Array.IndexOf(cameras, c)).ToArray();

        if (usableCameras.Length < 1) { return; } // No cameras to switch to

        int currentIndex = Array.IndexOf(cameras, currentCamera);
        int newIndex = -1;

        try
        {
            // Trying to find a usable index that is greater than the current one
            newIndex = usableIndexes.First(i => i > currentIndex);
        }
        catch
        {
            // If no such index is found, an exception is thrown and we use the first one instead
            newIndex = usableIndexes[0];
        }

        ActivateCamera(newIndex);
    }

    void ActivateCamera(int index)
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(i == index);
            if (i == index) { currentCamera = cameras[index]; }
        }
    }

    public void DeactivateAllCameras()
    {
        ActivateCamera(-1);
    }

    public void ReactivateCurrentCamera()
    {
        ActivateCamera(Array.IndexOf(cameras, currentCamera));
    }
}
