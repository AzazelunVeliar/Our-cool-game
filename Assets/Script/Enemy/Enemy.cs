using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public int Enemy_hp = 100;
    public int Enemy_maxhp = 100;
    private NavMeshAgent agent;
    private Player player;
    public Slider enemyslider;

    void Start()
    {
        Enemy_hp = Enemy_maxhp;
    }
    void Update()
    {
        UpdateHealthBar();
    }
    void UpdateHealthBar()
    {
        enemyslider.value = Enemy_hp;
    }
    private void FixedUpdate()
    {
        Update();
    }
}
