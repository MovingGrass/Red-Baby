using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepScript : MonoBehaviour
{
    public GameObject footstep;

    // Start is called before the first frame update
    void Start()
    {
        footstep.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        bool isMoving = Input.GetKey("w") || Input.GetKey("s") || Input.GetKey("a") || Input.GetKey("d");
        
        if (isMoving)
        {
            footsteps();
        }
        else
        {
            StopFootsteps();
        }
    }

    void footsteps()
    {
        if (!footstep.activeSelf)
        {
            footstep.SetActive(true);
        }
    }

    void StopFootsteps()
    {
        if (footstep.activeSelf)
        {
            footstep.SetActive(false);
        }
    }
}
