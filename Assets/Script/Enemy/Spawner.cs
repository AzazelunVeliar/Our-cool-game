using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Spawner : MonoBehaviour
{
    [Header("Префабы мобов и босса")]
    public GameObject meleeEnemyPrefab;
    public GameObject rangedEnemyPrefab;
    public GameObject bossPrefab;
    IMobFactory meleeFactory;
    IMobFactory rangedFactory;
    IMobFactory bossFactory;

    [Header("Интервалы (сек)")]
    public float mobInterval = 8f;

    [Header("Область спавна")]
    public float spawnAreaSize = 10f;

    private int killCount = 0;
    private bool bossSpawned = false;
    private bool victoryLogged = false;
    private int previousScore;
    public Text text;

    void Start()
    {
        meleeFactory  = new MeleeEnemyFactory(meleeEnemyPrefab);
        rangedFactory = new RangedEnemyFactory(rangedEnemyPrefab);
        bossFactory   = new BossFactory(bossPrefab);
        previousScore = ScoreManager.Score;
        ScoreManager.OnScoreChanged += OnScoreChanged;

        InvokeRepeating(nameof(SpawnMob), 0f, mobInterval);
    }

    void OnDestroy()
    {
        ScoreManager.OnScoreChanged -= OnScoreChanged;
    }

    private void OnScoreChanged(int newScore)
    {
        int delta = newScore - previousScore;
        previousScore = newScore;

        if (delta > 0)
        {
            killCount++;
            Debug.Log($"Убийств мобов: {killCount}");

            if (!bossSpawned && killCount >= 3)
            {
                bossSpawned = true;
                Debug.Log("Спавним босса по условию (3 убийства)");
                SpawnBoss();
            }

            if (!victoryLogged && killCount >= 5)
            {
                victoryLogged = true;
                text.text="ПОБЕДА";
                Debug.Log("Вы убили 5 врагов");
                StartCoroutine(SwitchSceneAfterDelay(10f));
            }
        }
    }

   void SpawnMob()
    {
        var pos = RandomPosition();
        if (Random.value < 0.5f)
            meleeFactory.Create(pos);
        else
            rangedFactory.Create(pos);
    }

    void SpawnBoss()
    {
        bossFactory.Create(RandomPosition());
    }

    Vector3 RandomPosition()
    {
        float half = spawnAreaSize / 2f;
        return new Vector3(
            UnityEngine.Random.Range(-half, half),
            1f,
            UnityEngine.Random.Range(-half, half)
        );
    }
    private IEnumerator SwitchSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Main Menu");
    }
}