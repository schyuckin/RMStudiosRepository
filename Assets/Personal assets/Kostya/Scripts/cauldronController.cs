using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cauldronController : MonoBehaviour
{
    [SerializeField] private GameObject waterBody; // Water in the cauldron
    [SerializeField]
    private string[] States = {"Neutral", "Red", "Blue", "Yellow", "Purple", "Orange", "Green", "Black"}; // All possible states of the base
    public Material[] baseColours = new Material[8]; // Water materials
    [SerializeField] private Transform[] originPositions = new Transform[3]; // Original placements of the chemicals
    private bool[] coloursInside = new bool[3]; // Is red, blue or yellow in the cauldron

    [Space] // Current state control

    [SerializeField] private flaskState flaskPassing;
    private bool dead = false;
    [SerializeField] private int ingredientAmount = 0; // Amount of the objects currently in the cauldron
    public int cauldronPotency = 0; // Potency of the cauldron
    public int cauldronSigil;
    [SerializeField] private GameObject cauldronPotencyDisplay; // Placeholder for the display of current potency turned into intensity

    [Space] // Name and state

    [SerializeField] private string currentStateName = null; // Mostly for inspector convenience
    public int currentState = 0; // Current state of the base

    [Space] // Delay variables

    [SerializeField] private bool ingredientDelay = false;
    [SerializeField] private float timeDelay = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        SettingUp();
    }

    public void SettingUp() // Default ingredient setting
    {
        ingredientAmount = 0;
        currentState = 0;
        cauldronPotency = 0;
        currentStateName = null;
        waterBody.GetComponent<MeshRenderer>().material = baseColours[currentState];
    }

    private void DropDelay()
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

    void Update()
    {
        DropDelay();
    }

    private void ColourUpdate() // [NOTE] THE ORDER IS RED, BLUE, YELLOW
    {

        if ((!coloursInside[0]) && (!coloursInside[1]) && (!coloursInside[2])) // Neutral
        {
            currentState = 0;
        }

        if ((coloursInside[0]) && (!coloursInside[1]) && (!coloursInside[2])) // Red
        {
            currentState = 1;
        }

        if ((!coloursInside[0]) && (coloursInside[1]) && (!coloursInside[2])) // Blue
        {
            currentState = 2;
        }

        if ((!coloursInside[0]) && (!coloursInside[1]) && (coloursInside[2])) // Yellow
        {
            currentState = 3;
        }

        if ((coloursInside[0]) && (coloursInside[1]) && (!coloursInside[2])) // Purple
        {
            currentState = 4;
        }

        if ((coloursInside[0]) && (!coloursInside[1]) && (coloursInside[2])) // Orange
        {
            currentState = 5;
        }

        if ((!coloursInside[0]) && (coloursInside[1]) && (coloursInside[2])) // Green
        {
            currentState = 6;
        }

        if ((coloursInside[0]) && (coloursInside[1]) && (coloursInside[2])) // Black (potion)
        {
            currentState = 7;
            dead = true;
        }
        currentStateName = States[currentState];
        waterBody.GetComponent<MeshRenderer>().material = baseColours[currentState];
    }

    private void PotencyUpdate()
    {
        if (ingredientAmount >=1 && ingredientAmount <= 5) // Weak potion
        {
            cauldronPotency = 1;
        }
        if (ingredientAmount >= 6 && ingredientAmount <= 13) // Mild potion
        {
            cauldronPotency = 2;
        }
        if (ingredientAmount >= 14 && ingredientAmount <= 19) // Strong potion
        {
            cauldronPotency = 3;
        }

        if (ingredientAmount >= 20) // Black potion
        {
            cauldronPotency = 4;
            dead = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "baseElement" && !ingredientDelay)
        {
            switch (other.name)
            {
                case "Red stuff":
                    coloursInside[0] = true;
                    other.transform.position = originPositions[0].transform.position;
                    break;
                case "Blue stuff":
                    coloursInside[1] = true;
                    other.transform.position = originPositions[1].transform.position;
                    break;
                case "Yellow stuff":
                    coloursInside[2] = true;
                    other.transform.position = originPositions[2].transform.position;
                    break;
            }
            ingredientDelay = true;
            ingredientAmount++;
            PotencyUpdate();
            ColourUpdate();
        }
        if (other.tag == "flask")
        {
            if (dead)
            {
                flaskPassing.deadPotion = true;
            }
            flaskPassing.RetrievingInfo();
        }
    }
}
