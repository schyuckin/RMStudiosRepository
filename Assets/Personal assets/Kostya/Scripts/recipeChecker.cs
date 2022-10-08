using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class recipeChecker : MonoBehaviour
{
    [SerializeField]
    private string[] recipeName = {"Neutral", "Reddish", "Blueish", "Yellowish", "Red", "Blue",
        "Yellow", "Purple", "Orange", "Green", "Black"}; // Names of the recipes
    [SerializeField] private GameObject flask;
    [SerializeField] private GameObject teleporter;
    // [SerializeField] private GameObject wrongIngredients;
    // Start is called before the first frame update
    void Start()
    {
        RecipeChoice();
    }

    // Currently unused

    void RecipeChoice()
    {
        // This is where it has to check the recipe chosen by the randomizer
        this.GetComponent<description_list>();
    }

    void PotionReset()
    {
        // Resets the potion to default position and attributes
        flask.transform.position = teleporter.transform.position;
        var resetting = flask.GetComponent<flaskState>();
        resetting.isReset = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == flask.GetComponent<Collider>())
        {
            // !!!!!!!!!!!!!
            // PETERS SCRIPT SHOULD COME IN HERE PROBABLY, EITHER BY ENABLING ANOTHER OBJECT OR OUTRIGHT HAVING IT WITHIN THIS CODE
            // YOU HAVE TO CHECK WHAT IS IN THE DESCRIPTION LIST SCRIPT (MANDY'S FOLDER) AND FLASK STATE SCRIPT (KOSTYA'S FOLDER)   



            // Checks for all conditions
            //if (flaskChem.flaskBase == recipeState)
            //{
            //    if ((flaskTemp.flaskTemperature < recipeTemperature + 5) && (flaskTemp.flaskTemperature > recipeTemperature - 5))
            //    {
            //        if (flaskEnch.enchantmentType == enchantmentType)
            //        {
            //            if (corrOrder.isCorrectOrder)
            //            {
                               RecipeChoice();
                               PotionReset();
            //            }
            //        }

            //    }
            //}
            //else if (!(flaskChem.flaskBase == recipeState) || !((flaskTemp.flaskTemperature < recipeTemperature + 5)
            //    && (flaskTemp.flaskTemperature > recipeTemperature - 5)) || !(flaskEnch.enchantmentType == enchantmentType))
            //{
            //    wrongIngredients.SetActive(true);
            //}
        }
    }
}
