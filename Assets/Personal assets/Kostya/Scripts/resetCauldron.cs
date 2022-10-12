using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetCauldron : MonoBehaviour
{
    public GameObject resettingCauldron;
    public GameObject waterColouring;
    public GameObject burner;
    public Material neutralWater;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
            // This triggers a function within Cauldron Controller that refreshes all of the ingredients on the list
            resettingCauldron.SetActive(false);
            resettingCauldron.SetActive(true);
            // Manual restoration of water to normal colour
            waterColouring.GetComponent<MeshRenderer>().material = neutralWater;
            // Crap below resets all of cauldron parameters to default
            // At least I think so, some of it is probably redundant but I'm afraid to touch it
            var controllerReset = resettingCauldron.GetComponent<cauldronController>();
            controllerReset.ingredientAmount = 0;
            var burnerPot = burner.GetComponent<burnerState>();
            burnerPot.burnerPotency = 0;
            var isPotent = burner.GetComponent<burnerState>();
            isPotent.potencySet = false;
            var ingredientChange = waterColouring.GetComponent<waterState>();
            ingredientChange.ingredientControl = 0;
            var colourChange = waterColouring.GetComponent<waterState>();
            colourChange.currentState = 0;
    }
}
