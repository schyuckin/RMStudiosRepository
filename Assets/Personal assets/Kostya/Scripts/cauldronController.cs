using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cauldronController : MonoBehaviour
{

    public string[] baseIngredients = new string[10]; // List of objects in the cauldron, up to 10 things
    public string recentChemical = "None"; // Latest thing added to the cauldron
    public int ingredientAmount = 0; // Amount of the objects currently in the cauldron
    private int arrayControl = 0; // Internal integer used to control the updates
    [SerializeField] private bool ingredientDelay = false;
    [SerializeField] private float timeDelay = 0.5f;
    public GameObject waterBody;
    [SerializeField] private GameObject[] Chemical = new GameObject[3];
    [SerializeField] private GameObject[] TeleportLocation = new GameObject[3];

    // Start is called before the first frame update
    void Start()
    {
        SettingUp();
    }

    public void SettingUp() // Default ingredient setting
    {
        arrayControl = 0;
        baseIngredients[0] = "None";
        baseIngredients[1] = "None";
        for (var k = 2; k < 10; k++)
        {
            baseIngredients[k] = "Nothing Should Be Here";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (timeDelay > 0)
        {
            if (ingredientDelay)
            {
                timeDelay -= Time.deltaTime;
                this.GetComponent<BoxCollider>().enabled = false;
            }
        }
        else
        {
            timeDelay = 0;
            ingredientDelay = false;
            this.GetComponent<BoxCollider>().enabled = true;
            timeDelay = 0.5f;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // COMMENTED THIS TO FIX THAT WEIRD BUG WHERE ANYTHING BESIDES BASE ELEMENT WOULD CAUSE THE CAULDRON TO TURN BLACK
        // THE CODE STILL NEEDS THE OPTIONAL CONDITIONS TO MAKE SURE THAT THE PLAYER CANNOT JUST DROP STUFF IN THE CAULDRON


        //if (other.tag != "baseElement" && other.tag != "Player" && other.tag != "flask" && other.tag != "handElement")
        //{
        //    if (other.tag != "handElement")
        //    {
        //        ingredientAmount++;
        //    }
        //    waterBody.GetComponent<waterState>().enabled = true;
        //    var chemicals = waterBody.GetComponent<waterState>();
        //    chemicals.ingredientControl = 3;
        //}
        // Essentially means that all of the other crap wont matter if it falls into the cauldron
        // Mostly done to prevent collision with the player, also has some constraints in terms of current code
        if (other.tag == "baseElement" && !ingredientDelay)
        {
            ingredientDelay = true;
            // Lets the water body know how many chemicals are in the water
            ingredientAmount++;
            waterBody.GetComponent<waterState>().enabled = true;
            var chemicals = waterBody.GetComponent<waterState>();
            if (ingredientAmount < 4)
            {
                chemicals.ingredientControl = ingredientAmount;
            }
        }
        Debug.Log("The amount of chemicals is " + ingredientAmount);
        // Starts checking for what is the colour of the chemical
        // This is very very very very very VERY BAD code
        switch (other.name)
        {
            case "Red stuff":
                recentChemical = "Red";
                other.transform.position = TeleportLocation[0].transform.position;
                break;
            case "Yellow stuff":
                recentChemical = "Yellow";
                other.transform.position = TeleportLocation[1].transform.position;
                break;
            case "Blue stuff":
                recentChemical = "Blue";
                other.transform.position = TeleportLocation[2].transform.position;
                break;
        }
        while (arrayControl < ingredientAmount)
        {
            baseIngredients[ingredientAmount - 1] = recentChemical;
            arrayControl++;
        }
        // This is where it checks all of the current chemicals
    }
}
