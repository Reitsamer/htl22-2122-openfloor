using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardButton : MonoBehaviour
{
    public Material backMaterial;
    private Material frontMaterial;

    private bool showFrontMaterial = false;

    private int index;
    
    private void Start()
    {
        SetUsedMaterial();
    }

    public void SetIndex(int index)
    {
        this.index = index;
    }

    public void SetMaterial(Material material)
    {
        // this.GetComponent<Image>().material = material;
        frontMaterial = material;
    }

    public void OnClicked()
    {
        CardManager.Instance.CardClicked(index);
    }
    
    public void ChangeMaterial()
    {
        showFrontMaterial = !showFrontMaterial;
        
        SetUsedMaterial();
    }

    private void SetUsedMaterial()
    {
        if (showFrontMaterial)
        {
            this.GetComponent<Image>().material = frontMaterial;
        }
        else
        {
            this.GetComponent<Image>().material = backMaterial;
        }
    }
}
