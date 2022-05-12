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

    [SerializeField]
    private float speedMultiplier = 1f;
    
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
                if (gunFireLeft.activeSelf)
                {
                    ShootGun(gunFireLeft.transform);
                }

                if (gunFireRight.activeSelf)
                {
                    ShootGun(gunFireRight.transform);
                }
            }
        }
        else
        {
            remainingTime = 1/fireFrequency;
            gunFireLeft.SetActive(false);
            gunFireRight.SetActive(false);
        }
    }

    private void ShootGun(Transform gun)
    {
        if (GetHitPosition(gun, out RaycastHit hitInfo))
        {
            Transform enemy = hitInfo.transform;
            CarBrain enemyCarBrain = enemy.GetComponent<CarBrain>();
            enemyCarBrain.GotHit();
        }
    }

    public void FixedUpdate()
    {
        float speed = (IsOnRoad() ? 1f : 0.25f) * speedMultiplier;  // ternary operator
        
        var acceleration = Input.GetAxis("Vertical");
        if (acceleration < 0)
            acceleration *= 2;

        float motor = maxMotorTorque * speed * acceleration;
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

    private bool GetHitPosition(Transform barrelTransform, out RaycastHit hitPoint)
    {
        bool hitSuccess = Physics.Raycast(
            barrelTransform.position, 
            transform.TransformDirection(Vector3.forward), 
            out hitPoint, 
            Mathf.Infinity,
            1 << 7);

        return hitSuccess;
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
            if (GetHitPosition(gunBarrelLeft, out RaycastHit hitInfo))
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(hitInfo.point, 0.1f);
            }
        }
        
        if (HitsEnemy(gunBarrelRight))
        {
            if (GetHitPosition(gunBarrelRight, out RaycastHit hitInfo))
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(hitInfo.point, 0.1f);
            }
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