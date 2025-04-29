using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    public void Exit_Button()
    {
        Application.Quit();
    }
    public void Restart()
    {
        SceneManager.LoadScene("Game");

    }
}
