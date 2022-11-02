using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetCauldron : MonoBehaviour
{
    public cauldronController caulControl;
    [SerializeField] private bool manualActivation = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        if (manualActivation)
        {
            manualActivation = false;
            ResettingCauldron();
        }
    }
    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "flask")
        {
            ResettingCauldron();
        }

    }

    public void ResettingCauldron()
    {
        caulControl.SettingUp();
    }
}
