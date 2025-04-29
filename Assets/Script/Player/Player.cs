using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using UnityEngine.UI;
[System.Serializable]
public class PlayerSaveData
{
    public Vector3 position;
    public int hp;
}
public class Player : MonoBehaviour
{
    public int Maxhp;
    public double Stamina;
    public int hp;
    public int att;
    public int matt;
    private Animator animator;
    private Enemy enemy;
    private PlayerController PC;
    private bool Attacking;
    private float meleeAttackCooldown = 1.2f;
    private float lastMeleeAttackTime = 0f;
    public Slider playerslider;
    public GameObject projectilePrefab;
    public float projectileSpeed = 10f;
    private float rangedAttackCooldown = 2f;
    private float lastRangedAttackTime = 0f;
    public Slider healthSlider;

    void Start()
    {
        animator = GetComponent<Animator>();
        PC = GetComponent<PlayerController>();
    }
    void Update()
    {
        UpdateHealthBar();
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool("Punching", true);
            if (Attacking && Time.time - lastMeleeAttackTime >= meleeAttackCooldown)
            {
                Attack(enemy);
                lastMeleeAttackTime = Time.time;
            }
        }
        else
        {
            animator.SetBool("Punching", false);
        }
        if (Input.GetMouseButtonDown(1) && Time.time - lastRangedAttackTime >= rangedAttackCooldown) // Added cooldown check
        {
            animator.SetBool("Shooting", true);
            RangedAttack();
            lastRangedAttackTime = Time.time; // Update the last attack time
        }
        else
        {
            animator.SetBool("Shooting", false);
        }
        if (hp <= 0)
        {
            SceneManager.LoadScene("Restart");
        }
        if (hp > 100) { hp = 100; }
        if (hp < 0) { hp = 0; }
        if (Stamina < 0) { Stamina = 0; }
        Stamina = Stamina + 0.01;
        if (Stamina > 100) { Stamina = 100; }
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
    void UpdateHealthBar()
    {
        playerslider.value = hp;
    }
    private void RangedAttack()
    {
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
    public PlayerSaveData GetSaveData()
    {
        return new PlayerSaveData
        {
            position = transform.position,
            hp = this.hp,
        };
    }

    public void LoadData(PlayerSaveData data)
    {
        transform.position = data.position;
        hp = data.hp;
        UpdateHealthUI();
    }
        private void UpdateHealthUI()
    {
        if (healthSlider != null)
            healthSlider.value = (float)hp / Maxhp;
    }

}