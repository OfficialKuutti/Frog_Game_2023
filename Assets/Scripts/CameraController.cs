using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Runtime.CompilerServices;

public class CameraController : MonoBehaviour
{
    public CinemachineVirtualCamera basic2dCamera;
    public CinemachineFramingTransposer cameraSettings;
    public CinemachineBasicMultiChannelPerlin shakeSettings;

    //ScreenShake Parameters:
    public float shakeduration = 1.0f;
    public float shakeamplitude = 4.0f;


    // Start is called before the first frame update
    void Start()
    {
        //basic2dCamera.m_Lens.OrthographicSize = 3f;
        
        cameraSettings = basic2dCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        shakeSettings = basic2dCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        //Example of Screen offset, handling with scripts!
        //cameraSettings.m_ScreenX = 0.1f;
    }
    public void ScreenShake()
    {
        StartCoroutine(ScreenShakeStart());
    }


    public IEnumerator ScreenShakeStart()
    {
        shakeSettings.m_AmplitudeGain = shakeamplitude;
        yield return new WaitForSeconds(shakeduration);
        shakeSettings.m_AmplitudeGain = 0f;
    }

    private void FixedUpdate()
    {
           
    }

    //CameraFlip, but it doesnt work like it should.

   /* public void FlipScreenX(bool facingRight)
    {

        if (facingRight)
        {
            cameraSettings.m_ScreenX = 1f;
        }

        else if (!facingRight)
        {
            cameraSettings.m_ScreenX = 5f;
        }
    }
   */

}