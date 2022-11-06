using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hoveringObject : MonoBehaviour
{
    [SerializeField] private float amplitude;
    [SerializeField] private float frequency;

   private Vector3 posOffset = new Vector3();
   private Vector3 tempPos = new Vector3();

    // Use this for initialization
    void Start()
    {
        posOffset = transform.position;
    }

    void Update()
    {
        tempPos = posOffset;
        tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;
        transform.position = tempPos;
    }
}
