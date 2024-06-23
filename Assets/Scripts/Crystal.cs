using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    [SerializeField] private GameObject crystal;
    [SerializeField] private GameObject pickUpText;
    [SerializeField] private AudioSource keySound;

    private bool inReach;
    [SerializeField] ExitDoor exitDoor;

    void Start()
    {
        inReach = false;
        pickUpText.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = true;
            pickUpText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = false;
            pickUpText.SetActive(false);
        }
    }

    void Update()
    {
        if (inReach && Input.GetButtonDown("Interact"))
        {
            keySound.Play();
            // Create a CrystalData object and pass it to the ExitDoor
            CrystalData crystalData = new CrystalData(gameObject.name); // Assuming the name is unique and used as the ID
            exitDoor.AddCrystal(crystalData);
            pickUpText.SetActive(false);
            crystal.SetActive(false);
        }
    }
}
