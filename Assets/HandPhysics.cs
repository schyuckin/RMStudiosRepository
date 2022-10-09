using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandPhysics : MonoBehaviour
{
    public Transform target;
    private Rigidbody rb;
    public Renderer nonPhysicalhand;
    public float shownonPhysicalhandDistance = 0.05f;

    private Collider[] handCollider;

    void Start()
    {
        //get the rigidbody of attached object
        rb = GetComponent<Rigidbody>();

        //Handcollider is all the colliders in the children of the hand
        handCollider = GetComponentsInChildren<Collider>();
            
            }

    public void EnableHandCollider()
    {
        //If an item is out of the hand, enable collider
        foreach(var item in handCollider)
        {
            item.enabled = true;
        }
    }

    public void DisableHandCollider()
    {
        //if an item is in the hand collider, disable collider
        foreach (var item in handCollider)
        {
            item.enabled = false;
        }
    }

    public void EnabelHandColliderDelay(float delay)
    {
        //Delay between enabling the collider to prevent pushing
        //The delay time can be regulated in the inspector
        Invoke("EnableHandCollider", delay);
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        
        if(distance > shownonPhysicalhandDistance)
        {
            nonPhysicalhand.enabled = true;
        }
        else
        {
            nonPhysicalhand.enabled = false;
        }
    }

    void FixedUpdate()
    {
        // hand position
        //Get velocity of rigidbody by dividing the position by the Time.deltatime
        rb.velocity = (target.position - transform.position) / Time.fixedDeltaTime;

        rb.rotation = target.rotation;

        //hand rotation
        //Get rotation of the hands by targeting the rotating of the object and getting the difference from its position
        //  Quaternion postRotation = transform.rotation * Quaternion.Euler(0, 0, -90);
        // Quaternion rotationDifference = target.rotation * Quaternion.Inverse(postRotation);
        //  rotationDifference.ToAngleAxis(out float angleInDegree, out Vector3 rotationAxis);

        // Vector3 rotationDifferenceInDegree = angleInDegree * rotationAxis;

        //Get velocity of rotationdifference by dividing it by Time.deltatime
        // rb.angularVelocity = rotationDifferenceInDegree * Mathf.Deg2Rad / Time.deltaTime;
    }
}
