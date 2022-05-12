using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class SimpleCarController : MonoBehaviour {
    public List<AxleInfo> axleInfos; // the information about each individual axle
    public float maxMotorTorque; // maximum torque the motor can apply to wheel
    public float maxSteeringAngle; // maximum steer angle the wheel can have

    public Transform gunBarrelLeft;
    public Transform gunBarrelRight;

    public GameObject gunFireLeft;
    public GameObject gunFireRight;

    public float fireFrequency = 5f;
    private float remainingTime;
    
    // finds the corresponding visual wheel
    // correctly applies the transform
    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0) {
            return;
        }
     
        Transform visualWheel = collider.transform.GetChild(0);
     
        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);
     
        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }

    public void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            remainingTime -= Time.deltaTime;
            if (remainingTime < 0)
            {
                remainingTime = 1/fireFrequency;
                gunFireLeft.SetActive(!gunFireLeft.activeSelf);
                gunFireRight.SetActive(!gunFireRight.activeSelf);
            }
        }
        else
        {
            remainingTime = 1/fireFrequency;
            gunFireLeft.SetActive(false);
            gunFireRight.SetActive(false);
        }
    }
    
    public void FixedUpdate()
    {
        float speed = IsOnRoad() ? 1f : 0.25f;  // ternary operator
        
        float motor = maxMotorTorque * speed * Input.GetAxis("Vertical");
        float steering = maxSteeringAngle * Input.GetAxis("Horizontal");
            
        foreach (AxleInfo axleInfo in axleInfos) {
            if (axleInfo.steering) {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor) {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }
            ApplyLocalPositionToVisuals(axleInfo.leftWheel);
            ApplyLocalPositionToVisuals(axleInfo.rightWheel);
        }
    }

    private bool IsOnRoad() => 
        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), Mathf.Infinity, 1 << 6);

    private bool HitsEnemy(Transform barrelTransform)
    {
        return Physics.Raycast(barrelTransform.position, transform.TransformDirection(Vector3.forward), Mathf.Infinity, 1 << 7);
    }

    private Vector3 GetHitPosition(Transform barrelTransform)
    {
        Physics.Raycast(
            barrelTransform.position, 
            transform.TransformDirection(Vector3.forward), 
            out RaycastHit hitInfo, 
            Mathf.Infinity, 1 << 7);

        return hitInfo.point;
    }
    
    private void OnDrawGizmos()
    {
        Color oldColor = Gizmos.color;
        
        Gizmos.color = IsOnRoad() ? Color.green : Color.red;
        Gizmos.DrawSphere(transform.position + Vector3.up * 2, 0.25f);

        Gizmos.color = HitsEnemy(gunBarrelLeft) ? Color.green : Color.red;
        Gizmos.DrawRay(gunBarrelLeft.position, transform.TransformDirection(Vector3.forward)*10);
        Gizmos.color = HitsEnemy(gunBarrelRight) ? Color.green : Color.red;
        Gizmos.DrawRay(gunBarrelRight.position, transform.TransformDirection(Vector3.forward)*10);

        if (HitsEnemy(gunBarrelLeft))
        {
            Vector3 hitPoint = GetHitPosition(gunBarrelLeft);
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(hitPoint, 0.1f);
        }
        
        if (HitsEnemy(gunBarrelRight))
        {
            Vector3 hitPoint = GetHitPosition(gunBarrelRight);
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(hitPoint, 0.1f);
        }
        
        Gizmos.color = oldColor;
    }
}
    
[System.Serializable]
public class AxleInfo {
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor; // is this wheel attached to motor?
    public bool steering; // does this wheel apply steer angle?
}