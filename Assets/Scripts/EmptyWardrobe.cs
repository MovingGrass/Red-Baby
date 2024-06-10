using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyWardrobe : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Animator boxOB;
    [SerializeField] private GameObject openText;
    [SerializeField] private AudioSource openSound;
    [SerializeField] private bool inReach;
    [SerializeField] private bool isOpen;
    [SerializeField] private Collider collider;



    void Start()
    {
        inReach = false;
        openText.SetActive(false);
        
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
           
        }
    }


    void Update()
    {
        if ( inReach && Input.GetButtonDown("Interact") && isOpen == false)
        {
            
            openSound.Play();
            boxOB.SetBool("Open", true);
            openText.SetActive(false);
            
            isOpen = true;
        }

       

        if(isOpen)
        {
            collider.enabled = false;
            this.enabled = false;
        }
    }
}
