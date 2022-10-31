using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SigilEnable : MonoBehaviour
{

    [SerializeField] private Collider[] differentSigils = new Collider[3];
    [SerializeField] private GameObject[] differentParticles = new GameObject[3];

    [SerializeField] private GameObject flask;
    private int sigTouched = 0; // Used in conjuction to prevent some potentially weird value passing stuff
    public int sigilTouched = 0; // Used in conjuction to prevent some potentially weird value passing stuff

    public void Start()
    {
        differentParticles[0].GetComponent<ParticleSystem>().Pause();
        differentParticles[1].GetComponent<ParticleSystem>().Pause();
        differentParticles[2].GetComponent<ParticleSystem>().Pause();
    }


    private void OnTriggerEnter(Collision collision)
    {
        //Saving the component for future use 
        ParticleSystem particles = GetComponentInChildren<ParticleSystem>();

        //Here we check if we indeed find the Particle system and can use it
        //or else we would get an error if we work with not existing component
        if (particles != null)
        {
            if (collision.gameObject.tag == "potionSigil")
            {
                //particles.Play(); //Here we use the Play function to start the particle system

                // [NOTE] I am not sure if the funky syntax works like that
                if (collision.collider == differentSigils[0])
                {
                    sigTouched = 1;
                }
                if (collision.collider == differentSigils[1])
                {
                    sigTouched = 2;
                }
                if (collision.collider == differentSigils[2])
                {
                    sigTouched = 3;
                }
                differentParticles[sigTouched - 1].GetComponent<ParticleSystem>().Play();
                sigilTouched = sigTouched;
            }
            // [NOTE] Not sure about that either
            if (collision.collider == flask.GetComponent<BoxCollider>())
            {
                // Passing stuff to flask
                var passingToFlask = flask.GetComponent<flaskState>();
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
