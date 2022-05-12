using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour
{
    public Transform[] spawnPoints;
    public Transform rocketPrefab;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(rocketPrefab, spawnPoint.position, Quaternion.identity);
        }
    }
}
