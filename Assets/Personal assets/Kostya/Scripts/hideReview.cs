using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class hideReview : MonoBehaviour
{
    [SerializeField] private GameObject reviewPopUp;
    [SerializeField] private GameObject currentRequest;
    private void OnTriggerEnter(Collider other)
    {
        reviewPopUp.GetComponent<TextMeshPro>().enabled = false;
        currentRequest.GetComponent<TextMeshPro>().enabled = true;
    }
}