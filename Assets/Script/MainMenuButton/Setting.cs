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
    public TMP_Dropdown dropdown3;
    Resolution[] resolutions;
    [SerializeField] GameObject gameObject;
    [SerializeField] GameObject gameObject2;
    [SerializeField] GameObject gameObject3;

    public enum DifficultyLevel
    {
        Easy,
        Normal,
        Hard
    }
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
        PlayerPrefs.SetInt("DifficultyLevel", dropdown3.value);
        Debug.Log((dropdown3.value));
        if(dropdown3.value==0){
            GameSettings.PeacefulMode=true;
        }
        else{
            GameSettings.PeacefulMode=false;
        }
    }
    void InitializeDifficultyDropdown()
    {
        dropdown3.ClearOptions();
        List<string> difficultyOptions = new List<string>();
        difficultyOptions.Add("Easy");
        difficultyOptions.Add("Normal");
        difficultyOptions.Add("Hard");
        dropdown3.AddOptions(difficultyOptions);
        dropdown3.RefreshShownValue();
    }
    public void SetDifficulty(int difficultyIndex)
    {
        PlayerPrefs.SetInt("DifficultyLevel", difficultyIndex);
        ApplyDifficultySettings((DifficultyLevel)difficultyIndex);
    }

void ApplyDifficultySettings(DifficultyLevel difficulty)
{
    switch (difficulty)
    {
        case DifficultyLevel.Easy:
            GameSettings.PeacefulMode = true;
            GameSettings.DamageMultiplier = 0.7f;  // игрок наносит меньше урона
            GameSettings.HealthMultiplier = 0.8f;  // у босса меньше здоровья
            GameSettings.EnemyAttackRate = 0.8f;  // враги атакуют реже
            GameSettings.PlayerDamageTaken = 0.7f; // игрок получает меньше урона
            break;
            
        case DifficultyLevel.Normal:
            GameSettings.PeacefulMode = false;
            GameSettings.DamageMultiplier = 1f;
            GameSettings.HealthMultiplier = 1f;
            GameSettings.EnemyAttackRate = 1f;
            GameSettings.PlayerDamageTaken = 1f;
            break;
            
        case DifficultyLevel.Hard:
            GameSettings.PeacefulMode = false;
            GameSettings.DamageMultiplier = 1.3f;  // игрок наносит больше урона
            GameSettings.HealthMultiplier = 1.5f;  // у босса больше здоровья
            GameSettings.EnemyAttackRate = 1.3f;   // враги атакуют чаще
            GameSettings.PlayerDamageTaken = 1.5f; // игрок получает больше урона
            break;
    }
    
    Debug.Log($"Difficulty set to: {difficulty}");
}
    public void LoadSettings(int currentresolutionindex)
    {
        if (PlayerPrefs.HasKey("QualitySettingReferens"))
            dropdown2.value = PlayerPrefs.GetInt("QualitySettingReferens"); 
        else
            dropdown2.value = 3; // ���������� 'values' �� 'value' 

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