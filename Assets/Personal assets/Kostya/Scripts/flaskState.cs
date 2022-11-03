    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flaskState : MonoBehaviour
{
    public int flaskBase; // De-facto state of the ingredient within
    public int flaskPotency; // Potency of the flask ingredient
    public int sigilType; // Enchantment used on the flask
    [SerializeField] private Material neutralMaterial; // Default material
    public GameObject baseLiquid; // Liquid inside the flask
    [SerializeField] private GameObject cauldronWater; // Refers to the cauldron
    [SerializeField] private cauldronController cauldronControl; // Fetching variables from the script
    [SerializeField] private Material baseMaterial; // New liquid scooped from the cauldron
    public bool deadPotion = false;
    void Start()
    {
        SettingProperties();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Everything is set to default state
    public void SettingProperties()
    {
        flaskBase = 0;
        flaskPotency = 0;
        sigilType = 0;
        baseLiquid.GetComponent<MeshRenderer>().material = neutralMaterial;
    }

    public void RetrievingInfo()
    {
        flaskBase = cauldronControl.currentState;
        flaskPotency = cauldronControl.cauldronPotency;
        sigilType = cauldronControl.cauldronSigil;
        baseMaterial = cauldronControl.baseColours[flaskBase];
        baseLiquid.GetComponent<Renderer>().material = baseMaterial;

    }
}
