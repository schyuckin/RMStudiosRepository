using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handtrackingphysics : MonoBehaviour
{
    private Rigidbody rb;
    private Collider[] handCollider;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        handCollider = GetComponentsInChildren<Collider>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (var item in handCollider)
        {
            
            item.enabled = true;
        }
        foreach (var item in handCollider)
        {
            item.enabled = false;
        }
    }

    public void EnableHandCollider()
    {
        foreach (var item in handCollider)
        {
            item.enabled = true;
        }
    }
    public void DisableHandCollider()
    {
        foreach (var item in handCollider)
        {
            item.enabled = false;
        }
    }

    public void EnabelHandColliderDelay(float delay)
    {
        Invoke("EnableHandCollider", delay);
    }
}
