using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public void SetMaterial(Material material)
    {
        this.GetComponent<MeshRenderer>().sharedMaterial = material;
    }
}
