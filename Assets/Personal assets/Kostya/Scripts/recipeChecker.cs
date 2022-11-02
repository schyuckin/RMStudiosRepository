using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class recipeChecker : MonoBehaviour
{

    [SerializeField] private GameObject flask;
    [SerializeField] private GameObject teleporter;
    [SerializeField] public Material reviewStars;

    [Space]

    [SerializeField] private int requiredBase;
    [SerializeField] private int requiredPot; // Desired properties for the currently chosen potion recipe
    [SerializeField] private int requiredSigil;

    [Space]

    [SerializeField] private int flaskBase;
    [SerializeField] private int flaskPot; // De facto properties of the potion, contained within a flask
    [SerializeField] private int flaskSigil;
    [SerializeField] private flaskState flaskProp;
    [SerializeField] private string potionName;

    [Space]

    private string reviewSentence = "If you see this, it means that potionmaking went wrong. We apologize.";
    private bool orderDelay = false;
    private float droppingDelay = 1.0f;
    public GameObject reviewDisplayText;
    public GameObject reviewDisplayEntire;
    [SerializeField] private cauldronController resettingCauldron;
    void Start()
    {
        reviewStars.SetFloat("_ProgressBorder", -1); // Resetting the material back to normal
    }

    private void Update()
    {
        if (droppingDelay > 0)
        {
            if (orderDelay)
            {
                droppingDelay -= Time.deltaTime;
            }
        }
        else
        {
            droppingDelay = 0;
            orderDelay = false;
        }
    }

    // Acquires needed recipe properties when it gets chosen
    public void RequestedProperties()
    {
        var getRequired = this.GetComponent<recipeGiver>();
        requiredBase = getRequired.potionBase;
        requiredPot = getRequired.potionPotency;
        requiredSigil = getRequired.potionSigil;
    }

    // Acquires actual potion properties when the flask gets submitted
    void DeFactoProperties()
    {
        var getActual = flask.GetComponent<flaskState>();
        flaskBase = getActual.flaskBase;
        flaskPot = getActual.flaskPotency;
        flaskSigil = getActual.sigilType;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == flask.GetComponent<Collider>() && !orderDelay)
        {
            orderDelay = true;
            DeFactoProperties();
            var curRequest = this.GetComponent<recipeGiver>();
            var corOrder = flask.GetComponent<flaskState>();
            flask.GetComponent<flaskState>().SettingProperties();
            resettingCauldron.SettingUp();

            switch (curRequest.potionType)
            {
                case "Love":
                    potionName = "LovePotion";
                    break;

                case "Soothe":
                    potionName = "SoothePotion";
                    break;
            }
            if (!flaskProp.deadPotion)
            {
                if (requiredBase == flaskBase && requiredPot == flaskPot && requiredSigil == flaskSigil)
                {
                    FiveStarReview();
                }
                if (requiredBase == flaskBase && requiredPot == flaskPot && requiredSigil != flaskSigil)
                {
                    FourStarReview();
                }
                if (requiredBase == flaskBase && requiredPot < flaskPot)
                {
                    TwoStarReview1();
                }
                if (requiredBase == flaskBase && requiredPot > flaskPot)
                {
                    TwoStarReview2();
                }
                else if (requiredBase != flaskBase)
                {
                    IncorrectPotion();
                }
            }
            else
            {
                DeadPotion();
            }
            var endCheck = this.GetComponent<recipeGiver>();
            if (endCheck.crashControl < 4)
            {
                endCheck.crashControl++;
                this.GetComponent<recipeGiver>().ChoosingRecipe();
                flask.transform.position = teleporter.transform.position;
            }
            this.GetComponent<recipeGiver>().disablingRequest(); // Hiding the request in favour of review
            // [NOTE] There MAY be a small delay where the player can see the next request before it disappears, can probably be fixed with some rearrangement
            reviewDisplayText.GetComponent<TextMeshPro>().text = reviewSentence;
            reviewDisplayEntire.SetActive(true); // Displaying the review
            flaskBase = 0;
            flaskPot = 0;
            flaskSigil = 0;
        }
    }

    /*[NOTE] The second part of the code that checks for the base is redundant if we keep all potions of the same type
     within the same colour scheme; the same thing applies to the recipeGiver script*/
    void FiveStarReview()
    {
        if (potionName == "LovePotion")
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

        if (potionName == "SoothePotion")
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
        reviewStars.SetFloat("_ProgressBorder", 0);
    }
    void FourStarReview()
    {
        if (potionName == "LovePotion")
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

        if (potionName == "SoothePotion")
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
        reviewStars.SetFloat("_ProgressBorder", -0.033f);
    }
    void TwoStarReview1() // Potion too strong
    {
        if (potionName == "LovePotion")
        {
            switch (flaskPot)
            {
                case 2:
                    reviewSentence = "Okay, I just wanted to flirt and she's way too intense...";
                    break;
                case 3:
                    reviewSentence = "I said I wanted him to love me, not follow me like a dog!";
                    break;
            }
        }

        if (potionName == "SoothePotion" && flaskBase == 9)
        {
            switch (flaskPot)
            {
                case 2:
                    reviewSentence = "Lmao we haven't thought of that yet";
                    break;
                case 3:
                    reviewSentence = "I didn't mean it like that! I only wanted him to sleep! Jesus Christ, who hired you?";
                    break;
            }
        }
        reviewStars.SetFloat("_ProgressBorder", -0.0615f);
    }
    void TwoStarReview2() // Potion too weak
    {
        if (potionName == "LovePotion")
        {
            switch (flaskPot)
            {
                case 0:
                    reviewSentence = "It does not work! It looks like a potion, but it isn't!";
                    break;
                case 1:
                    reviewSentence = "Ugh, she's not much better! I still have to woo her!";
                    break;
                case 2:
                    reviewSentence = "This is not enough passion! Did you not do it because you think you know better?";
                    break;
            }
        }

        if (potionName == "SoothePotion")
        {
            switch (flaskPot)
            {
                case 0:
                    reviewSentence = "It does not work! It looks like a potion, but it isn't!";
                    break;
                case 1:
                    reviewSentence = "He is not quiet, he is just annoying now! Acts weird, too.";
                    break;
                case 2:
                    reviewSentence = "You don't get the slang, do you? Sleeping is not what I meant by 'gone'!";
                    break;
            }
        }
        reviewStars.SetFloat("_ProgressBorder", -0.0615f);
    }
    void IncorrectPotion()
    {
        reviewSentence = "This is not what I ordered at all! This sucks!";
        reviewStars.SetFloat("_ProgressBorder", -1);
    }

    void DeadPotion()
    {
        reviewSentence = "WHAT THE HELL DID YOU DO???";
        reviewStars.SetFloat("_ProgressBorder", -1);
    }

}
