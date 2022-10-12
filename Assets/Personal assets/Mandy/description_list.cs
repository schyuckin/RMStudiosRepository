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

            //"I wish she liked me back"[Potency 1]
//Colour: Magenta
//Red + Blue = Magenta
//Blue mushroom +Spider Eye
//Heat: 25 degrees
//Psyche: Mind

            case 0:
                potionBase = 7;
                potionPotency = 1;
                break;

                //"My arm got blasted off in battle"[Potency 2]
//Colour: Green
//Blue + Yellow = Green
//Blue mushroom +Concentrated stardust
//Heat: 78 degrees
//Psyche: Body

            case 1:
                potionBase = 9;
                potionPotency = 2;
                break;

                //"I want them to worship me"[Potency 3]
//Colour: (dark)purple
//Red + Blue = Magenta
//Blue mushroom +Spider Eye
//Heat: 96 degrees
//Psyche: Mind

            case 2:
                potionBase = 7;
                potionPotency = 3;
                break;

                //"My neighbor is so loud, I wish he could be quiet for once"[Potency 2]
//Colour: Orange
//Red + Yellow = Orange
//Spider eye +Concentrated stardust
//Heat: 65 degrees
//Psyche: Body

            case 3:
                potionBase = 8;
                potionPotency = 2;
                break;

                //"I want them gone..."[Potency 3]
//Colour: (Dark)green
//Yellow + Blue
//Concentrated stardust +Blue mushroom
//Heat: 99 degrees
//Psyche: Body

            case 4:
                potionBase = 9;
                potionPotency = 3;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
