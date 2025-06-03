using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public enum EnemyState
{
    Peaceful,  // новый мирный режим
    Idle,
    Aggro,
    Attack,
    Flee
}

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyAI : MonoBehaviour
{
    [Header("Refs")]
    public Transform player;
    public Animator animator;
    public GameObject Pluear;

    [Header("Stats")]
    public int attackDamage;
    public float detectionRadius = 15f;
    public float attackRange = 2f;
    public float fleeHpThreshold = 30f;
    public float fleeDistance = 10f;
    public float attackCooldown = 2f;
    private Enemy enemy;
    //private ScoreManager Score;

    private NavMeshAgent agent;
    private EnemyState state;
    private float lastAttackTime;
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        if (animator == null)
            animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        Pluear = GameObject.FindGameObjectWithTag("Player");
        player=Pluear.transform;
        enemy=GetComponent<Enemy>();
        // стартовое состояние в зависимости от настройки
        if (GameSettings.PeacefulMode)
            TransitionTo(EnemyState.Peaceful);
        else
            TransitionTo(EnemyState.Idle);
    }

    void Update()
    {
        // динамическое переключение при изменении флага в меню
        if (GameSettings.PeacefulMode)
        {
            if (state != EnemyState.Peaceful && state != EnemyState.Flee)
                TransitionTo(EnemyState.Peaceful);
        }
        else if (state == EnemyState.Peaceful)
        {
            TransitionTo(EnemyState.Idle);
        }

        switch (state)
        {
            case EnemyState.Peaceful: StatePeaceful(); break;
            case EnemyState.Idle:     StateIdle();     break;
            case EnemyState.Aggro:    StateAggro();    break;
            case EnemyState.Attack:   StateAttack();   break;
            case EnemyState.Flee:     StateFlee();     break;
        }
        if (enemy.Enemy_hp <= 0){
            ScoreManager.Add(10);
            Debug.Log("помер");
            Destroy(gameObject);
        }
    }

    void TransitionTo(EnemyState newState)
    {
        if (state == newState) return;
        state = newState;
        Debug.Log($"[{name}] → EnemyState: {newState}");
    }

    void StatePeaceful()
    {
        animator.Play("Idle");
        // в мирном режиме моб не агрится, но если мало ХП — убегает
        if (enemy.Enemy_hp <= fleeHpThreshold)
            TransitionTo(EnemyState.Flee);
    }

    void StateIdle()
    {
        animator.Play("Idle");
        float dist = Vector3.Distance(transform.position, player.position);
        if (dist <= detectionRadius && enemy.Enemy_hp > fleeHpThreshold)
            TransitionTo(EnemyState.Aggro);
        else if (enemy.Enemy_hp <= fleeHpThreshold)
            TransitionTo(EnemyState.Flee);
    }

    void StateAggro()
    {
        animator.Play("walk");
        agent.isStopped = false;
        agent.SetDestination(player.position);

        float dist = Vector3.Distance(transform.position, player.position);
        if (dist <= attackRange) TransitionTo(EnemyState.Attack);
        else if (enemy.Enemy_hp <= fleeHpThreshold) TransitionTo(EnemyState.Flee);
    }

    void StateAttack()
    {
        agent.isStopped = true;
        transform.LookAt(player);
        animator.Play("Punching");

        if (Time.time - lastAttackTime >= attackCooldown)
        {
            player.GetComponent<Player>().TakeDamage(attackDamage);
            lastAttackTime = Time.time;
        }

        float dist = Vector3.Distance(transform.position, player.position);
        if (dist > attackRange) TransitionTo(EnemyState.Aggro);
        else if (enemy.Enemy_hp <= fleeHpThreshold) TransitionTo(EnemyState.Flee);
    }

    void StateFlee()
    {
        animator.Play("Running");
        agent.isStopped = false;
        Vector3 dir = (transform.position - player.position).normalized;
        agent.SetDestination(transform.position + dir * fleeDistance);

        float dist = Vector3.Distance(transform.position, player.position);
        if (dist >= detectionRadius) TransitionTo(EnemyState.Idle);
    }

    public void TakeDamage(int damage)
    {
        if (enemy.Enemy_hp <= fleeHpThreshold && state != EnemyState.Flee)
            TransitionTo(EnemyState.Flee);
    }
}
