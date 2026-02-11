using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    [SerializeField] private Animator door;
    [SerializeField] private GameObject openText;
    [SerializeField] private GameObject closeText;


    [SerializeField] private AudioSource doorSound;
    [SerializeField] private AudioSource doorClosed;


    [SerializeField] private bool inReach;

    bool open;

    public bool IsOpen()
    {
        return open;
    }

    public void SetDoorState(bool state)
    {
        open = state;
        // Update the animator immediately to match the saved state
        if(open)
        {
            door.SetBool("Open", true);
            door.SetBool("Closed", false);
        }
        else
        {
            door.SetBool("Open", false);
            // If we load a closed door, ensure the animation is in closed state
            door.SetBool("Closed", true); 
        }
    }



    void Start()
    {
        open = false;
        inReach = false;
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
        }
        
    }





    void Update()
    {

        if (inReach && Input.GetButtonDown("Interact") && open == false)
        {
            DoorOpens();
        }

        else if (inReach && Input.GetButtonDown("Interact") && open == true)
        {
            DoorCloses();
        }




    }
    void DoorOpens ()
    {
        
        door.SetBool("Open", true);
        door.SetBool("Closed", false);
        doorSound.Play();
        open = true;

    }

    void DoorCloses()
    {
        
        door.SetBool("Open", false);
        door.SetBool("Closed", true);
        open = false;
        doorClosed.Play();
    }


}