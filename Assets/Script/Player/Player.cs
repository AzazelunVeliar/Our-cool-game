using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int HP;
    public int MP;

    public int Maxhp;
    public double Stamina;
    public int hp;
    public int att;
    public int matt;
    private Animator animator;

    private Enemy enemy;
    private PlayerController PC;
    private bool Attacking;
    public Transform cameraTransform;
    private float timeSinceMouseDown = 0f;
    private float meleeAttackCooldown = 1.2f; // ����� ����� �������
    private float lastMeleeAttackTime = 0f;
    private float rangedAttackCooldown = 2f;
    private float lastRangedAttackTime = 0f;
    public GameObject projectilePrefab;
    public float projectileSpeed = 10f;
    public Slider playerslider;
    public Slider playerslider2;
    void Start()
    {
        animator = GetComponent<Animator>();
        Maxhp = hp;
        PC = GetComponent<PlayerController>();
    }
    void Update()
    {
        playerslider.value = hp;
        playerslider2.value = (int)Stamina;
        if (Input.GetMouseButtonUp(0))
        {
            timeSinceMouseDown = 0f;

        }
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool("Punching", true);
        }
        else
        {
            animator.SetBool("Punching", false);
        }
        if (Input.GetMouseButtonDown(1) && Time.time - lastRangedAttackTime >= rangedAttackCooldown&& MP!=0)
        {
            animator.SetBool("Shooting", true);
            RangedAttack();
            Stamina=Stamina-20;
            lastRangedAttackTime = Time.time;
        }
        else
        {
            animator.SetBool("Shooting", false);
        }
        if(hp<=0)
        {
            SceneManager.LoadScene("Restart");
        }
        if (hp > 100) { hp = 100; }
        if (hp < 0) { hp = 0; }
        if (Stamina < 0) { Stamina = 0; }
        Stamina = Stamina + 0.01;
        if (Stamina > 100) { Stamina = 100; }
        if (Input.GetMouseButtonDown(0) && Attacking)
        {
            if (enemy != null && Time.time - lastMeleeAttackTime >= meleeAttackCooldown)
            {
                Attack(enemy);
                lastMeleeAttackTime = Time.time;
            }
        }
        HP = hp;
        MP = (int)Stamina;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            enemy = other.GetComponent<Enemy>();
            Attacking = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Attacking = false;
        }
    }
    private void Attack(Enemy enemy)
    {
        if (enemy != null)
        {
            enemy.Enemy_hp -= att;
        }
    }
    private void RangedAttack()
    {
        enemy=FindObjectOfType<Enemy>();
        if(enemy!=null)
        {
        if (projectilePrefab != null)
        {
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 direction = (enemy.transform.position - transform.position).normalized;
                rb.velocity = direction * projectileSpeed;
            }
        }   
        }
    }
    public void TakeDamage(int damage)
    {
        int finalDamage = Mathf.RoundToInt(damage * GameSettings.PlayerDamageTaken);
        hp -= finalDamage;
    }
}


