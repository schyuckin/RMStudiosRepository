using System;
using System.Collections;
using System.Collections.Generic;
using Oculus.Platform;
using Unity.Mathematics;
using UnityEngine;

public class Explode : MonoBehaviour
{
    private Rigidbody _rigidbody;

    public GameObject explosionParticle;
    
    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Environment"))
        {
            print("collided");
            if (_rigidbody.velocity.magnitude >= 0.1f)
            {
                Destroy(gameObject);
                Instantiate(explosionParticle, transform.position, quaternion.identity);
                
                //spawn new bottle here;
            }
        }
    }
}
