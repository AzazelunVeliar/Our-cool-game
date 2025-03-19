using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicSlider : MonoBehaviour
{
    public Slider slider;
    public AudioSource audio;
    public float volume;
    public Toggle toggle;
    void Start()
    {
        Load();
        ValumeMusic();
    }


    public void SliderMusic()
    {
        volume=slider.value;
        Save();
        ValumeMusic();
    }
    public void TogleMusic()
    {
        if (toggle.isOn == true)
        {
            volume = 1;
        }
        else
        {
            volume = 0;
        }
        Save();
        ValumeMusic();
    }
    private void ValumeMusic()
    {
        audio.volume = volume;
        slider.value= volume;
        volume = slider.value;
        if(volume == 0 )
        {
            toggle.isOn = false;
        }
        else
        {
            toggle.isOn = true;
        }
    }
    private void Save()
    {
        PlayerPrefs.SetFloat("volume", volume);
    }
    private void Load()
    {
        volume = PlayerPrefs.GetFloat("volume", volume);
    }
}
