using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrapper : MonoBehaviour
{
    [SerializeField] private GameObject audioServicePrefab; // Изменено на GameObject
    [SerializeField] private GameObject saveServicePrefab; // Изменено на GameObject

    private void Awake()
    {
        // Создаем сервисы как GameObject
        Instantiate(audioServicePrefab);
        Instantiate(saveServicePrefab);
        
        SceneManager.LoadScene("MainMenu");
    }
}