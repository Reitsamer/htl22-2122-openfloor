using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SantaCollisionManager : MonoBehaviour
{
    [SerializeField]
    private Transform spawnPoint;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Boundary"))
        {
            transform.position = spawnPoint.position;
        }
        else if (other.CompareTag("Finish"))
        {
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }
    }
}
