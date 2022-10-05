using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterState : MonoBehaviour
{
    [SerializeField]
    private string[] States = {"Neutral", "Reddish", "Blueish", "Yellowish", "Red", "Blue",
        "Yellow", "Purple", "Orange", "Green", "Black"}; // All possible states of the base
    public Material[] Colours = new Material[11]; // Materials for each of the states
    public int currentState = 0; // Manages both of the above arrays by associating a particular material with a certain colour
    public int ingredientControl = 0; // Amount of chemicals currently in the water
    public GameObject cauldronControl; // Connection to the controller
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Different function based on amount of chemicals
        if (ingredientControl == 1)
        {
            OneChemical();
        }

        if (ingredientControl == 2)
        {
            TwoChemicals();
        }

        // Three and more ingridients always trigger Black state
        if (ingredientControl >= 3)
        {
            currentState = 10;
        }
        this.GetComponent<MeshRenderer>().material = Colours[currentState];
        Debug.Log("Current state is " + States[currentState]);
        this.enabled = false;
    }

    void OneChemical() // Checks one chemical in the cauldron
    {
        // Needs to check the first ingridient in the baseIngridients list
        // Can trigger states 1, 2 and 3
        var Chemical = cauldronControl.GetComponent<cauldronController>();
        switch (Chemical.baseIngredients[0])
        {
            case "Red":
                currentState = 1;
                break;
            case "Blue":
                currentState = 2;
                break;
            case "Yellow":
                currentState = 3;
                break;
        }
    }

    void TwoChemicals() // Checks two chemicals in the cauldron
    {
        // Needs to check both the first and the second ingridient in the baseIngridients list
        // Can trigger states 4,5,6,7,8,9
        var Chemical = cauldronControl.GetComponent<cauldronController>();

        // Mono colours code
        // Doesnt seem to work with equation of all three parts
        if (Chemical.baseIngredients[0] == "Red" && Chemical.baseIngredients[1] == "Red")
        {
            currentState = 4; // Red
        }
        if (Chemical.baseIngredients[0] == "Blue" && Chemical.baseIngredients[1] == "Blue")
        {
            currentState = 5; // Blue
        }
        if (Chemical.baseIngredients[0] == "Yellow" && Chemical.baseIngredients[1] == "Yellow")
        {
            currentState = 6; // Yellow
        }

        // Double colours code, checks for position of both regardless of which was placed first
        // Can probably be optimized
        if ((Chemical.baseIngredients[0] == "Red" && Chemical.baseIngredients[1] == "Blue")
            || (Chemical.baseIngredients[0] == "Blue" && Chemical.baseIngredients[1] == "Red"))
        {
            currentState = 7; // Purple
        }
        if ((Chemical.baseIngredients[0] == "Red" && Chemical.baseIngredients[1] == "Yellow")
            || (Chemical.baseIngredients[0] == "Yellow" && Chemical.baseIngredients[1] == "Red"))
        {
            currentState = 8; // Orange
        }
        if ((Chemical.baseIngredients[0] == "Blue" && Chemical.baseIngredients[1] == "Yellow")
           || (Chemical.baseIngredients[0] == "Yellow" && Chemical.baseIngredients[1] == "Blue"))
        {
            currentState = 9; // Green
        }
    }
}
