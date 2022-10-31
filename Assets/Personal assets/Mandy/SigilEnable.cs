using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SigilEnable : MonoBehaviour
{

    [SerializeField] private Collider[] differentSigils = new Collider[3];
    public ParticleSystem[] differentParticles = new ParticleSystem[3];

    [SerializeField] private GameObject flask;
    [SerializeField] private int sigTouched = 0; // Used in conjuction to prevent some potentially weird value passing stuff
    public int sigilTouched = 0; // Used in conjuction to prevent some potentially weird value passing stuff

    public void Start()
    {
        differentParticles[0].Pause();
        differentParticles[1].Pause();
        differentParticles[2].Pause();
        Debug.Log("They paused");
    }

    private void OnTriggerEnter(Collider other)
    {
        //Saving the component for future use 
        ParticleSystem particles = GetComponentInChildren<ParticleSystem>();

        //Here we check if we indeed find the Particle system and can use it
        //or else we would get an error if we work with not existing component
        if (particles != null)
        {
            if (other.tag == "potionSigil")
            {

                Debug.Log("It registeres touching the enchantment");
                //particles.Play(); //Here we use the Play function to start the particle system

                // [NOTE] I am not sure if the funky syntax works like that
                if (other == differentSigils[0])
                {
                    differentParticles[0].Play();
                    sigTouched = 1;
                }
                if (other == differentSigils[1])
                {
                    differentParticles[1].Play();
                    sigTouched = 2;
                }
                if (other == differentSigils[2])
                {
                    differentParticles[2].Play();
                    sigTouched = 3;
                }

                sigilTouched = sigTouched;
            }
            sigTouched = 0;
            //particles.Stop(); //Here we use the Stop function to stop the particle system from playing
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "flask")
        {
            // Passing stuff to flask
            var passingToFlask = flask.GetComponent<flaskState>();
            passingToFlask.sigilType = sigilTouched;
            differentParticles[0].Stop();
            differentParticles[1].Stop();
            differentParticles[2].Stop();
            StartCoroutine(SigilDecay());
        }
    }

    IEnumerator SigilDecay() // Resets everything to standard values
    {
        yield return new WaitForSeconds(2.5f);
        sigilTouched = sigTouched;
    }
}
