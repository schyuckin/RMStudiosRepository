using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class hideReview : MonoBehaviour
{
    public GameObject reviewPopUp;
    public GameObject currentRequest;
    [SerializeField] private bool manualActivation = false;
    private void OnTriggerEnter(Collider other)
    {
        ReviewToRequest();
    }
    private void ReviewToRequest()
    {
        currentRequest.SetActive(true);
        reviewPopUp.SetActive(false);
    }

    private void Update()
    {
        if (manualActivation)
        {
            manualActivation = false;
            ReviewToRequest();
        }
    }
}