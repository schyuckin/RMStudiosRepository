using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class materialChange : MonoBehaviour
{
    public GameObject cauldronWater;
    // Temporary script, replaces default stuff in the flask with water
    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Cauldron water")
        {
            Debug.Log("Does this work?");
            this.GetComponent<MeshRenderer>().material = cauldronWater.GetComponent<MeshRenderer>().material;
        }
    }
}
