using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handtrackingphysics : MonoBehaviour
{
    private Rigidbody rb;
    private Collider[] handCollider;
    private IEnumerator coroutine;
    public float delayroutine;

    // Start is called before the first frame update
    void Start()
    {
        //Get rigidbody and collider in all children of the gameobject attached
        rb = GetComponent<Rigidbody>();

        handCollider = GetComponentsInChildren<Collider>();

        //Delay of coroutine is set to 0.03f
        coroutine = WaitforCollisionReturn(delayroutine);
    }


    private void OnCollisionEnter(Collision collision)
    {
        //If a gameobject collides to attached gameobject,collision will be disabled
        if (collision.gameObject)
        {
            foreach(var item in handCollider)
            {
                item.enabled = false;
            }
        }
        //If collision is not detected return collision
        else
        {
            StartCoroutine(coroutine);
        }
    }
    public void DisableHandCollider()
    {
        foreach (var item in handCollider)
        {
            item.enabled = true;
        }
    }
    public void EnabelHandColliderDelay(float delay)
    {
        Invoke("EnableHandCollider", delay);
    }
    
    private IEnumerator WaitforCollisionReturn(float delay)
    {
        Invoke("EnableHandCollider", delay);
        yield return WaitforCollisionReturn(delay);
    }
}
