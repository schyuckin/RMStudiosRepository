using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cauldronController : MonoBehaviour
{

    public string[] baseIngredients = new string[10]; // List of objects in the cauldron, up to 10 things
    public string recentChemical = "None"; // Latest thing added to the cauldron
    public int ingredientAmount = 0; // Amount of the objects currently in the cauldron
    private int arrayControl = 0; // Internal integer used to control the updates
    public GameObject waterBody;
    [SerializeField] private GameObject[] Chemical = new GameObject[3];
    [SerializeField] private GameObject[] TeleportLocation = new GameObject[3];

    // Start is called before the first frame update
    void Start()
    {
        SettingUp();
    }

    private void OnDisable() // Resets the ingredients when the side button is pressed
    {
        SettingUp();

    }

    void SettingUp() // Default ingredient setting
    {
        // This array control thing was ANNOYING to find
        // Apparently it messed up the entire reset process
        arrayControl = 0;
        baseIngredients[0] = "None";
        baseIngredients[1] = "None";
        for (var k = 2; k < 10; k++)
        {
            // From this point on water becomes totally black, placeholder for an explosion Easter Egg
            baseIngredients[k] = "Nothing Should Be Here";
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag != "baseElement" && other.tag != "Player" && other.tag != "flask")
        {
            ingredientAmount++;
            waterBody.GetComponent<waterState>().enabled = true;
            var chemicals = waterBody.GetComponent<waterState>();
            chemicals.ingredientControl = 3;
        }
        // Essentially means that all of the other crap wont matter if it falls into the cauldron
        // Mostly done to prevent collision with the player, also has some constraints in terms of current code
        if (other.tag == "baseElement")
        {
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
        // Can probably be replaced with a case-switch (ask Kor)
        // This is very very very very very VERY BAD code
        switch (other.name)
        {
            case "Red stuff":
                recentChemical = "Red";
                Chemical[0].transform.position = TeleportLocation[0].transform.position;
                break;
            case "Yellow stuff":
                recentChemical = "Yellow";
                Chemical[1].transform.position = TeleportLocation[1].transform.position;
                break;
            case "Blue stuff":
                recentChemical = "Blue";
                Chemical[2].transform.position = TeleportLocation[2].transform.position;
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
