using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class CameraSwapper : MonoBehaviour
{
    public CinemachineVirtualCamera primaryCamera;
    public CinemachineVirtualCamera secondaryCamera;

    private bool isPrimaryLive;

    // Start is called before the first frame update
    void Start()
    {
        isPrimaryLive = true;
    }

    void OnSwap(InputValue swapValue)
    {
        if (swapValue.isPressed)
        {
            SwitchCamera();
        }
    }

    private void SwitchCamera()
    {
        if(isPrimaryLive)
        {
            secondaryCamera.enabled = true;
            primaryCamera.enabled = false;

        }
        else
        {
            primaryCamera.enabled = true;
            secondaryCamera.enabled = false;
        }

        isPrimaryLive = !isPrimaryLive;
    }
}
