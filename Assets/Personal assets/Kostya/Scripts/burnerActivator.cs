using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class burnerActivator : MonoBehaviour
{
    public GameObject waterHeating;
    [SerializeField] private bool manualActivation = false;
    // Start is called before the first frame update

    private void ActivatingCauldron()
    {
        var activate = waterHeating.GetComponent<burnerState>();
        if (!activate.isActivated)
        {
            activate.isActivated = true;
        }
        else
        {
            activate.isActivated = false;
        }
    }

    private void Update()
    {
        if (manualActivation)
        {
            manualActivation = false;
            ActivatingCauldron();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        ActivatingCauldron();

    }
}
