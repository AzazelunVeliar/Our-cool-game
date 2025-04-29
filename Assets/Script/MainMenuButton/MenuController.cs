using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private Button saveButton;
    [SerializeField] private Button loadButton;
    
    private SaveSystem saveSystem;
    private Player player;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        saveSystem = new SaveSystem(player);
        
        saveButton.onClick.AddListener(SaveGame);
        loadButton.onClick.AddListener(LoadGame);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleMenu();
        }
    }

    public void ToggleMenu()
    {
        menuPanel.SetActive(!menuPanel.activeSelf);
        Time.timeScale = menuPanel.activeSelf ? 0 : 1;
    }

    private void SaveGame()
    {
        saveSystem.SaveGame();
        ToggleMenu();
    }

    private void LoadGame()
    {
        saveSystem.LoadGame();
        ToggleMenu();
    }
}