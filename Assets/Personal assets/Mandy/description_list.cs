using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class description_list : MonoBehaviour
{
    public string[] customerRequests;
    public int potionPotency;

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
            case 0:
                potionPotency = 1;
                break;
            case 1:
                potionPotency = 2;
                break;
            case 2:
                potionPotency = 3;
                break;
            case 3:
                potionPotency = 2;
                break;
            case 4:
                potionPotency = 3;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

 // Potion potency depends on the recipe
 //The higher the potency the stronger are the effects
}
