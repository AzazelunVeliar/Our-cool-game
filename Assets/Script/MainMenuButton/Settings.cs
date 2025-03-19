using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    [SerializeField] GameObject Setings_menu;
    [SerializeField] GameObject Setings_menu2;
    [SerializeField] GameObject Setings_menu3;
    void Start()
    {
        Setings_menu.SetActive(false);
        Setings_menu2.SetActive(false);

    }
    public void Setings_Button()
    {
        Setings_menu.SetActive(true);
        Setings_menu2.SetActive(true);
        Setings_menu3.SetActive(false);
    }
}
