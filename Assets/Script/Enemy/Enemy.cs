using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Transform player_position;
    public int Enemy_hp = 100;
    public int Enemy_maxhp = 100;
    public int Enemy_attack = 20;
    public float maxStamina = 100f;
    public float currentStamina;
    public float staminaRecoveryRate = 10f;
    public float staminaRecoveryDelay = 2f;
    private NavMeshAgent agent;
    public float attackRadius = 2f;
    private bool canAttack = true;
    private float attackCooldown = 3f;
    private Player player;
    private Rigidbody rb;
    private Animator animator;
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
        if (Enemy_hp <= 0)
        {
            GameObject.Destroy(gameObject);
        }
    }
    private void FixedUpdate()
    {
        Update();
    }
}
