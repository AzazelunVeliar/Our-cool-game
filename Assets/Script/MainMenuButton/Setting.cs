using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Setting : MonoBehaviour
{
    public TMP_Dropdown dropdown1;
    public TMP_Dropdown dropdown2;
    Resolution[] resolutions;
    [SerializeField] GameObject gameObject;
    [SerializeField] GameObject gameObject2;
    [SerializeField] GameObject gameObject3;

    void Start()
    {
        dropdown1.ClearOptions();
        List<string> options = new List<string>();
        resolutions = Screen.resolutions; 
        int currentresolutionindex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height + " " + resolutions[i].refreshRate + "Hz"; 
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                currentresolutionindex = i;
        }
        dropdown1.AddOptions(options);
        dropdown1.RefreshShownValue();
        LoadSettings(currentresolutionindex);
    }


    public void SetFullscreen(bool isfullscreen)
    {
        Screen.fullScreen = isfullscreen;
    }

    public void SetResolution(int resolutionindex)
    {
        Resolution resolution = resolutions[resolutionindex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetQuality(int qualityindex)
    {
        QualitySettings.SetQualityLevel(qualityindex); 
    }

    public void ExitSettings()
    {
        gameObject.SetActive(false);
        gameObject2.SetActive(false);
        gameObject3.SetActive(true);
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetInt("QualitySettingReferens", dropdown2.value);
        PlayerPrefs.SetInt("ResolutionReferens", dropdown1.value);
        PlayerPrefs.SetInt("FullScreenReference", System.Convert.ToInt32(Screen.fullScreen));
    }

    public void LoadSettings(int currentresolutionindex)
    {
        if (PlayerPrefs.HasKey("QualitySettingReferens"))
            dropdown2.value = PlayerPrefs.GetInt("QualitySettingReferens"); 
        else
            dropdown2.value = 3; // Исправлено 'values' на 'value' 

        if (PlayerPrefs.HasKey("ResolutionReferens"))
            dropdown1.value = PlayerPrefs.GetInt("ResolutionReferens");
        else
            dropdown1.value = currentresolutionindex; 

        if (PlayerPrefs.HasKey("FullScreenReference"))
            Screen.fullScreen = System.Convert.ToBoolean(PlayerPrefs.GetInt("FullScreenReference")); 
        else
            Screen.fullScreen = true;
    }
    public void Game()
    {
        SceneManager.LoadScene("Game");
    }
    public void Exite_to_main_menu_button()
    {
        SceneManager.LoadScene("Main Menu");
    }
}