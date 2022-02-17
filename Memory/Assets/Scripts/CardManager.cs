using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardManager : MonoBehaviour
{
    public CardButton[] cards;
    public Material[] materials;

    // private List<int> selectedCards = new List<int>();
    private int[] selectedCards = new int[2];
    private int selectedCardsCount = 0;
    
    private static CardManager instance;
    public static CardManager Instance
    {
        get
        {
            return instance;
        }
    }

    private CardManager()
    {
        
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        List<Material> listOfMaterials = new List<Material>(materials.Length * 2);
        for (int i = 0; i < materials.Length; i++)
        {
            for (int j = 0; j < 2; j++)
            {
                listOfMaterials.Add(materials[i]);
            }
        }
        
        for (int i = 0; i < cards.Length; i++)
        {
            int index = Random.Range(0, listOfMaterials.Count);
            cards[i].SetIndex(i);
            cards[i].SetMaterial(listOfMaterials[index]);
            listOfMaterials.RemoveAt(index);
        }
    }

    public void CardClicked(int index)
    {
        Debug.Log($"Card clicked: #{index}");
        
        // selectedCardsCount++;
        // if (selectedCardsCount == 1)
        // {
        //     
        // }
        //
        // if selectedCardsCount == 2)
        // {
        //     
        // }
    }
}