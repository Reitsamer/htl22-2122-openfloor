using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPlayerMovement : MonoBehaviour
{
    public float radius = 5;

    public float speed = 0.2f;
    
    private float alpha = 0f;

    private Vector3 center;
    
    // Start is called before the first frame update
    void Start()
    {
        center = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        alpha = (alpha + speed * Time.deltaTime * 360f) % 360f;
        float radians = alpha * Mathf.Deg2Rad;
        
        float x = Mathf.Sin(radians) * radius;
        float z = Mathf.Cos(radians) * radius;

        transform.position = center + new Vector3(x, 0, z);
    }
}


