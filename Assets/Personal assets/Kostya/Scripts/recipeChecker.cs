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
    [SerializeField] private string potionName;

    [Space]

    private string reviewSentence = "If you see this, it means that potionmaking went wrong. We apologize.";
    private bool orderDelay = false;
    private float droppingDelay = 3.0f;

    [Space]

    public GameObject reviewDisplayText;
    [SerializeField] private flaskState flaskProp;
    [SerializeField] private moveReview movingReview;
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

                case "Energy":
                    potionName = "EnergyPotion";
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
            movingReview.MovingUpDown();
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
                    reviewSentence = "I was going to write a complaint but I started falling asleep as I was typing it so I guess it worked.";
                    break;
                case 2:
                    reviewSentence = "The potion was perfect! Whenever my neighbor is being loud, I just gift him some tea and enjoy the rest of the day!";
                    break;
                case 3:
                    reviewSentence = "Thanks a lot! One cup of tea was just enough. Like my new neighbour way more, too.";
                    break;
            }
                    
        }

        if (potionName == "EnergyPotion")
        {
            switch (requiredPot)
            {
                case 1:
                    reviewSentence = "They announced my promotion halfway through the presentation... Okay, thanks!";
                    break;
                case 2:
                    reviewSentence = "I WON I WON I WON I WON I WON I WON I WON I WON I WON";
                    break;
                case 3:
                    reviewSentence = "I don't know I ended up in Mexico, but it's cool here. I might need another potion to get back though.";
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
                    reviewSentence = "It worked but it kind of feels like I'm still dreaming.";
                    break;
                case 2:
                    reviewSentence = "My neighbor is asleep, which is good. His snoring, however, causes our entire building to shake.";
                    break;
                case 3:
                    reviewSentence = "Thank you for helping with my neighbour, but doing that to his wife was unneccessary...";
                    break;
            }
        }

        if (potionName == "EnergyPotion")
        {
            switch (requiredPot)
            {
                case 1:
                    reviewSentence = "It was fine until I started speaking Japanese midway. My boss was impressed, but I need to learn the language now.";
                    break;
                case 2:
                    reviewSentence = "When they gave me my victory trophy, I accidentally broke it because my legs could not stop twitching.";
                    break;
                case 3:
                    reviewSentence = "I ran so fast I ended up in North Korea. Luckily, there was still some potion left...";
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
                    switch (requiredPot)
                    {
                        case 1:
                            reviewSentence = "SHE'S KNOCKING ON THE DOOR RIGHT NOW AND SCREAMING SWEETIE PIE SEND SOMETHING TO CALM HER";
                            break;
                        case 2:
                            reviewSentence = "I said I wanted him to love me, not follow me like a dog!";
                            break;
                    }
                    break;
            }
        }

        if (potionName == "SoothePotion")
        {
            switch (flaskPot)
            {
                case 2:
                    reviewSentence = "My son had trouble issues falling asleep and he's not waking up for the third day. If this continues, I will report to the authorities.";
                    break;
                case 3:
                    switch (requiredPot)
                    {
                        case 1:
                            reviewSentence = "My son hasn't woken up in two weeks, what did you give him?";
                            break;
                        case 2:
                            reviewSentence = "I didn't mean it like that! I only wanted him to sleep! Jesus Christ, who hired you?";
                            break;
                    }
                    break;
            }
        }

        if (potionName == "EnergyPotion")
        {
            switch (flaskPot)
            {
                case 2:
                    reviewSentence = "I could not stop screaming so the meeting members had to use earplugs. I apologised, but it was awkward.";
                    break;
                case 3:
                    switch (requiredPot)
                    {
                        case 1:
                            reviewSentence = "They postponed the meeting for two days and told me to seek professional help... Your store has a reputation now.";
                            break;
                        case 2:
                            reviewSentence = "I got disqualified because I ran the entire race two times while they were preparing for it.";
                            break;
                    }
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
                    reviewSentence = "Nobody told me that this thing is active for such a short period of time.";
                    break;
                case 1:
                    switch (requiredPot)
                    {
                        case 2:
                            reviewSentence = "Ugh, she's not much better! I still have to woo her!";
                            break;
                        case 3:
                            reviewSentence = "They said they want to take it slow... Who even says that? I don't have much time!";
                            break;
                    }
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
                    switch (requiredPot)
                    {
                        case 2:
                            reviewSentence = "He is not quiet, he is just annoying now! Acts weird, too.";
                            break;
                        case 3:
                            reviewSentence = "Soooooo.....Does this just work slowly or should I use the second portion orrrrr....?";
                            break;
                    }
                    break;
                case 2:
                    reviewSentence = "It was awkward to see them in the hallway after I sent condolences to their family... Do you really not get the slang?";
                    break;
            }
        }

        if (potionName == "EnergyPotion")
        {
            switch (flaskPot)
            {
                case 0:
                    reviewSentence = "It worked for like five and a half minutes!";
                    break;
                case 1:
                    switch (requiredPot)
                    {
                        case 2:
                            reviewSentence = "Still got beat by a little kid... I gotta know where he gets his potions.";
                            break;
                        case 3:
                            reviewSentence = "Made it out of the city and that's it. Now I also have to pay for the bus back.";
                            break;
                    }
                    break;
                case 2:
                    reviewSentence = "This is only good enough to run through a country as small as Luxembourg xD xD =D";
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
