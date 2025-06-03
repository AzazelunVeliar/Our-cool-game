using UnityEngine;
using UnityEngine.AI;

public enum BossState
{
    Peaceful,    // мирный режим — не агрится, пока не ударят
    Idle,
    Aggro,
    Attack,
    HeavyAttack,
    Roar,
    Summon,
    Enrage
}

[RequireComponent(typeof(NavMeshAgent))]
public class BossAI : MonoBehaviour
{
    [Header("Refs")]
    public Animator animator;
    public Transform player;
    public GameObject Pluear;
    public GameObject IceType;
    public GameObject FireType;
    public GameObject EarthType;
    public GameObject EtherType;
    
    [Header("Stats")]
    public float detectionRadius = 20f;
    public float attackRange = 3f;
    public float heavyAttackRange = 5f;
    public int attackDamage;
    public int heavyAttackDamage;
    public float attackCooldown = 2f;
    public float heavyAttackCooldown = 8f;

    // Добавлено для работы с элементами
    public Element CurrentElement;
    private NavMeshAgent agent;
    private BossState state;
    private bool hasBeenHit;
    private float lastAttackTime, lastHeavyAttackTime;
    private Enemy enemy;
    private bool ror = true;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        if (animator == null)
            animator = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        Debug.Log($"Boss element on start: {CurrentElement}");
        Pluear = GameObject.FindGameObjectWithTag("Player");
        player = Pluear.transform;
        enemy = GetComponent<Enemy>();
        hasBeenHit = false;

        // Настройка элемента
        SetupElementEffects();

        // стартовое состояние
        if (GameSettings.PeacefulMode)
            TransitionTo(BossState.Peaceful);
        else
            TransitionTo(BossState.Idle);
    }

    void SetupElementEffects()
    {
        // Деактивируем все эффекты
        if (IceType != null) IceType.SetActive(false);
        if (FireType != null) FireType.SetActive(false);
        if (EarthType != null) EarthType.SetActive(false);
        if (EtherType != null) EtherType.SetActive(false);

        // Устанавливаем множитель урона и активируем нужный эффект
        switch (CurrentElement)
        {
            case Element.Ice:
                if (IceType != null) IceType.SetActive(true);
                break;
            case Element.Fire:
                if (FireType != null) FireType.SetActive(true);
                break;
            case Element.Earth:
                if (EarthType != null) EarthType.SetActive(true);
                break;
            case Element.Ether:
                if (EtherType != null) EtherType.SetActive(true);
                break;
        }
    }

    void Update()
    {
        if (GameSettings.PeacefulMode && !hasBeenHit)
        {
            if (state != BossState.Peaceful)
                TransitionTo(BossState.Peaceful);
        }
        else if (!GameSettings.PeacefulMode && state == BossState.Peaceful)
        {
            TransitionTo(BossState.Idle);
        }

        switch (state)
        {
            case BossState.Peaceful:    StatePeaceful();    break;
            case BossState.Idle:        StateIdle();        break;
            case BossState.Aggro:       StateAggro();       break;
            case BossState.Attack:      StateAttack();      break;
            case BossState.HeavyAttack: StateHeavyAttack(); break;
            case BossState.Roar:        StateRoar();        break;
            case BossState.Summon:      StateSummon();      break;
            case BossState.Enrage:      StateEnrage();      break;
        }

        if (enemy.Enemy_hp <= 0)
        {
            ScoreManager.Add(30);
            Debug.Log("Boss defeated");
            Destroy(gameObject);
        }
    }

    void TransitionTo(BossState newState)
    {
        if (state == newState) return;
        state = newState;
        Debug.Log($"[{name}] → BossState: {newState}");
    }

    void StatePeaceful()
    {
        // Мирный режим - бездействие
    }

    void StateIdle()
    {
        animator.Play("Idle");
        if (Vector3.Distance(transform.position, player.position) <= detectionRadius)
            TransitionTo(BossState.Aggro);
    }

    void StateAggro()
    {
        animator.SetTrigger("Walk");
        agent.isStopped = false;
        agent.SetDestination(player.position);
        
        float dist = Vector3.Distance(transform.position, player.position);
        if (dist <= attackRange && Time.time - lastAttackTime >= attackCooldown)
            TransitionTo(BossState.Attack);
        else if (dist <= heavyAttackRange && Time.time - lastHeavyAttackTime >= heavyAttackCooldown)
            TransitionTo(BossState.HeavyAttack);
            
        if (ror && enemy.Enemy_hp <= enemy.Enemy_maxhp * 0.5f)
            TransitionTo(BossState.Roar);
    }

    void StateAttack()
    {
        agent.isStopped = true;
        transform.LookAt(player);
        animator.SetTrigger("Attack");
        player.GetComponent<Player>().TakeDamage(attackDamage);
        lastAttackTime = Time.time;
        TransitionTo(BossState.Aggro);
    }

    void StateHeavyAttack()
    {
        agent.isStopped = true;
        transform.LookAt(player);
        animator.SetTrigger("HeavyAttack");
        player.GetComponent<Player>().TakeDamage(heavyAttackDamage);
        lastHeavyAttackTime = Time.time;
        TransitionTo(BossState.Aggro);
    }

    void StateRoar()
    {
        animator.SetTrigger("Roar");
        ror = false;
        TransitionTo(BossState.Summon);
    }

    void StateSummon()
    {
        TransitionTo(BossState.Enrage);
    }

    void StateEnrage()
    {
        animator.Play("Enrage");
        TransitionTo(BossState.Aggro);
    }

    public void TakeDamage(int damage)
    {
        if (state == BossState.Peaceful)
        {
            TransitionTo(BossState.Aggro);
            return;
        }
        else if (enemy.Enemy_hp <= enemy.Enemy_maxhp * 0.3f)
        {
            TransitionTo(BossState.HeavyAttack);
        }
    }
}