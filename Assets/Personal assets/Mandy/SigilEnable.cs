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
    [SerializeField] private cauldronController caulControl;

    //I need to check if no other particles have been enabled beforehand or else they will stack on each other
    public bool particleEnabled;
    public bool isEnchanted = false; // Does the cauldron have the sigil on it?

    public ParticleSystem[] aura_purple = new ParticleSystem[5];

    [Space] private AudioSource _audioSource;
    public void Start() 
    {
        //PAUSE ALL PARTICLES IN THE BEGINNING SO THEY WON'T SHOW UP IN THE GAME
        differentParticles[0].Pause(); // Mind
        differentParticles[1].Pause(); // Body
        differentParticles[2].Pause(); // Soul
        Debug.Log("They paused");
        particleEnabled = false;

        _audioSource = GetComponent<AudioSource>();
        CauldronEffects();
    }

    public void CauldronEffects()
    {
        isEnchanted = false;
        cauldronEffects[0].GetComponentInChildren<Renderer>().enabled = false;
        cauldronEffects[0].GetComponentInChildren<Light>().enabled = false;
        cauldronEffects[1].GetComponentInChildren<Renderer>().enabled = false;
        cauldronEffects[1].GetComponentInChildren<Light>().enabled = false;
        aura_purple[0].Stop();
        aura_purple[1].Stop();
        aura_purple[2].Stop();
        aura_purple[3].Stop();
        aura_purple[4].Stop();
        cauldronEffects[2].GetComponentInChildren<Light>().enabled = false;
        sigTouched = 0;
        sigilTouched = sigTouched;
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
                _audioSource.Play();
                sigilTouched = sigTouched;
            }
        
            if (other.gameObject.tag == "Cauldron")
            {
                if (!isEnchanted)
                {
                    isEnchanted = true;
                    switch (sigilTouched)
                    {
                        case 1: // Mind
                            cauldronEffects[0].GetComponentInChildren<Renderer>().enabled = true;
                            cauldronEffects[0].GetComponentInChildren<Light>().enabled = true;
                            caulControl.cauldronSigil = 1;
                            break;
                        case 2: // Body
                            cauldronEffects[1].GetComponentInChildren<Renderer>().enabled = true;
                            cauldronEffects[1].GetComponentInChildren<Light>().enabled = true;
                            caulControl.cauldronSigil = 2;
                            break;
                        case 3: // Soul
                            cauldronEffects[2].GetComponentInChildren<Light>().enabled = true;
                            aura_purple[0].Play();
                            aura_purple[1].Play();
                            aura_purple[2].Play();
                            aura_purple[3].Play();
                            aura_purple[4].Play();
                            caulControl.cauldronSigil = 3;
                            break;
                    }
                }

                differentParticles[0].Stop();
                differentParticles[1].Stop();
                differentParticles[2].Stop();
                StartCoroutine(SigilDecay());
                particleEnabled = false;
            }
            //sigTouched = 0;
            //particles.Stop(); //Here we use the Stop function to stop the particle system from playing
        }
    }

    IEnumerator SigilDecay() // Resets everything to standard values
    {
        yield return new WaitForSeconds(2.5f);
        sigilTouched = sigTouched;
    }
}
