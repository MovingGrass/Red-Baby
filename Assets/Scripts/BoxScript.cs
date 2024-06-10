using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : MonoBehaviour
{
    [SerializeField] private Animator boxOB;
    [SerializeField] private GameObject keyOBNeeded;
    [SerializeField] private GameObject openText;
    [SerializeField] private GameObject keyMissingText;
    [SerializeField] private GameObject Object_inside;
    [SerializeField] private AudioSource openSound;
    [SerializeField] private AudioSource LockedSound;
    [SerializeField] private bool inReach;
    [SerializeField] private bool isOpen;
    [SerializeField] private Collider collider;



    void Start()
    {
        inReach = false;
        openText.SetActive(false);
        keyMissingText.SetActive(false);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Reach" && isOpen == false)
        {
            inReach = true;
            openText.SetActive(true);

        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Reach" && isOpen == false)
        {
            inReach = false;
            openText.SetActive(false);
            keyMissingText.SetActive(false);
        }
    }


    void Update()
    {
        if (keyOBNeeded.activeInHierarchy == true && inReach && Input.GetButtonDown("Interact") && isOpen == false)
        {
            keyOBNeeded.SetActive(false);
            openSound.Play();
            boxOB.SetBool("Open", true);
            openText.SetActive(false);
            keyMissingText.SetActive(false);
            isOpen = true;
        }

        else if (keyOBNeeded.activeInHierarchy == false && inReach && Input.GetButtonDown("Interact"))
        {
            openText.SetActive(false);
            keyMissingText.SetActive(true);
            LockedSound.Play();
        }

        if(isOpen)
        {
            Object_inside.SetActive(true);
            collider.enabled = false;
            this.enabled = false;
        }
    }
}