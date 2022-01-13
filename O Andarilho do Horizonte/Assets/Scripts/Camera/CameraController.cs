using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class CameraController : MonoBehaviour
{
   /* public GameObject shakeFX;
    public float shakeDuration;

    //public CinemachineVirtualCamera virtualCamera;

    public CinemachineImpulseSource impulse;


    private void Start()
    {

        shakeFX.SetActive(false);
    //    impulse = transform.GetComponent<CinemachineImpulseSource>();
      //  Invoke("CameraShake", 3f);
    }
    /*public void CameraShake()
    {

        impulse.GenerateImpulse(10f);
        //StartCoroutine("Shake");
        

    }*/

   /* IEnumerator Shake(float t)
    {
        shakeFX.SetActive(true);
        yield return new WaitForSeconds(t);
        shakeFX.SetActive(false);
    }*/

   /* public IEnumerator Shake()
    {
        virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 3f;
        virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 3f;

        yield return new WaitForSeconds(.5f);

        virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0f;
        virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0f;
        

        


    }*/
}
