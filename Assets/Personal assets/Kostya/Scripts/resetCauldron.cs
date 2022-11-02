using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetCauldron : MonoBehaviour
{
    public GameObject caulController;
    public GameObject waterColouring;
    public GameObject burner;
    public Material neutralWater;
    [SerializeField] private bool manualActivation = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        if (manualActivation)
        {
            manualActivation = false;
            ResettingCauldron();
        }
    }
    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        ResettingCauldron();

    }

    public void ResettingCauldron()
    {
        // This triggers a function within Cauldron Controller that refreshes all of the ingredients on the list
        caulController.GetComponent<cauldronController>().SettingUp();
        // Manual restoration of water to normal colour
        waterColouring.GetComponent<MeshRenderer>().material = neutralWater;
        waterColouring.GetComponent<burnerState>().SettingBurner();
        // Crap below resets all of cauldron parameters to default
        // At least I think so, some of it is probably redundant but I'm afraid to touch it
        var controllerReset = caulController.GetComponent<cauldronController>();
        controllerReset.ingredientAmount = 0;
        var burnerPot = burner.GetComponent<burnerState>();
        burnerPot.burnerPotency = 0;
        var ingredientChange = waterColouring.GetComponent<waterState>();
        ingredientChange.ingredientControl = 0;
        var colourChange = waterColouring.GetComponent<waterState>();
        colourChange.currentState = 0;
    }
}
