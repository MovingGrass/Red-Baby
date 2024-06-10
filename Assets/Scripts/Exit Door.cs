using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExitDoor : MonoBehaviour
{
    // Start is called before the first frame update
    private List<Crystal>Crystals;
    [SerializeField] private bool inReach;

    [SerializeField] private SceneChanger sceneChanger;

    bool open;

    [SerializeField] private GameObject ExitText;
    [SerializeField] private GameObject CollectText;
    [SerializeField] private int crystal_number;

     [SerializeField] private TextMeshProUGUI crystalCountText;

    private void Awake()
    {
        Crystals = new List<Crystal>();
        open = false;
        inReach = false;
        UpdateCrystalCountText();
    }

    public void AddCrystal(Crystal crystal)
    {
        Crystals.Add(crystal);
    }
    
     void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Reach" && open == false)
        {
            inReach = true;
            ExitText.SetActive(true);
           
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = false;
            ExitText.SetActive(false);
            CollectText.SetActive(false);        
        }
        
    }
   
    void Update()
    {
        if (inReach && Input.GetButtonDown("Interact") && Crystals.Count == crystal_number )
        {
            Debug.Log("15");
            sceneChanger.ChangeSceneString("Win");
        }
        else if (inReach && Input.GetButtonDown("Interact") && Crystals.Count < crystal_number)
        {
            CollectText.SetActive(true);
            ExitText.SetActive(false);
        }
         UpdateCrystalCountText();
    }
    private void UpdateCrystalCountText()
    {
        crystalCountText.text = "Crystal collected:\n" + Crystals.Count + " / " + crystal_number;
    }

}
