using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class burnerState : MonoBehaviour
{
    public bool isActivated = false; // Is burner turned on
    public float burnerTemperature = 20.0f; // Current temperature of the burner
    [SerializeField] private int burnerTemp = 20; // Converted to float
    [SerializeField] private GameObject burnerDisplay;
    [SerializeField] private Material[] burnerMoods = new Material[3]; // Just a fancier name to describe the visuals I suppose
    public GameObject potionFlask;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Renderer>().material = burnerMoods[0];
    }

    // Update is called once per frame
    void Update()
    {
        // Heating up
        if (isActivated)
        {
            this.GetComponent<Renderer>().material = burnerMoods[1];
            if (burnerTemperature < 100)
            {
                burnerTemperature += Time.deltaTime * 5;
                burnerTemp = (int)burnerTemperature;
            }
        }
        // Cooling down after a small delay
        if (!isActivated)
        {
            StartCoroutine(CoolingDown());
        }
        // Displays the current temperature outside
        string burnerTempString = burnerTemp.ToString();
        burnerDisplay.GetComponent<TextMeshPro>().text = burnerTempString;
    }
    IEnumerator CoolingDown()
    {
        yield return new WaitForSeconds(1.5f);
        if (burnerTemperature > 20)
        {
            this.GetComponent<Renderer>().material = burnerMoods[2];
            burnerTemperature -= Time.deltaTime;
        }
        // Returns the temperature to default to avoid any problems
        if (burnerTemperature < 20)
        {
            this.GetComponent<Renderer>().material = burnerMoods[0];
            burnerTemperature = 20;
        }
        burnerTemp = (int)burnerTemperature;
    }
    private void OnTriggerStay(Collider other)
    {
        // Heats up the flask
        if (isActivated)
        {
            var givenTemperature = potionFlask.GetComponent<flaskState>();
            // Prevents the flask from dropping its own temperature whenever the heater starts to cool down
            if (givenTemperature.flaskTemperature <= burnerTemp)
            {
                givenTemperature.flaskTemperature = burnerTemp;
            }
        }
    }
}
