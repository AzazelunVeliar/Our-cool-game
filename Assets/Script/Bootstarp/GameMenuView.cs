using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuView : MonoBehaviour
{
    public GameObject menuPanel;
    private Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    public void OnSaveClicked()
    {
        GameBootstrapper.SaveRepo.SavePlayer(player);
    }

    public void OnLoadClicked()
    {
        GameBootstrapper.SaveRepo.LoadPlayer(player);
    }

    public void OnMainMenuClicked()
    {
        SceneManager.LoadScene("MainMenu"); // название сцены главное меню
    }

    public void ToggleMenu()
    {
        menuPanel.SetActive(!menuPanel.activeSelf);
    }
}
