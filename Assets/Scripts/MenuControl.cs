using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuControl : MonoBehaviour
{
    [SerializeField] private TMP_Text volumetextvalue = null;
    [SerializeField] private Slider volumeslider = null;
    [SerializeField] private GameObject confirmationPrompt = null;

    [Header("Gameplay Settings")]
    [SerializeField] private TMP_Text controllerSenTextValue = null;
    [SerializeField] private Slider controllerSensSlider = null;
    [SerializeField] private int defaultSen = 4;
    public int mainControllerSen = 4;

    [Header("Levels To Load")]
    public string _newGameLevel;
    private string levelToLoad;

    [SerializeField] private GameObject noSavedGameDialog = null;

    private void Start()
    {
        LoadSettings();
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("volume", volumeslider.value);
        PlayerPrefs.SetInt("controllerSensitivity", (int)controllerSensSlider.value);
        PlayerPrefs.Save();
    }

    public void LoadSettings()
    {
        if (PlayerPrefs.HasKey("volume"))
        {
            volumeslider.value = PlayerPrefs.GetFloat("volume");
            volumetextvalue.text = volumeslider.value.ToString("0.0");
        }
        if (PlayerPrefs.HasKey("controllerSensitivity"))
        {
            controllerSensSlider.value = PlayerPrefs.GetInt("controllerSensitivity");
            controllerSenTextValue.text = controllerSensSlider.value.ToString("0");
            mainControllerSen = PlayerPrefs.GetInt("controllerSensitivity");
        }
    }

    public void NewGameDialogYes()
    {
        SaveSettings();
        SceneManager.LoadScene(_newGameLevel);
    }

    public void LoadGameDialogYes()
    {
        SaveSettings();
        if (PlayerPrefs.HasKey("SavedLevel"))
        {
            levelToLoad = PlayerPrefs.GetString("SavedLevel");
            SceneManager.LoadScene(levelToLoad);
        }
        else
        {
            noSavedGameDialog.SetActive(true);
        }
    }

    public void ExitButton()
    {
        SaveSettings();
        Application.Quit();
    }

    public void SetVolume(float volume)
    {
        volumetextvalue.text = volume.ToString("0.0");
        AudioListener.volume = volume;
    }

    public void SetControllerSensitivity(float sensitivity)
    {
        controllerSenTextValue.text = sensitivity.ToString("0");
        mainControllerSen = (int)sensitivity;
    }
}
