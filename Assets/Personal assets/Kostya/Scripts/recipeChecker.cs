using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class recipeChecker : MonoBehaviour
{

    [SerializeField] private GameObject flask;
    [SerializeField] private GameObject teleporter;

    [SerializeField] private Material reviewStars;
    [Space]

    [SerializeField] private int requiredBase;
    [SerializeField] private int requiredPot; // Desired properties for the currently chosen potion recipe
    [SerializeField] private int requiredSigil;

    [Space]

    [SerializeField] private int flaskBase;
    [SerializeField] private int flaskPot; // De facto properties of the potion, contained within a flask
    [SerializeField] private int flaskSigil;

    [SerializeField] private string potionName;

    private string reviewSentence = "Default message";
    public GameObject reviewDisplay;
    void Start()
    {

    }

    // Acquires needed recipe properties when it gets chosen
    public void RequestedProperties()
    {
        var getRequired = this.GetComponent<recipeGiver>();
        requiredBase = getRequired.potionBase;
        requiredPot = getRequired.potionPotency;
        requiredSigil = getRequired.potionSigil;
    }

    // Acquires actual potion properties when it gets submitted
    void DeFactoProperties()
    {
        var getActual = flask.GetComponent<flaskState>();
        flaskBase = getActual.flaskBase;
        flaskPot = getActual.flaskPotency;
        flaskSigil = getActual.sigilType;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == flask.GetComponent<Collider>())
        {
            DeFactoProperties();
            var curRequest = this.GetComponent<recipeGiver>();
            var corOrder = flask.GetComponent<flaskState>();

            switch (curRequest.potionType)
            {
                case "Love":
                    potionName = "LovePotion";
                    break;

                case "Soothe":
                    potionName = "SoothePotion";
                    break;
            }
                if (requiredBase == flaskBase && requiredPot == flaskPot && requiredSigil == flaskSigil && corOrder.isCorrectOrder)
                {
                    FiveStarReview();
                }
                if (requiredBase == flaskBase && requiredPot == flaskPot && requiredSigil != flaskSigil && corOrder.isCorrectOrder)
                {
                    FourStarReview();
                }
                if (requiredBase == flaskBase && requiredPot < flaskPot && corOrder.isCorrectOrder)
                {
                    TwoStarReview1();
                }
                if (requiredBase == flaskBase && requiredPot > flaskPot && corOrder.isCorrectOrder)
                {
                    TwoStarReview2();
                }
            else if (requiredBase != flaskBase || (!corOrder.isCorrectOrder))
            {
                IncorrectPotion();
            }

            // [NOTE] I am not sure about that part, see recipeGiver for more details
            var endCheck = this.GetComponent<recipeGiver>();
            if (endCheck.crashControl < 4)
            {
                endCheck.crashControl++;
                this.GetComponent<recipeGiver>().ChoosingRecipe();
                flask.transform.position = teleporter.transform.position;
            }
            this.GetComponent<recipeGiver>().disablingRequest(); // Hiding the request in favour of review
            // [NOTE] There MAY be a small delay where the player can see the next request before it disappears, can probably be fixed with some rearrangement
            reviewDisplay.GetComponent<TextMeshPro>().text = reviewSentence;
            reviewDisplay.SetActive(true); // Displaying the review
        }
    }

    /*[NOTE] The second part of the code that checks for the base is redundant if we keep all potions of the same type
     within the same colour scheme; the same thing applies to the recipeGiver script*/
    void FiveStarReview()
    {
        if (potionName == "LovePotion" && flaskBase == 7)
        {
            switch (requiredPot) {
                case 1:
                    reviewSentence = "Thank you so much for your potion! We are going on a date in a few days!";
                    break;
                case 2:
                    reviewSentence = "The potion was perfect! He is head over heels for me and we are already planning our wedding.";
                    break;
                case 3:
                    reviewSentence = "Thank you for the potion. I basically got a pet right now.";
                    break;
            }
        }

        if (potionName == "SoothePotion" && flaskBase == 8)
        {
            switch (requiredPot)
            {
                case 1:
                    reviewSentence = "Lmao we haven't thought of that yet";
                    break;
                case 2:
                    reviewSentence = "The potion was perfect! Whenever my neighbor is being loud, I just gift him some tea and enjoy the rest of the day!";
                    break;
                case 3:
                    reviewSentence = "Thanks a lot! One cup of tea was just enough. Like my new neighbour way more, too.";
                    break;
            }
                    
        }
        reviewStars.SetFloat("ProgressBorder", 0);
    }
    void FourStarReview()
    {
        if (potionName == "LovePotion" && flaskBase == 7)
        {
            switch (requiredPot)
            {
                case 1:
                    reviewSentence = "She does seem interested in me, but she just can't stop talking about lizards whenever we go out... Okay, I guess...";
                    break;
                case 2:
                    reviewSentence = "I guess it worked? He has fallen for me, but his head caught on fire! I wanted a spark between us but not like this!";
                    break;
                case 3:
                    reviewSentence = "Okay, I guess I had it coming when I said 'obsessed', but for some reason he treats me as an Egyptian god?";
                    break;
            }
                  
        }

        if (potionName == "SoothePotion" && flaskBase == 8)
        {
            switch (requiredPot)
            {
                case 1:
                    reviewSentence = "Lmao we haven't thought of that yet";
                    break;
                case 2:
                    reviewSentence = "My neighbor is asleep, which is good. His snoring, however, causes our entire building to shake.";
                    break;
                case 3:
                    reviewSentence = "Thank you for helping with my neighbour, but doing that to his wife was unneccessary...";
                    break;
            }
        }
        reviewStars.SetFloat("ProgressBorder", -0.033f);
    }
    void TwoStarReview1() // Potion too strong
    {
        if (potionName == "LovePotion" && flaskBase == 7)
        {
            switch (requiredPot)
            {
                case 1:
                    reviewSentence = "Okay, I just wanted to flirt and she's way too intense...";
                    break;
                case 2:
                    reviewSentence = "I said I wanted him to love me, not follow me like a dog!";
                    break;
            }
        }

        if (potionName == "SoothePotion" && flaskBase == 8)
        {
            switch (requiredPot)
            {
                case 1:
                    reviewSentence = "Lmao we haven't thought of that yet";
                    break;
                case 2:
                    reviewSentence = "I didn't mean it like that! I only wanted him to sleep! Jesus Christ, who hired you?";
                    break;
            }
        }
        reviewStars.SetFloat("ProgressBorder", -0.0615f);
    }
    void TwoStarReview2() // Potion too weak
    {
        if (potionName == "LovePotion" && flaskBase == 7)
        {
            switch (requiredPot)
            {
                case 2:
                    reviewSentence = "Ugh, she's not much better! I still have to woo her!";
                    break;
                case 3:
                    reviewSentence = "This is not enough passion! Did you not do it because you think you know better?";
                    break;
            }
        }

        if (potionName == "SoothePotion" && flaskBase == 8)
        {
            switch (requiredPot)
            {
                case 2:
                    reviewSentence = "He is not quiet, he is just annoying now! Acts weird, too.";
                    break;
                case 3:
                    reviewSentence = "You don't get the slang, do you? Sleeping is not what I meant by 'gone'!";
                    break;
            }
        }
        reviewStars.SetFloat("ProgressBorder", -0.0615f);
    }
    void IncorrectPotion()
    {
        reviewSentence = "This is not what I ordered at all! What did you do?!";
        reviewStars.SetFloat("ProgressBorder", -1);
    }
}
