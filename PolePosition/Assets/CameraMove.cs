using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public CinemachineVirtualCamera virtualCamera;
    public float speed = 1f;
    
    // Update is called once per frame
    void Update()
    {
        CinemachineTrackedDolly trackedDolly = virtualCamera.GetCinemachineComponent<CinemachineTrackedDolly>();
        trackedDolly.m_PathPosition += Time.deltaTime * speed;
    }
}
