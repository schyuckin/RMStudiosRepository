using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class TurnPage : MonoBehaviour
{
    [FormerlySerializedAs("turnToRight")] public bool flipToRight = true;

    public Book book;
    private void OnTriggerEnter(Collider other)
    {
        book.FlipPage(flipToRight);
    }
}
