using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardButton : MonoBehaviour
{
    public Material backMaterial;
    public Material frontMaterial;

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
        if (showFrontMaterial)
            return;
        
        if (CardManager.Instance.SelectedCardsCount < 2)
            ShowFrontMaterial(true);
        
        CardManager.Instance.CardClicked(index);
    }

    public bool Hidden
    {
        get
        {
            return !GetComponent<Image>().enabled;
        }
    }
    
    public void HideCard(bool flag)
    {
        GetComponent<Image>().enabled = !flag;
    }
    
    public void ShowFrontMaterial(bool flag)
    {
        showFrontMaterial = flag;
        
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
