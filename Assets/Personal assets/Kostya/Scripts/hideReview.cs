using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class hideReview : MonoBehaviour
{
    [SerializeField] private GameObject reviewPopUp;
    private void OnTriggerEnter(Collider other)
    {
        reviewPopUp.GetComponent<TextMeshPro>().enabled = false;
    }
}