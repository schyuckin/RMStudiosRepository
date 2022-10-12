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
    
    
    [Space]
    //Peter Stuff
    public flaskState _flaskState;

    [SerializeField] private int requiredBase;
    [SerializeField] private int requiredPot;
    [SerializeField] private int requiredSigil;
    
    [SerializeField] private int baseCol;
    [SerializeField] private int pot;
    [SerializeField] private int sigil;

    [SerializeField] private string potionName;
    [SerializeField] private int flaskName;

    private string reviewSentence;
    public GameObject reviewDisplay;
    void Start()
    {

    }
    // Currently unused

    void RecipeChoice()
    {
        // This is where it has to check the recipe chosen by the randomizer
        var getStuff = this.GetComponent<description_list>();
        requiredBase = getStuff.potionBase;
        requiredPot = getStuff.potionPotency;
        requiredSigil = getStuff.potionSigil;
    }

    void PotionReset()
    {
        // Resets the potion to default position and attributes
        flask.transform.position = teleporter.transform.position;
        var resetting = flask.GetComponent<flaskState>();
        resetting.isReset = true;
    }

    void GetPotionAttributes()
    {
        var _flaskState = flask.GetComponent<flaskState>();
        baseCol = _flaskState.flaskBase;
        pot = _flaskState.flaskPotency;
        sigil = _flaskState.sigilType;
    }

    private void OnTriggerEnter(Collider other)
    {
        RecipeChoice();
        if (other == flask.GetComponent<Collider>())
        {
            // !!!!!!!!!!!!!
            // PETERS SCRIPT SHOULD COME IN HERE PROBABLY, EITHER BY ENABLING ANOTHER OBJECT OR OUTRIGHT HAVING IT WITHIN THIS CODE
            // YOU HAVE TO CHECK WHAT IS IN THE DESCRIPTION LIST SCRIPT (MANDY'S FOLDER) AND FLASK STATE SCRIPT (KOSTYA'S FOLDER)  

            GetPotionAttributes();
            var stuff = this.GetComponent<description_list>();
            var flaskStuff = flask.GetComponent<flaskState>();
            flaskName = flaskStuff.flaskBase;

            // Checks for love potions
            if ((stuff.descriptionChosen == 0) || (stuff.descriptionChosen == 1) || (stuff.descriptionChosen == 2))
            {
                potionName = "LovePotion";
            }
            // Checks for sleep potions
            if ((stuff.descriptionChosen == 3) || (stuff.descriptionChosen == 4))
            {
                potionName = "SleepPotion";
            }

            if (requiredBase == baseCol && requiredPot == pot &&  requiredSigil == sigil)
            {
                FiveStarReview();
            }
            if (requiredBase == baseCol && requiredPot == pot &&  requiredSigil != sigil)
            {
                FourStarReview();
            }
            if (requiredBase == baseCol && requiredPot < pot)
            {
                TwoStarReview1();
            }
            if (requiredBase == baseCol && requiredPot > pot)
            {
                TwoStarReview2();
            }
            if (requiredBase != baseCol)
            {
                Incorrect();
            }
            reviewDisplay.SetActive(true);



            #region oldCode




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
            #endregion
        }
    }

    void FiveStarReview()
    {
        if (potionName == "LovePotion" && flaskName == 7)
        {
            reviewSentence = "The Potion was perfect, he is head over heels for me, we already planned our wedding for next week";
            reviewDisplay.GetComponent<TextMeshPro>().text = reviewSentence;
        }

        if (potionName == "SleepPotion" && flaskName == 9)
        {
            reviewSentence = "The potion was perfect, whenever my neighbor is being loud I just gift him some tea and enjoy the rest of the day!";
            reviewDisplay.GetComponent<TextMeshPro>().text = reviewSentence;
        }
    }
    void FourStarReview()
    {
        if (potionName == "LovePotion" && flaskName == 7)
        {
            reviewSentence = "The potion was almost perfect, he has fallen in love for me, however, his head caught on fire, I wanted a spark between us but that's not what I meant";
            reviewDisplay.GetComponent<TextMeshPro>().text = reviewSentence;
        }

        if (potionName == "SleepPotion" && flaskName == 9)
        {
            reviewSentence = "The potion was almost perfect, my neighbor is asleep, however he snores so much he causes small earthquakes in the building";
            reviewDisplay.GetComponent<TextMeshPro>().text = reviewSentence;
        }
    }
    void TwoStarReview1()
    {
        if (potionName == "LovePotion" && flaskName == 7)
        {
            reviewSentence = "I wanted him to love me, however this potion made him extremely obsessed with me! He doesnt leave the house and says he likes to watch me sleep!";
            reviewDisplay.GetComponent<TextMeshPro>().text = reviewSentence;
        }

        if (potionName == "SleepPotion" && flaskName == 9)
        {
            reviewSentence = "When I said I wanted my neighbor to be quiet, I expected a potion to make him unable to talk or unconscious! Not to kill him immediately! I am facing 5-to-10 for first degree murder.";
            reviewDisplay.GetComponent<TextMeshPro>().text = reviewSentence;
        }
    }
    
    void TwoStarReview2()
    {
        if (potionName == "LovePotion" && flaskName == 7)
        {
            reviewSentence = "I wanted him to love me, but he seems like he just wants to be my friend! He keeps inviting me to watch sports and play cards with him!";
            reviewDisplay.GetComponent<TextMeshPro>().text = reviewSentence;
        }

        if (potionName == "SleepPotion" && flaskName == 9)
        {
            reviewSentence = "I wanted my neighbor to be quiet but this potion just made him act really tired and weird, he's not much more quiet he just slurs his words and barely opens his eyes now";
            reviewDisplay.GetComponent<TextMeshPro>().text = reviewSentence;
        }
    }

    void Incorrect()
    {
        reviewSentence = "The potion was completely wrong";
        reviewDisplay.GetComponent<TextMeshPro>().text = reviewSentence;
    }
}
