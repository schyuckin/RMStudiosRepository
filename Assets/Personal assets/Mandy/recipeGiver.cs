using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class recipeGiver : MonoBehaviour
{
    [SerializeField] private List<string> customerRequests = new List<string>(); // List with all of the recipes
    [SerializeField] private string[] potionTypes = new string[] {"Love", "Soothe"};
    public int descriptionChosen = -1; // Number of the recipe that is currently requested, used in the recipeChecker script
    private string requestChosen; // Text of the current request
    public GameObject descriptionShown; // Display of the current request in the overworld

    [Space]

    public int potionBase;
    public int potionPotency; // Variables passed to the recipeChecker that define the properties of the current request
    public int potionSigil;
    public string potionType;

    [Space]

    [SerializeField] private bool manualActivation = false;
    public int crashControl = 0;

    public void ChoosingRecipe()
    {
        // Chooses a random recipe and displays it in the overworld
        while (customerRequests[descriptionChosen] == "null")
        {
            descriptionChosen = Random.Range(0, customerRequests.Count);
        }
        requestChosen = customerRequests[descriptionChosen];
        descriptionShown.GetComponent<TextMeshPro>().text = requestChosen;

        // descriptionChosen shows the position of the ORIGINAL element in array, NOT the one seen in Inspector in its place
        switch (descriptionChosen)
        {

            /* [NOTE] The cases are deliberately declared in such a way that we can easily manipulate each of the recipes
            This can be used if we DO decide to use different colours for the same potion later on */

            //"I wish she liked me back"
            // Magenta (R + B) [7], [Potency 1], Mind Sigil (1), Love

            case 0:
                potionBase = 7;
                potionPotency = 1;
                potionSigil = 1;
                potionType = potionTypes[0];
                break;

            //"I want this wizard to be desperately in love with me"
            //Magenta (R + B) [7], [Potency 2], Mind Sigil (1), Love

            case 1:
                potionBase = 7;
                potionPotency = 2;
                potionSigil = 1;
                potionType = potionTypes[0];
                break;

            //"I want him to worship me"
            //Magenta (R + B) [7], [Potency 3], Mind Sigil (1), Love

            case 2:
                potionBase = 7;
                potionPotency = 3;
                potionSigil = 1;
                potionType = potionTypes[0];
                break;

            //"My neighbor is so loud, I wish he could be quiet for once"
            //Orange (R + Y) [8], [Potency 2], Body Sigil (2), Soothe

            case 3:
                potionBase = 8;
                potionPotency = 2;
                potionSigil = 2;
                potionType = potionTypes[1];
                break;

            //"I want them gone..."
            //Orange (R + Y) [8], [Potency 3], Body Sigil (2), Soothe

            case 4:
                potionBase = 8;
                potionPotency = 3;
                potionSigil = 2;
                potionType = potionTypes[1];
                break;
        }

        // Passes the values of the chosen recipe to the Checker script
        // [NOTE] *this* has to be replaced if the recipeChecker script gets reattached to a different object
        this.GetComponent<recipeChecker>().RequestedProperties();

        // Removes the last request so that it cannot be chosen again
        // customerRequests.Remove(customerRequests[descriptionChosen]);
        customerRequests[descriptionChosen] = "null";
    }
    void Start()
    {
        // First cycle is started manually due to a while-loop condition
        descriptionChosen = Random.Range(0, customerRequests.Count);
        ChoosingRecipe();
    }

    // Triggering recipes from the Inspector
    private void Update()
    {
        if (manualActivation)
        {
            manualActivation = false;
            // [NOTE] This is needed for preventing what seems to be either an infinite loop or a stack overflow
            if (crashControl < 4)
            {
                crashControl++;
                this.GetComponent<recipeGiver>().ChoosingRecipe();
            }
        }
    }
}
