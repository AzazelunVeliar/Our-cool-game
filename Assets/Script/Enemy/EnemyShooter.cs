using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public enum ShooterState
{
    Peaceful,  // Мирный режим: не агрится, но убегает при низком HP
    Idle,      // Покой
    Aggro,     // Преследование игрока
    Attack,    // Стрельба
    Flee       // Бегство при малом HP
}

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyShooter : MonoBehaviour
{
    [Header("Refs")]
    public Transform player;
    public Animator animator;
    public GameObject projectilePrefab;
    public Transform shootPoint;

    [Header("Stats")]
    public float detectionRadius = 20f;
    public float shootRange = 15f;
    public float shootInterval = 1.5f;
    public int damage;
    public float fleeHpThreshold = 30f;
    public float fleeDistance = 10f;

    private NavMeshAgent agent;
    private ShooterState state;
    private float lastShootTime;
    private Enemy enemy;
     public GameObject Pluear;


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

        // Установка начального состояния в зависимости от режима
        if (GameSettings.PeacefulMode)
            TransitionTo(ShooterState.Peaceful);
        else
            TransitionTo(ShooterState.Idle);
    }

    void Update()
    {
        // Проверка режима мирности
        if (GameSettings.PeacefulMode)
        {
            if (state != ShooterState.Peaceful && state != ShooterState.Flee)
                TransitionTo(ShooterState.Peaceful);
        }
        else if (state == ShooterState.Peaceful)
        {
            TransitionTo(ShooterState.Idle);
        }

        switch (state)
        {
            case ShooterState.Peaceful:
                StatePeaceful();
                break;
            case ShooterState.Idle:
                StateIdle();
                break;
            case ShooterState.Aggro:
                StateAggro();
                break;
            case ShooterState.Attack:
                StateAttack();
                break;
            case ShooterState.Flee:
                StateFlee();
                break;
        }
            if (enemy.Enemy_hp <= 0){
            ScoreManager.Add(20);
            Debug.Log("помер");
            Destroy(gameObject);
        }
    }

    void TransitionTo(ShooterState newState)
    {
        if (state == newState) return;
        state = newState;
        Debug.Log($"[{name}] → ShooterState: {newState}");
    }

    void StatePeaceful()
    {
        // animator.Play("Idle");
        // В мирном режиме моб не агрится даже при близком игроке
        if (enemy.Enemy_hp <= fleeHpThreshold)
            TransitionTo(ShooterState.Flee);
    }

    void StateIdle()
    {
        animator.Play("Idle");
        float dist = Vector3.Distance(transform.position, player.position);
        if (dist <= detectionRadius && enemy.Enemy_hp > fleeHpThreshold)
            TransitionTo(ShooterState.Aggro);
        else if (enemy.Enemy_hp <= fleeHpThreshold)
            TransitionTo(ShooterState.Flee);
    }

    void StateAggro()
    {
        animator.Play("Walk");
        agent.isStopped = false;
        agent.SetDestination(player.position);

        float dist = Vector3.Distance(transform.position, player.position);
        if (dist <= shootRange)
            TransitionTo(ShooterState.Attack);
        else if (enemy.Enemy_hp <= fleeHpThreshold)
            TransitionTo(ShooterState.Flee);
    }

   void StateAttack()
{
    animator.Play("Punching");
    agent.isStopped = true;
    Vector3 directionToPlayer = player.position - transform.position;
    directionToPlayer.y = 0;
    if (directionToPlayer != Vector3.zero)
    {
        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.Euler(
            targetRotation.eulerAngles.x,
            transform.rotation.eulerAngles.y,
            targetRotation.eulerAngles.z
        );
    }

    if (Time.time - lastShootTime >= shootInterval)
    {
        ShootProjectile();
        lastShootTime = Time.time;
    }

    float dist = Vector3.Distance(transform.position, player.position);
    if (dist > shootRange)
        TransitionTo(ShooterState.Aggro);
    else if (enemy.Enemy_hp <= fleeHpThreshold)
        TransitionTo(ShooterState.Flee);
}
    void StateFlee()
    {
        animator.Play("Running");
        agent.isStopped = false;
        Vector3 dir = (transform.position - player.position).normalized;
        agent.SetDestination(transform.position + dir * fleeDistance);

        float dist = Vector3.Distance(transform.position, player.position);
        if (dist >= detectionRadius)
            TransitionTo(ShooterState.Idle);
    }
    void ShootProjectile()
    {
        if (projectilePrefab == null || shootPoint == null) return;
        var proj = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
        var projectile = proj.GetComponent<Projectile>();
        if (projectile != null)
        {
            projectile.damage = this.damage; // Передаем текущее значение урона врага
        }
    }
    public void TakeDamage(int damage)
    {
        if (enemy.Enemy_hp <= fleeHpThreshold && state != ShooterState.Flee)
            TransitionTo(ShooterState.Flee);
    }
}
