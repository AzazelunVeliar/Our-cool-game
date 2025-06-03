using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class ScoreView : MonoBehaviour
{
    Text _text;

    void Awake()
    {
        _text = GetComponent<Text>();
    }

    void OnEnable()
    {
        // подписываемся на событие
        ScoreManager.OnScoreChanged += UpdateView;
        // обновляем текущее значение сразу
        UpdateView(ScoreManager.Score);
    }

    void OnDisable()
    {
        ScoreManager.OnScoreChanged -= UpdateView;
    }

    void UpdateView(int newScore)
    {
        _text.text = $"Score: {newScore}";
    }
    void Update(){
       UpdateView(ScoreManager.Score);
    }
}