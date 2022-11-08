using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class recipeGiver : MonoBehaviour
{
    [SerializeField] private List<string> customerRequests = new List<string>(); // List with all of the recipes
    public List<string> customerNames = new List<string>(); // List with all customer names
    [SerializeField] private string[] potionTypes = new string[] {"Love", "Soothe", "Energy"};
    public int descriptionChosen = -1; // Number of the recipe that is currently requested, used in the recipeChecker script
    private string requestChosen; // Text of the current request
    [SerializeField] private GameObject descriptionShownText; // Display of the current request in the overworld
    [SerializeField] private GameObject descriptionShown; // More stuff Don't ask
    [SerializeField] private GameObject nameShown; // Name of the person whose request is being processed
    public int crashControl = 0;

    [Space]

    public int potionBase;
    public int potionPotency; // Variables passed to the recipeChecker that define the properties of the current request
    public int potionSigil;
    public string potionType;

    [Space]

    [SerializeField] private bool manualActivation = false;


    public void ChoosingRecipe()
    {
        // Chooses a random recipe and displays it in the overworld
        while (customerRequests[descriptionChosen] == "null" && crashControl < customerRequests.Count)
        {
            descriptionChosen = Random.Range(0, customerRequests.Count);
        }
        crashControl++;
        requestChosen = customerRequests[descriptionChosen];
        descriptionShownText.GetComponent<TextMeshPro>().text = requestChosen;
        nameShown.GetComponent<TextMeshPro>().text = customerNames[descriptionChosen];

        // descriptionChosen shows the position of the ORIGINAL element in array, NOT the one seen in Inspector in its place
        switch (descriptionChosen)
        {

            /* [NOTE] The cases are deliberately declared in such a way that we can easily manipulate each of the recipes
            This can be used if we DO decide to use different colours for the same potion later on */

            // Girl from alchemstry class
            // Magenta (R + B) [4], [Potency 1], Mind Sigil (1), Love

            case 0:
                potionBase = 4;
                potionPotency = 1;
                potionSigil = 1;
                potionType = potionTypes[0];
                break;

            // Wizard in love
            //Magenta (R + B) [4], [Potency 2], Mind Sigil (1), Love

            case 1:
                potionBase = 4;
                potionPotency = 2;
                potionSigil = 1;
                potionType = potionTypes[0];
                break;

            // Worshipping dude
            //Magenta (R + B) [4], [Potency 3], Mind Sigil (1), Love

            case 2:
                potionBase = 4;
                potionPotency = 3;
                potionSigil = 1;
                potionType = potionTypes[0];
                break;

            // Trouble with sleep
            // Green (B + Y) [6], [Potency 1], Body Sigil (2), Soothe

            case 3:
                potionBase = 6;
                potionPotency = 1;
                potionSigil = 1;
                potionType = potionTypes[1];
                break;

            // Loud neighbour
            // Green (B + Y) [6], [Potency 2], Body Sigil (2), Soothe

            case 4:
                potionBase = 6;
                potionPotency = 2;
                potionSigil = 2;
                potionType = potionTypes[1];
                break;

            // Roommate murder
            //Orange (B + Y) [6], [Potency 3], Body Sigil (2), Soothe

            case 5:
                potionBase = 6;
                potionPotency = 3;
                potionSigil = 2;
                potionType = potionTypes[1];
                break;

            // Work project boost
            // Orange (R + B) [5], [Potency 1], Soul Sigil (3), Energy

            case 6:
                potionBase = 5;
                potionPotency = 1;
                potionSigil = 3;
                potionType = potionTypes[2];
                break;

            // Marathon thing
            // Orange (R + B) [5], [Potency 2], Soul Sigil (3), Energy

            case 7:
                potionBase = 5;
                potionPotency = 2;
                potionSigil = 3;
                potionType = potionTypes[2];
                break;

            // Walking through the country
            // Orange (R + B) [5], [Potency 3], Soul Sigil (3), Energy

            case 8:
                potionBase = 5;
                potionPotency = 3;
                potionSigil = 3;
                potionType = potionTypes[2];
                break;
        }

        // Passes the values of the chosen recipe to the Checker script
        // [NOTE] *this* has to be replaced if the recipeChecker script gets reattached to a different object
        this.GetComponent<recipeChecker>().RequestedProperties();

        // Removes the last request so that it cannot be chosen again
        // customerRequests.Remove(customerRequests[descriptionChosen]);
        customerRequests[descriptionChosen] = "null";

        if (crashControl >= customerRequests.Count)
        {
            NukeScene();
        }
    }
    void Start()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
        descriptionChosen = Random.Range(0, customerRequests.Count);
        ChoosingRecipe();
    }

    public void disablingRequest()
    {
        descriptionShown.SetActive(false);
    }

    public void NukeScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Triggering recipes from the Inspector
    private void Update()
    {
        if (manualActivation)
        {
            manualActivation = false;
            // [NOTE] This is needed for preventing what seems to be either an infinite loop or a stack overflow
            if (crashControl < customerRequests.Count)
            {
                this.GetComponent<recipeGiver>().ChoosingRecipe();
            }
            else
            {
                NukeScene();
            }
        }
    }
}
