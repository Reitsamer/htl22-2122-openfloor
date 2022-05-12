using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Camera camera;

    void Start()
    {
        if (camera == null)
            camera = Camera.main;
    }
    void LateUpdate()
    {
        transform.LookAt(transform.transform.position + camera.transform.forward);
    }
}
