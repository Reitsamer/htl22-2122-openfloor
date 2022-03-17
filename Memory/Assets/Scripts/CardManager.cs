using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardManager : MonoBehaviour
{
    public CardButton[] cards;
    public Material[] materials;
    public GameObject gameOverScreen;

    // private List<int> selectedCards = new List<int>();
    private int[] selectedCards = new int[2];
    private int selectedCardsCount = 0;

    public int SelectedCardsCount
    {
        get
        {
            return selectedCardsCount;
        }
    }
    
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

        if (selectedCardsCount == 2)
            return;
        
        selectedCards[selectedCardsCount] = index;
        selectedCardsCount++;

        if (selectedCardsCount == 1)
            return;

        //
        // if (selectedCardsCount == 2)

        if (cards[selectedCards[0]].frontMaterial == cards[selectedCards[1]].frontMaterial)
        {
            // Sind gleich
            Debug.Log("Die Karten sind gleich.");
            Invoke("HideCards", 2f);
        }
        else
        {
            // Sind nicht gleich
            Debug.Log("Die Karten sind unterschiedlich.");
            Invoke("FlipOpenCards", 2f);
        }
    }

    private void HideCards()
    {
        selectedCardsCount = 0;
        foreach (var selectedCardIndex in selectedCards)
        {
            cards[selectedCardIndex].HideCard(true);
        }

        if (AllCardsHidden())
            gameOverScreen.SetActive(true);
    }

    private bool AllCardsHidden()
    {
        foreach (var card in cards)
        {
            if (!card.Hidden)
                return false;
        }

        return true;
    }
    
    private void FlipOpenCards()
    {
        selectedCardsCount = 0;
        foreach (var selectedCardIndex in selectedCards)
        {
            cards[selectedCardIndex].ShowFrontMaterial(false);
        }
    }
}