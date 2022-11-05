using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    private int currentPage = 0;

    public GameObject[] pages;
    // Start is called before the first frame update

    private void Start()
    {
        UpdatePage();
    }

    public void FlipPage(bool isRight)
    {
        if (isRight)
        {
            currentPage++;
        }
        else
        {
            currentPage--;
        }
        currentPage = Mathf.Clamp(currentPage, 0, 3);
        UpdatePage();
    }

    private void UpdatePage()
    {
        foreach (GameObject page in pages)
        {
            if (pages[currentPage] == page)
            {
                page.SetActive(true);
            }
            else
            {
                page.SetActive(false);
            }
        }
    }
}
