using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SigilEnable : MonoBehaviour
{

    [SerializeField] private Collider[] differentSigils = new Collider[3];
    [SerializeField] private GameObject flask;
    private int sigTouched = 0; // Used in conjuction to prevent some potentially weird value passing stuff
    public int sigilTouched = 0; // Used in conjuction to prevent some potentially weird value passing stuff
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
                if (other == differentSigils[0])
                {
                    sigTouched = 1;
                }
                if (other == differentSigils[1])
                {
                    sigTouched = 2;
                }
                if (other == differentSigils[2])
                {
                    sigTouched = 3;
                }
                sigilTouched = sigTouched;
            }
            // Not sure about that
            if (other == flask.GetComponent<BoxCollider>())
            {
                // Passing stuff to flask
                var passingToFlask = GetComponent<flaskState>();
                passingToFlask.sigilType = sigilTouched;
            }
            sigTouched = 0;
            particles.Stop(); //Here we use the Stop function to stop the particle system from playing
            StartCoroutine(SigilDecay());
        }
    }

    IEnumerator SigilDecay() // Resets everything to standard values
    {
        yield return new WaitForSeconds(2.5f);
        sigilTouched = sigTouched;
    }
}
