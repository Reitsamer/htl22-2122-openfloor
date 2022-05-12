using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBrain : MonoBehaviour
{
    [SerializeField]
    private HealthBar healthBar;

    [SerializeField] private float hitDamage = 0.01f;
    
    // Start is called before the first frame update
    void Start()
    {
        if (healthBar == null)
        {
            healthBar = GetComponentInChildren<HealthBar>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GotHit()
    {
        healthBar.Health -= hitDamage;
    }
}
