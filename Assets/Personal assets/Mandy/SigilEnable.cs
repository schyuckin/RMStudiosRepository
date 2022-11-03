using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SigilEnable : MonoBehaviour
{

    [SerializeField] private Collider[] differentSigils = new Collider[3];
    public ParticleSystem[] differentParticles = new ParticleSystem[3];
    public GameObject[] cauldronEffects = new GameObject[3];

    [SerializeField] private GameObject flask;
    [SerializeField] private int sigTouched = 0; // Used in conjuction to prevent some potentially weird value passing stuff
    public int sigilTouched = 0; // Used in conjuction to prevent some potentially weird value passing stuff

    //I need to check if no other particles have been enabled beforehand or else they will stack on each other
    public bool particleEnabled;

    public ParticleSystem[] aura_purple = new ParticleSystem[5];

    public void Start() 
    {
        //PAUSE ALL PARTICLES IN THE BEGINNING SO THEY WON'T SHOW UP IN THE GAME
        differentParticles[0].Pause();
        differentParticles[1].Pause();
        differentParticles[2].Pause();
        Debug.Log("They paused");
        particleEnabled = false;

        cauldronEffects[0].GetComponentInChildren<Renderer>().enabled = false;
        cauldronEffects[0].GetComponentInChildren<Light>().enabled = false;
        cauldronEffects[1].GetComponentInChildren<Renderer>().enabled = false;
        cauldronEffects[1].GetComponentInChildren<Light>().enabled = false;
        aura_purple[0].Pause();
        aura_purple[1].Pause();
        aura_purple[2].Pause();
        aura_purple[3].Pause();
        aura_purple[4].Pause();
        cauldronEffects[2].GetComponentInChildren<Light>().enabled = false;
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
                if (particleEnabled == false)
                {
                    differentParticles[0].Play();
                    sigTouched = 1;
                    particleEnabled = true;
                }
                }

                if (other == differentSigils[1])
                {
                if (particleEnabled == false)
                {
                    differentParticles[1].Play();
                    sigTouched = 2;
                    particleEnabled = true;
                }
                }
                if (other == differentSigils[2])
                {
                if (particleEnabled == false)
                {
                    differentParticles[2].Play();
                    sigTouched = 3;
                    particleEnabled = true;
                }
                }

                sigilTouched = sigTouched;
            }
            //sigTouched = 0;
            //particles.Stop(); //Here we use the Stop function to stop the particle system from playing
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //STOP PARTICLES IF SIGILE IS TOUCHED AND WILL PASS ON ONTO THE CAULDRON
        if (collision.gameObject.tag == "Cauldron")
        {
            // Passing stuff to flask
            //var passingToFlask = flask.GetComponent<flaskState>();
            //passingToFlask.sigilType = sigilTouched;
            differentParticles[0].Stop();
            differentParticles[1].Stop();
            differentParticles[2].Stop();
            StartCoroutine(SigilDecay());
            particleEnabled = false;

            //ENABLED THE EFFECTS ON THE CAULDRON DEPENDING ON WHAT SIGIL IS ENABLED
            if(sigTouched == 1)
            {
                cauldronEffects[0].GetComponentInChildren<Renderer>().enabled = true;
                cauldronEffects[0].GetComponentInChildren<Light>().enabled = true;
            }
            if (sigTouched == 2)
            {
                cauldronEffects[1].GetComponentInChildren<Renderer>().enabled = true;
                cauldronEffects[1].GetComponentInChildren<Light>().enabled = true;
            }
            if (sigTouched == 3)
            {

                cauldronEffects[2].GetComponentInChildren<Light>().enabled = true;
                aura_purple[0].Play();
                aura_purple[1].Play();
                aura_purple[2].Play();
                aura_purple[3].Play();
                aura_purple[4].Play();


            }
        }
    }

    IEnumerator SigilDecay() // Resets everything to standard values
    {
        yield return new WaitForSeconds(2.5f);
        sigilTouched = sigTouched;
    }
}
