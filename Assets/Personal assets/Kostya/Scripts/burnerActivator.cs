using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class burnerActivator : MonoBehaviour
{
    public GameObject waterHeating;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
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
}
