using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class burnerActivator : MonoBehaviour
{
    public GameObject chemicalBurner;
    // Start is called before the first frame update
    private void OnMouseDown()
    {
        var activate = chemicalBurner.GetComponent<burnerState>();
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
