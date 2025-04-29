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
    private NavMeshAgent agent;
    public float attackRadius = 2f;
    private bool canAttack = true;
    private float attackCooldown = 3f;
    private Player player;
    public float jumpForce;
    private Rigidbody rb;
    private Animator animator;
    public Slider enemyslider;
    public string enemyID;
    public Slider healthSlider;

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
    public EnemyData GetSaveData()
{
    return new EnemyData
    {
        enemyID = this.enemyID,
        position = transform.position,
        hp = Enemy_hp
    };
}

public void LoadData(EnemyData data)
{
    transform.position = data.position;
    Enemy_hp = data.hp;
    UpdateHealthUI();
}
    private void UpdateHealthUI()
    {
        if (healthSlider != null)
            healthSlider.value = (float)Enemy_hp / Enemy_maxhp;
    }

}
