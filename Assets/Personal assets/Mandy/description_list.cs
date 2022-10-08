using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class description_list : MonoBehaviour
{
    public string[] customerRequests;
    public int potionBase;
    public int potionPotency;
    public int potionSigil;

    public int descriptionChosen;

    private string requestChosen;

    public GameObject descriptionShown;


    void Start()
    {
        descriptionChosen = Random.Range(0, 5);
        requestChosen = customerRequests[descriptionChosen];
        descriptionShown.GetComponent<TextMeshPro>().text = requestChosen;
        switch (descriptionChosen)
        {
            // Potion potency depends on the temperature
            //The higher the potency the stronger are the effects
            // Both the potion base and its sigil depend on the type of potion

            // Romantic potion, base ?, potency 1, sigil ?
            case 0:
                potionPotency = 1;
                break;
            // Romantic potion, base ?, potency 2, sigil ?
            case 1:
                potionPotency = 2;
                break;
            // Romantic potion, base ?, potency 3, sigil ?
            case 2:
                potionPotency = 3;
                break;
            // Soothing potion, base ?, potency 2, sigil ?
            case 3:
                potionPotency = 2;
                break;
            // Soothing potion, base ?, potency 3, sigil ?
            case 4:
                potionPotency = 3;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
