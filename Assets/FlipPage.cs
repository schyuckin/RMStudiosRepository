using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipPage : MonoBehaviour
{
    public bool flipToRight = true;

    public Book book;

    private void OnTriggerEnter(Collider other)
    {
        book.FlipPage(flipToRight);
    }
}
