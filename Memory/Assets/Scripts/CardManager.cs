using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardManager : MonoBehaviour
{
    public Card[] cards;
    public Material[] materials;

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
            cards[i].SetMaterial(listOfMaterials[index]);
            listOfMaterials.RemoveAt(index);
        }
    }
}
