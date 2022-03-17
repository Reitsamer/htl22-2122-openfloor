using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private Rigidbody rb;

    public KeyCode forwards;
    public KeyCode backwards;
    public KeyCode steerRight;
    public KeyCode steerLeft;
    
    public float speed = 1f;
    public float rotation = 2f;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(forwards))
            rb.AddRelativeForce(Vector3.forward * speed, ForceMode.Acceleration);
        else if (Input.GetKey(backwards))
            rb.AddRelativeForce(Vector3.back * speed, ForceMode.Acceleration);
        
        if (Input.GetKey(steerLeft))
            transform.Rotate(Vector3.up, -rotation, Space.Self);
        else if (Input.GetKey(steerRight))
            transform.Rotate(Vector3.up, rotation, Space.Self);
    }
}
