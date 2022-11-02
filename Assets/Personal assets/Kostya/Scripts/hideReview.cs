using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class hideReview : MonoBehaviour
{
    public GameObject reviewPopUp;
    public GameObject currentRequest;
    private void OnTriggerEnter(Collider other)
    {
        currentRequest.GetComponent<TextMeshPro>().enabled = true;
        reviewPopUp.SetActive(false);
    }
}