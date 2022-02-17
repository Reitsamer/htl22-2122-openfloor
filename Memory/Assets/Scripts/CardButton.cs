using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardButton : MonoBehaviour
{
    public void SetMaterial(Material material)
    {
        this.GetComponent<Image>().material = material;
    }
}
