using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackDoor : MonoBehaviour
{
    [SerializeField] private Animator door;
    [SerializeField] private GameObject openText;
    [SerializeField] private GameObject LockedText;

    [SerializeField] private GameObject closeText;
    [SerializeField] private GameObject KeyINV;

    [SerializeField] private AudioSource doorSound;
    [SerializeField] private AudioSource lockedSound;
    [SerializeField] private AudioSource closedSound;


    private bool inReach;
    private bool unlocked;
    private bool locked;
    private bool hasKey;

    private bool open;





    void Start()
    {
        inReach = false;
        hasKey = false;
        unlocked = false;
        locked = true;
        open = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Reach" && open == false)
        {
            inReach = true;
            openText.SetActive(true);
            closeText.SetActive(false);

        }
        
        
        else if (other.gameObject.tag == "Reach" && open == true)
        {
            inReach = true;
            openText.SetActive(false);
            closeText.SetActive(true);
            
        }
    }
    

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = false;
            openText.SetActive(false);
            closeText.SetActive(false);
            LockedText.SetActive(false);
        }
    }





    void Update()
    {
        if(KeyINV.activeInHierarchy)
        {
            locked = false;
            hasKey = true;
        }  
        
        else
        {
            hasKey = false;
        }

        if (hasKey && inReach && Input.GetButtonDown("Interact") && open == false)
        {
            unlocked = true;
            DoorOpens();
        }

        else if(hasKey && inReach && Input.GetButtonDown("Interact") && open == true)
        {
            DoorCloses();
        }

        if (locked && inReach && Input.GetButtonDown("Interact"))
        {
            lockedSound.Play();
            LockedText.SetActive(true);
            openText.SetActive(false);
            
        }




    }
    void DoorOpens ()
    {
        if (unlocked)
        {
            door.SetBool("Open", true);
            door.SetBool("Closed", false);
            doorSound.Play();
            open = true;
        }

    }

    void DoorCloses()
    {
        if (unlocked)
        {
            door.SetBool("Open", false);
            door.SetBool("Closed", true);
            closedSound.Play();
            open = false;
        }

    }


}