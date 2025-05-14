
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public enum EnemyState
{
    Idle,       // Покой
    Aggro,      // Агрессия (движение к игроку)
    Attack,     // Атака (ближний бой)
    Flee        // Бегство (при малом HP)
}

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    [Header("Refs")]
    public Transform player;
    public Slider healthBar;
    public Animator animator;

    [Header("Stats")]
    public int maxHp = 100;
    public int attackDamage = 20;
    public float detectionRadius = 15f;
    public float attackRange = 2f;
    public float fleeHpThreshold = 30f;
    public float fleeDistance = 10f;
    public float attackCooldown = 2f;

    private NavMeshAgent agent;
    private int currentHp;
    private EnemyState state;
    private float lastAttackTime;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentHp = maxHp;
        healthBar.maxValue = maxHp;
        healthBar.value = currentHp;
        TransitionTo(EnemyState.Idle);
    }

    void Update()
    {
        switch (state)
        {
            case EnemyState.Idle: StateIdle(); break;
            case EnemyState.Aggro: StateAggro(); break;
            case EnemyState.Attack: StateAttack(); break;
            case EnemyState.Flee: StateFlee(); break;
        }
    }

    #region States
    void StateIdle()
    {
        animator.Play("Idle");
        float dist = Vector3.Distance(transform.position, player.position);
        if (dist <= detectionRadius && currentHp > fleeHpThreshold)
            TransitionTo(EnemyState.Aggro);
        else if (currentHp <= fleeHpThreshold)
            TransitionTo(EnemyState.Flee);
    }

    void StateAggro()
    {
        animator.Play("Running");
        agent.isStopped = false;
        agent.SetDestination(player.position);

        float dist = Vector3.Distance(transform.position, player.position);
        if (dist <= attackRange)
            TransitionTo(EnemyState.Attack);
        else if (currentHp <= fleeHpThreshold)
            TransitionTo(EnemyState.Flee);
    }

    void StateAttack()
    {
        agent.isStopped = true;
        transform.LookAt(player);
        animator.Play("Punching");

        if (Time.time - lastAttackTime >= attackCooldown)
        {
            // наносим урон
            var pl = player.GetComponent<Player>();
            if (pl != null) pl.hp -= attackDamage;
            lastAttackTime = Time.time;
        }

        float dist = Vector3.Distance(transform.position, player.position);
        if (dist > attackRange)
            TransitionTo(EnemyState.Aggro);
        else if (currentHp <= fleeHpThreshold)
            TransitionTo(EnemyState.Flee);
    }

    void StateFlee()
    {
        animator.Play("Running");
        agent.isStopped = false;
        Vector3 dir = (transform.position - player.position).normalized;
        agent.SetDestination(transform.position + dir * fleeDistance);

        float dist = Vector3.Distance(transform.position, player.position);
        if (dist >= detectionRadius)
            TransitionTo(EnemyState.Idle);
    }
    #endregion

    void TransitionTo(EnemyState newState)
    {
        state = newState;
        // здесь можно вызвать OnEnter, OnExit-подход, если нужно
    }

    // Внешний метод для получения урона
    public void TakeDamage(int dmg)
    {
        currentHp = Mathf.Max(0, currentHp - dmg);
        healthBar.value = currentHp;
        if (currentHp == 0)
            Destroy(gameObject);
        else if (state != EnemyState.Flee && currentHp <= fleeHpThreshold)
            TransitionTo(EnemyState.Flee);
    }
}
