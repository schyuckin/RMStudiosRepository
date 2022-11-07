using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class moveReview : MonoBehaviour
{
    public GameObject reviewPopUp;
    public GameObject currentRequest;

    [Space]

    [SerializeField] private float inactiveHeight = 1f;
    [SerializeField] private float raisingDuration = 1.0f;
    [SerializeField] private bool isActive;
    private Vector3 inactivePosition;

    [Space]

    [SerializeField] private bool manualActivation = false;
    [SerializeField] private bool touchDelay = false;
    [SerializeField] private float timeDelay = 1.0f;

    private void Start()
    {
        inactivePosition = transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (((other.tag != "flask") || (other.tag != "baseElement")) && (!touchDelay))
        {
            touchDelay = true;
            currentRequest.SetActive(true);
            MovingUpDown();
        }
    }
    public void MovingUpDown()
    {
        StopAllCoroutines();
        if (!isActive)
        {
            Vector3 activePosition = inactivePosition - Vector3.up * inactiveHeight;
            StartCoroutine(MovingReview(activePosition));
        }
        else
        {
            StartCoroutine(MovingReview(inactivePosition));
        }
        isActive = !isActive;
    }

    IEnumerator MovingReview(Vector3 targetPosition)
    {
        float timeElapsed = 0;
        Vector3 startPosition = transform.position;
        while (timeElapsed < raisingDuration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, timeElapsed / raisingDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = targetPosition;
    }
    public void Update()
    {
        if (touchDelay)
        {
            timeDelay -= Time.deltaTime;
            if (timeDelay < 0)
            {
                timeDelay = 0;
                touchDelay = false;
            }
        }

        if (manualActivation)
        {
            touchDelay = true;
            currentRequest.SetActive(true);
            manualActivation = false;
            MovingUpDown();
        }
    }
}