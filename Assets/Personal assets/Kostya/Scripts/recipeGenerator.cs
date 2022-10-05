using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class recipeGenerator : MonoBehaviour
{
    [SerializeField]
    private string[] recipeName = {"Neutral", "Reddish", "Blueish", "Yellowish", "Red", "Blue",
        "Yellow", "Purple", "Orange", "Green", "Black"}; // Names of the recipes
    private int recipeState; // One of the random recipes
    private string chosenRecipeName; // Chosen recipe name to use in the UI
    private int recipeTemperature; // Chosen temperature of the recipe
    private int enchantmentType; // Chosen enchantment for the recipe
    [SerializeField] private GameObject[] uiPrompts = new GameObject[3]; // UI prompts showcasing the recipe
    [SerializeField] private GameObject flask;
    [SerializeField] private GameObject teleporter;
    [SerializeField] private GameObject wrongIngredients;
    // Start is called before the first frame update
    void Start()
    {
        RecipeCreation();
    }
    void RecipeCreation()
    {
        // Whole lot of stuff
        recipeState = (int)Random.Range(4, 10);
        chosenRecipeName = recipeName[recipeState]; // Chooses the recipe
        recipeTemperature = (int)Random.Range(30, 61); // Chooses the temperature
        enchantmentType = (int)Random.Range(1, 4); // Chooses the enchantment
        // Displays the changes on UI
        uiPrompts[0].GetComponent<TextMeshPro>().text = chosenRecipeName;
        var chooseTemp = recipeTemperature.ToString();
        uiPrompts[1].GetComponent<TextMeshPro>().text = chooseTemp;
        var chooseEnch = enchantmentType.ToString();
        uiPrompts[2].GetComponent<TextMeshPro>().text = chooseEnch;
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
            var flaskChem = flask.GetComponent<flaskState>();
            var flaskTemp = flask.GetComponent<flaskState>();
            var flaskEnch = flask.GetComponent<flaskState>();
            var corrOrder = flask.GetComponent<flaskState>();
            // Checks for all conditions
            if (flaskChem.flaskBase == recipeState)
            {
                if ((flaskTemp.flaskTemperature < recipeTemperature + 5) && (flaskTemp.flaskTemperature > recipeTemperature - 5))
                {
                    if (flaskEnch.enchantmentType == enchantmentType)
                    {
                        if (corrOrder.isCorrectOrder)
                        {
                            RecipeCreation();
                            PotionReset();
                        }
                    }

                }
            }
            else if (!(flaskChem.flaskBase == recipeState) || !((flaskTemp.flaskTemperature < recipeTemperature + 5)
                && (flaskTemp.flaskTemperature > recipeTemperature - 5)) || !(flaskEnch.enchantmentType == enchantmentType))
            {
                wrongIngredients.SetActive(true);
            }
        }
    }
}
