using UnityEngine;
using UnityEngine.AI;

public enum BossState
{
    Idle,
    Aggro,
    Attack,
    HeavyAttack,
    Roar,
    Summon,
    Enrage
}

[RequireComponent(typeof(NavMeshAgent))]
public class BossAI: MonoBehaviour
{
    [Header("Refs")]
    public Animator animator;
    public Transform player;

    [Header("Stats")]
    public float detectionRadius = 20f;
    public float attackRange = 3f;
    public float heavyAttackRange = 5f;
    public int maxHp = 500;
    public int attackDamage = 30;
    public int heavyAttackDamage = 70;
    public float attackCooldown = 2f;
    public float heavyAttackCooldown = 8f;

    private NavMeshAgent agent;
    private int currentHp;
    private BossState state;
    private float lastAttackTime, lastHeavyAttackTime;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        if (animator == null)
            animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        currentHp = maxHp;
        TransitionTo(BossState.Idle);
    }

    void Update()
    {
        switch (state)
        {
            case BossState.Idle: StateIdle(); break;
            case BossState.Aggro: StateAggro(); break;
            case BossState.Attack: StateAttack(); break;
            case BossState.HeavyAttack: StateHeavyAttack(); break;
            case BossState.Roar: StateRoar(); break;
            case BossState.Summon: StateSummon(); break;
            case BossState.Enrage: StateEnrage(); break;
        }
    }

    void TransitionTo(BossState newState)
    {
        if (state == newState) return;
        state = newState;
        Debug.Log($"[{name}] → State: {newState}");
    }

    void StateIdle()
    {
        Debug.Log($"[{name}] Enter Idle");
        // проиграть Idle-анимацию
        // animator.Play("Idle");
        if (Vector3.Distance(transform.position, player.position) <= detectionRadius)
            TransitionTo(BossState.Aggro);
    }

    void StateAggro()
    {
        Debug.Log($"[{name}] Enter Aggro");
        // проиграть ходьбу/бег
        // animator.SetBool("isMoving", true);
        agent.isStopped = false;
        agent.SetDestination(player.position);

        float dist = Vector3.Distance(transform.position, player.position);
        if (dist <= attackRange && Time.time - lastAttackTime >= attackCooldown)
            TransitionTo(BossState.Attack);
        else if (dist <= heavyAttackRange && Time.time - lastHeavyAttackTime >= heavyAttackCooldown)
            TransitionTo(BossState.HeavyAttack);
        else if (currentHp <= maxHp * 0.5f)
            TransitionTo(BossState.Roar);
    }

    void StateAttack()
    {
        Debug.Log($"[{name}] Enter Attack");
        agent.isStopped = true;
        transform.LookAt(player);
        // запустить обычную атаку
        // animator.SetTrigger("Attack");
        // player.GetComponent<Player>().TakeDamage(attackDamage);
        lastAttackTime = Time.time;
        TransitionTo(BossState.Aggro);
    }

    void StateHeavyAttack()
    {
        Debug.Log($"[{name}] Enter HeavyAttack");
        agent.isStopped = true;
        transform.LookAt(player);
        // запустить сильную атаку
        // animator.SetTrigger("HeavyAttack");
        // player.GetComponent<Player>().TakeDamage(heavyAttackDamage);
        lastHeavyAttackTime = Time.time;
        TransitionTo(BossState.Aggro);
    }

    void StateRoar()
    {
        Debug.Log($"[{name}] Enter Roar");
        // проиграть рев
        // animator.SetTrigger("Roar");
        TransitionTo(BossState.Summon);
    }

    void StateSummon()
    {
        Debug.Log($"[{name}] Enter Summon");
        // проиграть призыв помощников
        // animator.SetTrigger("Summon");
        // Instantiate(minionPrefab, transform.position, Quaternion.identity);
        TransitionTo(BossState.Enrage);
    }

    void StateEnrage()
    {
        Debug.Log($"[{name}] Enter Enrage");
        // проиграть ярость / баф
        // animator.SetTrigger("Enrage");
        // повысить параметры
        TransitionTo(BossState.Aggro);
    }

    public void TakeDamage(int dmg)
    {
        currentHp = Mathf.Max(0, currentHp - dmg);
        if (currentHp == 0)
        {
            // проиграть смерть
            // animator.SetTrigger("Die");
            Destroy(gameObject, 1f);
        }
        else if (state != BossState.Enrage && currentHp <= maxHp * 0.3f)
        {
            TransitionTo(BossState.HeavyAttack);
        }
    }
}
