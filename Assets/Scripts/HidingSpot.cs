using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hidingPlace : MonoBehaviour
{
    public GameObject hideText, stopHideText;
    public GameObject normalPlayer, hidingPlayer;
    
    public enemyAI[] monsterScripts; // Array to store multiple enemyAI scripts
    public Transform[] monsterTransforms; // Array to store multiple enemy Transforms
    
    bool interactable, hiding;
    public float loseDistance;
    public AudioSource hideSound, stopHideSound;
    public roomDetector detector;

    void Start()
    {
        interactable = false;
        hiding = false;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            if (detector.inTrigger)
            {
                hideText.SetActive(true);
                interactable = true;
            }
            else
            {
                hideText.SetActive(false);
                interactable = false;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            hideText.SetActive(false);
            interactable = false;
        }
    }

    void Update()
    {
        if (interactable && Input.GetKeyDown(KeyCode.E))
        {
            hideText.SetActive(false);
            hideSound.Play();
            hidingPlayer.SetActive(true);

            foreach (var monsterScript in monsterScripts)
            {
                float distance = Vector3.Distance(monsterScript.transform.position, normalPlayer.transform.position);
                if (distance > loseDistance && monsterScript.chasing)
                {
                    monsterScript.stopChase();
                }
            }

            stopHideText.SetActive(true);
            hiding = true;
            normalPlayer.SetActive(false);
            interactable = false;
        }

        if (hiding && Input.GetKeyDown(KeyCode.Q))
        {
            stopHideText.SetActive(false);
            stopHideSound.Play();
            normalPlayer.SetActive(true);
            hidingPlayer.SetActive(false);
            hiding = false;
        }
    }
}
