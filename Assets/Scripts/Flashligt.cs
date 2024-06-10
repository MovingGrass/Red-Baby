using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    [SerializeField] private GameObject flashlight;

    [SerializeField] private AudioSource turnOn;
    [SerializeField] private AudioSource turnOff;

    [SerializeField] private bool isOn;
   




    void Start()
    {
        isOn = false;
        flashlight.SetActive(false);
    }




    void Update()
    {
        if(!isOn && Input.GetButtonDown("F"))
        {
            flashlight.SetActive(true);
            turnOn.Play();
            
            isOn = true;
        }
        else if (isOn && Input.GetButtonDown("F"))
        {
            flashlight.SetActive(false);
            turnOff.Play();
            
            isOn = false;
        }



    }
}