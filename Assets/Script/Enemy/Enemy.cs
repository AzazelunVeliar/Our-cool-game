using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string Figure_type_enemy;
    public string Figure_Name;
    public Transform player_position;
    public int Enemy_hp;
    public int Enemy_maxhp;
    public bool White_color;
    public bool Black_color;
    public int Enemy_attack;
    public float maxStamina = 100f;
    public float currentStamina;
    public float staminaRecoveryRate = 10f; 
    public float staminaRecoveryDelay = 2f; 
    public float dashDistance = 10f; 
    public float dashSpeed = 50f;
    private UnityEngine.AI.NavMeshAgent agent;
    private bool canAttack = true;
    private float attackCooldown = 12f;
    public double Percentage;
    private bool isDashing = false;
    private Vector3 dashDirection;
    private float dashStartTime;
    private Player player;
    public float jumpForce;
    private Rigidbody rb;
    public bool canJump;
    private Animator animator;
    [SerializeField] GameObject arrow;
    public int numder_of_shots;
    public int max_numder_of_shots;
    [SerializeField] MonoBehaviour script;
    [SerializeField] GameObject after_deth;
    [SerializeField] GameObject bow;


    void Start()
    {
        animator = GetComponent<Animator>();
        max_numder_of_shots = numder_of_shots;
        player = FindFirstObjectByType<Player>();
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        //currentStamina = maxStamina;
        rb = GetComponent<Rigidbody>();

    }

    void Update()
    {
        if (Enemy_hp <= 0)
        {
            GameObject myObject = Instantiate(after_deth, transform.position, transform.rotation);
        }
        if ((player.is_it_black && Black_color) || (player.is_it_white && White_color))
        {
            script.enabled = false;
            agent.enabled = false;
            rb.isKinematic = true;
        }
        if ((player.is_it_black && White_color) || (player.is_it_white && Black_color))
        {
            script.enabled = true;
            agent.enabled = true;
            rb.isKinematic = false;
        }
        if (!canAttack)
        {
            attackCooldown -= Time.deltaTime;
            if (attackCooldown <= 0f)
            {
                canAttack = true;
                attackCooldown = 12f; 
            }
        }
        if (Figure_type_enemy == "white_pawn" || Figure_type_enemy == "wp")
        {
            White_color = true;
            Black_color = false;
            if (!isDashing)
            {
                if (currentStamina >= maxStamina)
                {
                    dashDirection = (player_position.position - transform.position).normalized;
                    isDashing = true;
                    dashStartTime = Time.time;
                    currentStamina = 0f;
                }
                else
                {
                    currentStamina += staminaRecoveryRate * Time.deltaTime;
                }
            }
            else
            {
                if (Time.time - dashStartTime >= dashDistance / dashSpeed)
                {
                    isDashing = false;
                }
                else
                {
                    agent.isStopped = false;
                    agent.velocity = dashDirection * dashSpeed;
                }
            }
        }

        if (Figure_type_enemy == "white_king" || Figure_type_enemy == "wk")
        {
            White_color = true;
            Black_color = false;
        }
        if (Figure_type_enemy == "white_queen" || Figure_type_enemy == "wq")
        {
            White_color = true;
            Black_color = false;
        }
        if (Figure_type_enemy == "white_horse" || Figure_type_enemy == "wh")
        {
            White_color = true;
            Black_color = false;

            if (currentStamina == maxStamina)
            {
                rb.isKinematic = true;
                agent.enabled = false;
                script.enabled = false;
                rb.isKinematic = false;
                bow.SetActive(true);
                Jump();
                currentStamina = 0;
            }
            if (currentStamina >= 600)
            {
                agent.enabled = true;
                script.enabled = true;
                bow.SetActive(false);
            }
            if (currentStamina == 250)
            {
                while (numder_of_shots != 0)
                {
                    if ((player.is_it_black && White_color) || (player.is_it_white && Black_color))
                    {
                        GameObject myObject = Instantiate(arrow, transform.position, transform.rotation);
                        numder_of_shots--;
                        rb.isKinematic = false;
                    }
                }
                numder_of_shots = max_numder_of_shots;
            }
            currentStamina++;
        }
        if (Figure_type_enemy == "white_rook" || Figure_type_enemy == "wr")
        {
            White_color = true;
            Black_color = false;
        }
        if (Figure_type_enemy == "white_bisp" || Figure_type_enemy == "wb")
        {
            White_color = true;
            Black_color = false;
        }
        if (Figure_type_enemy == "black_pawn" || Figure_type_enemy == "bp")
        {
            Black_color = true;
            White_color = false;
            if (!isDashing)
            {
                if (currentStamina >= maxStamina)
                {

                    dashDirection = (player_position.position - transform.position).normalized;
                    isDashing = true;
                    dashStartTime = Time.time;
                    currentStamina = 0f;
                }
                else
                {
                    currentStamina += staminaRecoveryRate * Time.deltaTime;
                }
            }
            else
            {
                if (Time.time - dashStartTime >= dashDistance / dashSpeed)
                {
                    isDashing = false;
                }
                else
                {
                    agent.isStopped = false;
                    agent.velocity = dashDirection * dashSpeed;
                }
            }
        }

        if (Figure_type_enemy == "black_king" || Figure_type_enemy == "bk")
        {
            Black_color = true;
            White_color = false;
        }
        if (Figure_type_enemy == "black_queen" || Figure_type_enemy == "bq")
        {
            Black_color = true;
            White_color = false;
        }
        if (Figure_type_enemy == "black_horse" || Figure_type_enemy == "bh")
        {
            Black_color = true;
            White_color = false;
            if (currentStamina == maxStamina)
            {
                rb.isKinematic = true;
                agent.enabled = false;
                script.enabled = false;
                rb.isKinematic = false;
                bow.SetActive(true);
                Jump();
                currentStamina = 0;
            }
            if (currentStamina >= 600)
            {
                agent.enabled = true;
                script.enabled = true;
                bow.SetActive(false);
            }
            if (currentStamina == 250)
            {
                while (numder_of_shots != 0)
                {
                    if ((player.is_it_black && White_color) || (player.is_it_white && Black_color))
                    {
                        GameObject myObject = Instantiate(arrow, transform.position, transform.rotation);
                        numder_of_shots--;
                        rb.isKinematic = false;
                    }
                }
                numder_of_shots = max_numder_of_shots;
            }
            currentStamina++;
        }
        if (Figure_type_enemy == "white_rook" || Figure_type_enemy == "wr")
        {
            White_color = true;
            Black_color = false;
        }
        if (Figure_type_enemy == "white_bisp" || Figure_type_enemy == "wb")
        {
            White_color = true;
            Black_color = false;
        }
        if (Figure_type_enemy == "black_pawn" || Figure_type_enemy == "bp")
        {
            Black_color = true;
            White_color = false;
            if (!isDashing)
            {
                if (currentStamina >= maxStamina)
                {

                    dashDirection = (player_position.position - transform.position).normalized;
                    isDashing = true;
                    dashStartTime = Time.time;
                    currentStamina = 0f;
                }
                else
                {
                    currentStamina += staminaRecoveryRate * Time.deltaTime;
                }
            }
            else
            {
                if (Time.time - dashStartTime >= dashDistance / dashSpeed)
                {
                    isDashing = false;
                }
                else
                {
                    agent.isStopped = false;
                    agent.velocity = dashDirection * dashSpeed;
                }
            }
        }

        if (Figure_type_enemy == "black_king" || Figure_type_enemy == "bk")
        {
            Black_color = true;
            White_color = false;
        }
        if (Figure_type_enemy == "black_queen" || Figure_type_enemy == "bq")
        {
            Black_color = true;
            White_color = false;
        }
        if (Figure_type_enemy == "black_horse" || Figure_type_enemy == "bh")
        {
            Black_color = true;
            White_color = false;
            if (currentStamina == maxStamina)
            {
                rb.isKinematic = true;
                agent.enabled = false;
                script.enabled = false;
                rb.isKinematic = false;
                bow.SetActive(true);
                Jump();
                currentStamina = 0;
            }
            if (currentStamina >= 600)
            {
                agent.enabled = true;
                script.enabled = true;
                bow.SetActive(false);
            }
            if (currentStamina == 250)
            {
                while (numder_of_shots != 0)
                {
                    rb.isKinematic = true;
                    if ((player.is_it_black && White_color) || (player.is_it_white && Black_color))
                    {
                        GameObject myObject = Instantiate(arrow, transform.position, transform.rotation);
                        numder_of_shots--;
                        rb.isKinematic = false;
                    }

                }
                numder_of_shots = max_numder_of_shots;
            }
            currentStamina++;
        }
        if (Figure_type_enemy == "black_rook" || Figure_type_enemy == "br")
        {
            Black_color = true;
            White_color = false;
        }
        if (Figure_type_enemy == "black_bisp" || Figure_type_enemy == "bb")
        {
            Black_color = true;
            White_color = false;
        }
        if (Enemy_hp <= 0)
        {
            if (Figure_type_enemy == "white_pawn" || Figure_type_enemy == "wp")
            {
                player.white_pawn_percentage += Percentage;
                Destroy(gameObject);
            }
            if (Figure_type_enemy == "white_king" || Figure_type_enemy == "wk")
            {
                player.white_king_percentage += Percentage;
                Destroy(gameObject);
            }
            if (Figure_type_enemy == "white_queen" || Figure_type_enemy == "wq")
            {
                player.white_queen_percentage += Percentage;
                Destroy(gameObject);
            }
            if (Figure_type_enemy == "white_horse" || Figure_type_enemy == "wh")
            {
                player.white_horse_percentage += Percentage;
                Destroy(gameObject);
            }
            if (Figure_type_enemy == "white_rook" || Figure_type_enemy == "wr")
            {
                player.white_rook_percentage += Percentage;
                Destroy(gameObject);
            }
            if (Figure_type_enemy == "white_bisp" || Figure_type_enemy == "wb")
            {
                player.white_bispo_percentage += Percentage;
                Destroy(gameObject);
            }
            if (Figure_type_enemy == "black_pawn" || Figure_type_enemy == "bp")
            {
                player.black_pawn_percentage += Percentage;
                Destroy(gameObject);
            }
            if (Figure_type_enemy == "black_king" || Figure_type_enemy == "bk")
            {
                player.black_king_percentage += Percentage;
                Destroy(gameObject);
            }
            if (Figure_type_enemy == "black_queen" || Figure_type_enemy == "bq")
            {
                player.black_queen_percentage += Percentage;
                Destroy(gameObject);
            }
            if (Figure_type_enemy == "black_horse" || Figure_type_enemy == "bh")
            {
                player.black_horse_percentage += Percentage;
                Destroy(gameObject);
            }
            if (Figure_type_enemy == "black_rook" || Figure_type_enemy == "br")
            {
                player.black_rook_percentage += Percentage;
                Destroy(gameObject);
            }
            if (Figure_type_enemy == "black_bisp" || Figure_type_enemy == "bb")
            {
                player.black_bispo_percentage += Percentage;
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && canAttack)
        {
            if ((player.is_it_black && White_color) || (player.is_it_white && Black_color))
            {
                animator.SetTrigger("Punching");
                player.hp -= Enemy_attack;
                canAttack = false; // установить задержку перед следующей атакой
            }
        }
        animator.ResetTrigger("Punching");
    }
    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

}

