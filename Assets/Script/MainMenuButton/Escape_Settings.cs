using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Escape_Settings : MonoBehaviour
{
    [SerializeField] GameObject gameObject;
    [SerializeField] GameObject gameObject2;
    [SerializeField] GameObject gameObject3;
    [SerializeField] GameObject gameObject4;
    bool isPaused = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }
    void TogglePause()
    {
        gameObject.SetActive(true);
        isPaused = !isPaused;

        if (isPaused)
        {

            gameObject.SetActive(true);
            gameObject2.SetActive(true);
            gameObject3.SetActive(false);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            gameObject.SetActive(false);
            gameObject2.SetActive(false);
            gameObject4.SetActive(false);
            gameObject3.SetActive(true);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
