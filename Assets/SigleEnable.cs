using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SigleEnable : MonoBehaviour
{
    private void OnCollisionEnter(Collider other)
    {
        //Saving the component for future use 
        ParticleSystem particles = GetComponentInChildren<ParticleSystem>();

        //Here we check if we indeed find the Particle system and can use it
        //or else we would get an error if we work with not existing component
        if (particles != null)
        {
            if (other.CompareTag("potionSigil"))
            {
                particles.Play(); //Here we use the Play function to start the particle system
            }
            else
            {
                particles.Stop(); //Here we use the Stop function to stop the particle system from playing
            }
        }
    }
}
