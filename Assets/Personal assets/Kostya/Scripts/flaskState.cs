    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flaskState : MonoBehaviour
{
    public int flaskBase; // De-facto state of the ingredient within
    private int checkBase;
    public int flaskPotency; // Potency of the flask ingredient
    private int checkPot;
    public int sigilType; // Enchantment used on the flask
    private int checkSig;
    [SerializeField] private Material neutralMaterial; // Default material
    public Collider[] possibleSigils = new Collider[3]; // All possible enchantments
    private bool isEnchanted;
    public bool isCorrectOrder;
    public GameObject baseLiquid; // Liquid inside the flask
    public Collider cauldronWater; // Refers to the cauldron
    public Material baseMaterial; // New liquid scooped from the cauldron
    // [SerializeField] public GameObject defaultLocation; // Temporary teleportation of the flask
    // Start is called before the first frame update
    void Start()
    {
        SettingProperties();
    }

    // Update is called once per frame
    void Update()
    {
        // This part is supposed to control the order, I'm not sure
        if ((flaskBase == checkBase && flaskPotency != checkPot) || (flaskBase == checkBase && sigilType != checkSig))
        {
            isCorrectOrder = false;
        }
        if (flaskPotency == checkPot && sigilType != checkSig)
        {
            isCorrectOrder = false;
        }

    }

    // Everything is set to default state
    public void SettingProperties()
    {
        flaskBase = 0;
        flaskPotency = 0;
        sigilType = 0;
        checkBase = flaskBase;
        checkPot = flaskPotency;
        checkSig = sigilType;
        isEnchanted = false;
        isCorrectOrder = true;
        baseLiquid.GetComponent<MeshRenderer>().material = neutralMaterial;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == cauldronWater)
        {
            baseMaterial = cauldronWater.GetComponent<Renderer>().material;
            baseLiquid.GetComponent<Renderer>().material = baseMaterial;
            var chosenBase = cauldronWater.GetComponent<waterState>();
            flaskBase = chosenBase.currentState;
            var chosenPotency = cauldronWater.GetComponent<burnerState>();
            flaskPotency = chosenPotency.burnerPotency;

        }
        // [NOTE] Commented for now since this is being declared in a Sigil Enable script

        //if (other.tag == "potionSigil" && !isEnchanted)
        //{
        //    isEnchanted = true;
        //    if (other == possibleSigils[0])
        //    {
        //        sigilType = 1;
        //    }
        //    if (other == possibleSigils[1])
        //    {
        //        sigilType = 2;
        //    }
        //    if (other == possibleSigils[2])
        //    {
        //        sigilType = 3;
        //    }
        //}
    }
}
