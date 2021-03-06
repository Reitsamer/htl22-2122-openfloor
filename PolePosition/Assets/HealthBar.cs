using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image fillImage;

    public Gradient healthBarColor;

    public Slider slider;

    public float Health
    {
        get
        {
            return slider.normalizedValue;
        }
        set
        {
            slider.normalizedValue = Mathf.Clamp01(value);
            fillImage.color = healthBarColor.Evaluate(slider.normalizedValue);
        }
    }
    
    void Start()
    {
        slider.value = 1f;
        fillImage.color = healthBarColor.Evaluate(1f);
    }
    
    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     slider.value -= 0.1f;
        //     fillImage.color = healthBarColor.Evaluate(slider.value);
        // }
    }
}
