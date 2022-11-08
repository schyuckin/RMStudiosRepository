using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipPage : MonoBehaviour
{
    public bool flipToRight = true;
    public bool delay = false;

    public Book book;

    private void OnTriggerEnter(Collider other)
    {
        if (!delay)
        {
            delay = true;
            book.FlipPage(flipToRight);
            StartCoroutine(Wait());
        }

    }

    private void OnTriggerStay(Collider other)
    {
        delay = true;
    }

    private void OnTriggerExit(Collider other)
    {
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
        delay = false;

    }
}
