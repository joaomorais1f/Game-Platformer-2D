using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour {
    public float shakeDuration, shakeAmplitude, shakeFrequency;

    private float shakeElapsedTime;

    public CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin virtualCameraNoise;

    void Awake () {
        virtualCameraNoise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    void Start () {

    }

    void Update () {
        if (shakeElapsedTime > 0) {
            virtualCameraNoise.m_AmplitudeGain = shakeAmplitude;
            virtualCameraNoise.m_FrequencyGain = shakeFrequency;
            shakeElapsedTime -= Time.deltaTime;
        } else {
            virtualCameraNoise.m_AmplitudeGain = 0;
            shakeElapsedTime = 0;
        }
    }

    public void ShakeItLow() {
        shakeAmplitude = .3f;
        shakeElapsedTime = shakeDuration;

    }

    public void ShakeItMedium() {
        shakeAmplitude = .6f;
        shakeElapsedTime = shakeDuration;

    }
}