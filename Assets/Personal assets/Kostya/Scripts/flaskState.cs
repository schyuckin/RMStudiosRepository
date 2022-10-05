using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flaskState : MonoBehaviour
{
    public int flaskBase; // De-facto state of the ingredient within
    private int checkBase;
    public int flaskTemperature; // Temperature of the flask after using burner
    private int checkTemp;
    public int enchantmentType; // Enchantment used on the flask
    private int checkEnch;
    [SerializeField] private Material neutralMaterial; // Default material
    public Collider[] possibleEnchantments = new Collider[3]; // All possible enchantments
    private bool isEnchanted;
    public bool isReset;
    public bool isCorrectOrder;
    public GameObject baseLiquid; // Liquid inside the flask
    public Collider cauldronWater; // Refers to the cauldron
    public Material baseMaterial; // New liquid scooped from the cauldron
    [SerializeField] private GameObject wrongOrderUI; // 
    [SerializeField] private GameObject wrongMaterialsUI; // 
    // [SerializeField] public GameObject defaultLocation; // Temporary teleportation of the flask
    // Start is called before the first frame update
    void Start()
    {
        SettingProperties();
    }

    // Update is called once per frame
    void Update()
    {
        if (isReset)
        {
            SettingProperties();
        }
        if ((flaskBase == checkBase && flaskTemperature != checkTemp) || (flaskBase == checkBase && enchantmentType != checkEnch))
        {
            ErrorMessage();
        }
        if (flaskTemperature == checkTemp && enchantmentType != checkEnch)
        {
            ErrorMessage();
        }

    }

    private void SettingProperties()
    {
        wrongOrderUI.SetActive(false);
        wrongMaterialsUI.SetActive(false);
        isReset = false;
        flaskBase = 0;
        flaskTemperature = 19;
        enchantmentType = 0;
        checkBase = flaskBase;
        checkTemp = flaskTemperature;
        checkEnch = enchantmentType;
        isEnchanted = false;
        isCorrectOrder = true;
        baseLiquid.GetComponent<MeshRenderer>().material = neutralMaterial;
    }

    private void ErrorMessage()
    {
        isCorrectOrder = false;
        wrongOrderUI.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Dropping the flask into the cauldron to change the base
        if (other == cauldronWater)
        {
            baseMaterial = cauldronWater.GetComponent<Renderer>().material;
            // Commented so that the water changes but the flask does not teleport God knows where
            // transform.position = defaultLocation.transform.position;
            baseLiquid.GetComponent<Renderer>().material = baseMaterial;
            var chosenBase = cauldronWater.GetComponent<waterState>();
            flaskBase = chosenBase.currentState;

        }
        if (other.tag == "enchantmentElement" && !isEnchanted)
        {
            isEnchanted = true;
            if (other == possibleEnchantments[0])
            {
                enchantmentType = 1;
            }
            if (other == possibleEnchantments[1])
            {
                enchantmentType = 2;
            }
            if (other == possibleEnchantments[2])
            {
                enchantmentType = 3;
            }
        }
    }
}
