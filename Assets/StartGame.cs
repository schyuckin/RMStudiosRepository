using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //use Scene Management features
public class StartGame : MonoBehaviour
{
    //assigns scene name to load
    private string sceneName = "Handtrackingtest";

    void Start()
    {

    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) //detects collision with box collider and hand
    {
        
        if (other.tag == "StartGame") //checks for StartGame tag on start button
        {
            SceneManager.LoadScene (sceneName); //loads scene
        }
        else
            print("Failed to Load");
    }
}
