using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class removeGuide : MonoBehaviour
{

    public GameObject prompt;

void OnTriggerEnter (Collider other){

if ((other.tag != "flask") || (other.tag != "baseElement")){

prompt.SetActive(false);

}
}

}
