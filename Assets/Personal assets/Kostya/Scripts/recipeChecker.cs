using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class recipeChecker : MonoBehaviour
{

    [SerializeField] private GameObject flask;
    [SerializeField] private GameObject teleporter;
    [SerializeField] public Material reviewStars;

    private int requiredBase;
    private int requiredPot; // Desired properties for the currently chosen potion recipe
    private int requiredSigil;

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
    [SerializeField] private recipeGiver recipeInfo;

    [SerializeField] List<Material> customerPictures = new List<Material>();

    [Space]

    [SerializeField] private TextMeshPro currentCustomerName;
    [SerializeField] private GameObject currentCustomerPicture;

    [Space] private AudioSource _audioSource;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
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
        requiredBase = recipeInfo.potionBase;
        requiredPot = recipeInfo.potionPotency;
        requiredSigil = recipeInfo.potionSigil;
    }

    // Acquires actual potion properties when the flask gets submitted
    void DeFactoProperties()
    {
        var getActual = flask.GetComponent<flaskState>();
        flaskBase = getActual.flaskBase;
        flaskPot = getActual.flaskPotency;
        flaskSigil = getActual.sigilType;
    }

    private void HandingInFlask()
    {
        orderDelay = true;
        DeFactoProperties();
        var curRequest = this.GetComponent<recipeGiver>();
        currentCustomerName.text = recipeInfo.customerNames[recipeInfo.descriptionChosen];
        currentCustomerPicture.GetComponent<MeshRenderer>().material = customerPictures[recipeInfo.descriptionChosen];
        _audioSource.Play();
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

        this.GetComponent<recipeGiver>().ChoosingRecipe();
        flask.transform.position = teleporter.transform.position;
       // this.GetComponent<recipeGiver>().disablingRequest(); // Hiding the request in favour of review
        reviewDisplayText.GetComponent<TextMeshPro>().text = reviewSentence;
        flask.GetComponent<flaskState>().SettingProperties();
        resettingCauldron.SettingUp();
        movingReview.MovingUpDown();
        flaskBase = 0;
        flaskPot = 0;
        flaskSigil = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == flask.GetComponent<Collider>() && !orderDelay)
        {
            HandingInFlask();
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
                    reviewSentence = "The potion was perfect! Whenever my neighbor is being loud, I just gift him some tea and enjoy the rest of my day!";
                    break;
                case 3:
                    reviewSentence = "It's astonishing how one cup of tea is the difference between life... and coma. Life is ever so fragile...";
                    break;
            }
                    
        }

        if (potionName == "EnergyPotion")
        {
            switch (requiredPot)
            {
                case 1:
                    reviewSentence = "They announced my promotion halfway through the presentation... You are great!";
                    break;
                case 2:
                    reviewSentence = "I WON I WON I WON I WON I WON I WON I WON I WON I WON";
                    break;
                case 3:
                    reviewSentence = "I don't know I ended up in Mexico but at least you-know-who is not watching me anymore... Or are they?";
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
                    reviewSentence = "I guess it worked? He has fallen for me, but his head caught on fire which is ... thats not what I meant by a 'spark'";
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
                    reviewSentence = "I am grateful for aiding with my neighbour, but I can still see his shadow wherever I go...";
                    break;
            }
        }

        if (potionName == "EnergyPotion")
        {
            switch (requiredPot)
            {
                case 1:
                    reviewSentence = "It was fine until I started speaking Japanese midway. My boss was impressed, but I have to learn the language now.";
                    break;
                case 2:
                    reviewSentence = "WhEeen theyY gav me MY victory TropPhy I broke ITtt becauseEE my bbBody Keeps TWitching";
                    break;
                case 3:
                    reviewSentence = "I ran so fast I ended up in North Korea. Maybe Uncle Sam is not so bad after all. Thank goodness there was still some potion left...";
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
                    reviewSentence = "My son had trouble issues falling asleep and he's not waking up for the third day after your potion. If this continues, I will report to the authorities.";
                    break;
                case 3:
                    switch (requiredPot)
                    {
                        case 1:
                            reviewSentence = "My son hasn't woken up in two weeks, what did you give him?";
                            break;
                        case 2:
                            reviewSentence = "Oh golly! I only wanted him to sleep! Sweet Zeus, who hired you?";
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
                    reviewSentence = "I could not stop SCREAMING so the meeting members had to use EARPLUGS. I apologised, but it was REALLY awkward.";
                    break;
                case 3:
                    switch (requiredPot)
                    {
                        case 1:
                            reviewSentence = "They postponed the meeting for two days and told me to seek PROFESSIONAL HELP!!! Your store has a reputation now. AND ME TOO!!";
                            break;
                        case 2:
                            reviewSentence = "i gt disqualfiied because i ran th enntire race two times whole they were preparriing forr it";
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
                            reviewSentence = "Ugh, he's not much better! I still have to woo him!";
                            break;
                        case 3:
                            reviewSentence = "He said he wanted to take it slow!! Do I LOOK LIKE I'm interested in that?";
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
                            reviewSentence = "He is not quiet, he is just annoying now! He acts weird as well...";
                            break;
                        case 3:
                            reviewSentence = "Life has once again subjected me to the curse of suffering... The potion is weak, but I have to stay strong...";
                            break;
                    }
                    break;
                case 2:
                    reviewSentence = "At this point I may be the one closer to death than he is... It seems that I was, once again, gravely misunderstood.";
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
                            reviewSentence = "Still got beat by a little kid... I gotta know where he gets his potions";
                            break;
                        case 3:
                            reviewSentence = "Made it out of the city and that's it. Now I also have to find something to get me back.";
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
        switch (recipeInfo.customerNames[recipeInfo.descriptionChosen])
        {
            case "Vincent Edgecombe":
                reviewSentence = "I can cook something like that myself, too!";
                break;
            case "Gellert Friedrich":
                reviewSentence = "Never ordering from here again lol";
                break;
            case "Sybil Silvertooth":
                reviewSentence = "I never needed a man anyway. This still sucks though!";
                break;
            case "w1lhelmGBS":
                reviewSentence = "i hope there is a mosquito who comes into your room at 3 AM and flies around your head";
                break;
            case "Granny Weatherwax":
                reviewSentence = "Back in my days we knew how to cook potions when we were two. Disappointing.";
                break;
            case "_AmorphicDesolation_":
                reviewSentence = "Just like with everything else in my life, I am left with immeasurable disappointment. This world is a cruel and lonely place and we all die alone";
                break;
            case "Korina Swivel":
                reviewSentence = "I will never trust a potion shop with something so important again.";
                break;
            case "every1wandsme":
                reviewSentence = "I don't even need your potions!!!!! I'm a winner at HEART";
                break;
            case "Abery Crabaham":
                reviewSentence = "So they were watching me through you... You cannot stop me anyway....";
                break;
        }
        reviewStars.SetFloat("_ProgressBorder", -1);
    }

    void DeadPotion()
    {
        switch (recipeInfo.customerNames[recipeInfo.descriptionChosen])
        {
            case "Vincent Edgecombe":
                reviewSentence = "I will report you to the magic authorities. You filthy murderer!";
                break;
            case "Gellert Friedrich":
                reviewSentence = "Thank god he was not in the office that day... You absolute monster";
                break;
            case "Sybil Silvertooth":
                reviewSentence = "I never needed a man anyway. This still sucks though!";
                break;
            case "w1lhelmGBS":
                reviewSentence = "Hello, this is Wilhelm's mom. Do you know when he will wake up?";
                break;
            case "Granny Weatherwax":
                reviewSentence = "oh no granny died.........anyways inheritance money is cool (c) Uma & Truman, her grandchildren";
                break;
            case "_AmorphicDesolation_":
                reviewSentence = "Everything around me has died, but I feel nothing at this loss of meaningful life.";
                break;
            case "Korina Swivel":
                reviewSentence = "I have to find a new job because everyone is @&*@@#(*@( DEAD!!!!!!!!!!!";
                break;
            case "every1wandsme":
                reviewSentence = "Well, that's one way to take care of the competition. There are no more organizers either tho";
                break;
            case "Abery Crabaham":
                reviewSentence = "They wanna prosecute me, an innocent man, for murder! Did you snitch??!!";
                break;
        }
        reviewStars.SetFloat("_ProgressBorder", -1);
    }

}
