using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport_Object : MonoBehaviour
{
    // Tags to use
    // Ingredient1 Ingredient2 Ingredient3 PotionBottle

    // the origin of the object. use an empty pivot for each origin
    [SerializeField] private List<Transform> objectOrigins = new List<Transform>();
    //This layer will teleport the object to the origin

    private void OnTriggerEnter(Collider other)
    {
        switch (other.name)
        {
            case "Red stuff":
                other.transform.position = objectOrigins[0].transform.position;
                break;
            case "Yellow stuff":
                other.transform.position = objectOrigins[1].transform.position;
                break;
            case "Blue stuff":
                other.transform.position = objectOrigins[2].transform.position;
                break;
            case "Potion Flask":
                other.transform.position = objectOrigins[3].transform.position;
                break;
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
