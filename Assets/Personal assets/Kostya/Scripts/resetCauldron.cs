using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetCauldron : MonoBehaviour
{
    public cauldronController caulControl;
    public SigilEnable[] caulEffects = new SigilEnable[2];
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
        if ((other.tag != "flask") || (other.tag != "baseElement"))
        {
            ResettingCauldron();
        }

    }

    public void ResettingCauldron()
    {
        caulControl.SettingUp();
        for (var i = 0; i < 2; i++)
        {
            caulEffects[i].CauldronEffects();
        }
    }
}
