using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetPotion : MonoBehaviour
{
    [SerializeField] private GameObject flask;
    [SerializeField] private GameObject teleporter;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        // Bad code, duplicate of what I already had in the recipe generator
        // I think there is a better way to do this by having this script be triggered from the recipe generator
        // And do not have it in its entire form in the generator itself
        flask.transform.position = teleporter.transform.position;
        var reset = flask.GetComponent<flaskState>();
        reset.isReset = true;
    }
}
