using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExitDoor : MonoBehaviour
{
    private List<CrystalData> collectedCrystals;
    [SerializeField] private bool inReach;

    [SerializeField] private SceneChanger sceneChanger;

    bool open;

    [SerializeField] private GameObject ExitText;
    [SerializeField] private GameObject CollectText;
    [SerializeField] private int crystal_number;

    [SerializeField] private TextMeshProUGUI crystalCountText;

    private void Awake()
    {
        collectedCrystals = new List<CrystalData>();
        open = false;
        inReach = false;
        UpdateCrystalCountText();
    }

    public void AddCrystal(CrystalData crystal)
    {
        collectedCrystals.Add(crystal);
        UpdateCrystalCountText();
    }

    public List<CrystalData> GetCollectedCrystals()
    {
        return collectedCrystals;
    }

    public void SetCollectedCrystals(List<CrystalData> crystals)
    {
        collectedCrystals = crystals;
        UpdateCrystalCountText();
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
        if (inReach && Input.GetButtonDown("Interact") && collectedCrystals.Count == crystal_number)
        {
            sceneChanger.ChangeSceneString("Win");
        }
        else if (inReach && Input.GetButtonDown("Interact") && collectedCrystals.Count < crystal_number)
        {
            CollectText.SetActive(true);
            ExitText.SetActive(false);
        }
        UpdateCrystalCountText();
    }

    private void UpdateCrystalCountText()
    {
        crystalCountText.text = "Crystal collected:\n" + collectedCrystals.Count + " / " + crystal_number;
    }
}


