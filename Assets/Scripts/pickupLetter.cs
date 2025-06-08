using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pickupLetter : MonoBehaviour
{
    public GameObject collectTextObj, intText;
    public AudioSource pickupSound;
    public AudioSource[] ambianceLayers; 

    public bool interactable;
    public static int pagesCollected;
    public Text collectText;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            intText.SetActive(true);
            interactable = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            intText.SetActive(false);
            interactable = false;
        }
    }

    void Update()
    {
        if (interactable && Input.GetKeyDown(KeyCode.E))
        {
            pagesCollected++;
            collectText.text = pagesCollected + "/8 pages";
            collectTextObj.SetActive(true);
            pickupSound.Play();


            if (pagesCollected - 1 < ambianceLayers.Length)
            {
                ambianceLayers[pagesCollected - 1].Play();
            }
            intText.SetActive(false);
            this.gameObject.SetActive(false);
            interactable = false;
        }
    }
}
