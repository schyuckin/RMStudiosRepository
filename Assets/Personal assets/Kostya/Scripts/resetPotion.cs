using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetPotion : MonoBehaviour
{
    [SerializeField] private GameObject flask;
    [SerializeField] private GameObject teleporter;
    [SerializeField] private bool manualActivation = false;
    // Start is called before the first frame update

    void ResettingFlask()
    {
        flask.transform.position = teleporter.transform.position;
        flask.GetComponent<flaskState>().SettingProperties();
    }

    private void Update()
    {
     if (manualActivation)
        {
            manualActivation = false;
            ResettingFlask();
        }
    }
    // Resets the state of the flask & teleports it to its original position
    private void OnTriggerEnter (Collider other)
    {
        if (other.tag != "flask")
        {
            ResettingFlask();
        }

    }
}
