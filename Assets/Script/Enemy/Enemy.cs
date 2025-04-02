using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public string Figure_type_enemy;
    public string Figure_Name;
    public Transform player_position;
    public int Enemy_hp = 100;
    public int Enemy_maxhp = 100;
    public bool White_color;
    public bool Black_color;
    public int Enemy_attack = 20;
    public float maxStamina = 100f;
    public float currentStamina;
    public float staminaRecoveryRate = 10f;
    public float staminaRecoveryDelay = 2f;
    public float dashDistance = 10f;
    public float dashSpeed = 50f;
    private NavMeshAgent agent;
    public float attackRadius = 2f;
    private bool canAttack = true;
    private float attackCooldown = 3f;
    private Player player;
    public float jumpForce;
    private Rigidbody rb;
    private Animator animator;
    [SerializeField] GameObject after_death;
    [SerializeField] GameObject bow;

    void Start()
    {

    }

    void Update()
    {

    }

}
