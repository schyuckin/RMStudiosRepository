using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetPotion : MonoBehaviour
{
    [SerializeField] private GameObject flask;
    [SerializeField] private GameObject teleporter;
    // Start is called before the first frame update

    // Resets the state of the flask & teleports it to its original position
    private void OnTriggerEnter (Collider other)
    {
        flask.transform.position = teleporter.transform.position;
        flask.GetComponent<flaskState>().SettingProperties();
    }
}
