using System;
using UnityEngine;
public static class ScoreManager
{
    private static int _score;
    public static int Score => _score;

    // событие, которое слушатели подпишут, чтобы узнавать об изменении
    public static event Action<int> OnScoreChanged;

    public static void Add(int points)
    {
        _score += points;
        OnScoreChanged?.Invoke(_score);
    }
}