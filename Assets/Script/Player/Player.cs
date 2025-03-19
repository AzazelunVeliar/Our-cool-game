using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public double Maxhp;
    public double Stamina;
    public double hp;
    public int att;
    private Animator animator;
    public string Figure_type;
    public bool is_it_black;
    public bool is_it_white;
    public double white_pawn_percentage;
    public double white_king_percentage;
    public double white_queen_percentage;
    public double white_horse_percentage;
    public double white_rook_percentage;
    public double white_bispo_percentage;
    public double black_pawn_percentage;
    public double black_king_percentage;
    public double black_queen_percentage;
    public double black_horse_percentage;
    public double black_rook_percentage;
    public double black_bispo_percentage;
    private Enemy enemy;
    private PlayerController PC;
    public double point;
    public Transform arrowSpawnPoint;
    public float force;
    [SerializeField] GameObject arrow;
    private bool Attacking;
    public Transform cameraTransform;
    private float timeSinceMouseDown = 0f;
    private float maxForce = 300f;
    private float lastShotTime = 0f;
    private float shotDelay = 2.5f;
    [SerializeField] GameObject bow;
    [SerializeField] GameObject aim;
    void Start()
    {
        animator = GetComponent<Animator>();
        Maxhp = hp;
        //is_it_white = true;
        PC = GetComponent<PlayerController>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Figure_type == "horse")
        {
            timeSinceMouseDown = 0f;
            bow.SetActive(true);
            aim.SetActive(true);
        }
        else if (Input.GetMouseButton(0) && Figure_type == "horse")
        {
            timeSinceMouseDown += Time.deltaTime;
            if (Time.time - lastShotTime >= shotDelay)
            {
                ShootArrow();
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            timeSinceMouseDown = 0f;
            bow.SetActive(false);
            aim.SetActive(false);
        }
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool("Punching", true);
        }
        else
        {
            animator.SetBool("Punching", false);
        }
        if(hp<=0)
        {
            SceneManager.LoadScene("Main Menu");
        }
        if (hp > 100) { hp = 100; }
        if (hp < 0) { hp = 0; }
        if (Stamina < 0) { Stamina = 0; }
        Stamina = Stamina + 0.01;
        if (Stamina > 100) { Stamina = 100; }
        //if(is_it_black==true){is_it_white = false;}
        //if (is_it_white == true) { is_it_black = false;}
        if (Input.GetMouseButtonDown(0) && Attacking)
        {
            if (enemy != null)
            {
                Attack(enemy);
            }
        }
        if (Figure_type == "pawn" & white_pawn_percentage != 0 & is_it_white == true)
        {
            Skill(Figure_type);
            if (Figure_type == "pawn" & white_pawn_percentage > 50)
            {
                Skill2(Figure_type);
            }
        }
        if (Figure_type == "king" & white_king_percentage != 0 & is_it_white == true)
        {
            Skill(Figure_type);
            if (Figure_type == "king" & white_king_percentage > 50)
            {
                Skill2(Figure_type);
            }
        }
        if (Figure_type == "queen" & white_queen_percentage != 0 & is_it_white == true)
        {
            Skill(Figure_type);
            if (Figure_type == "queen" & white_queen_percentage > 50)
            {
                Skill2(Figure_type);
            }
        }
        if (Figure_type == "horse" & white_horse_percentage != 0 & is_it_white == true)
        {
            Skill(Figure_type);
            if (Figure_type == "horse" & white_horse_percentage > 50)
            {
                Skill2(Figure_type);
            }
        }
        if (Figure_type == "rook" & white_rook_percentage != 0 & is_it_white == true)
        {
            Skill(Figure_type);
            if (Figure_type == "rook" & white_rook_percentage > 50)
            {
                Skill2(Figure_type);
            }
        }
        if (Figure_type == "bispo" & white_bispo_percentage != 0 & is_it_white == true)
        {
            Skill(Figure_type);
            if (Figure_type == "bispo" & white_bispo_percentage > 50)
            {
                Skill2(Figure_type);
            }
        }
        if (Figure_type == "pawn" & black_pawn_percentage != 0 & is_it_black == true)
        {
            Skill(Figure_type);
            if (Figure_type == "pawn" & black_pawn_percentage > 50)
            {
                Skill2(Figure_type);
            }
        }
        if (Figure_type == "king" & black_king_percentage != 0 & is_it_black == true)
        {
            Skill(Figure_type);
            if (Figure_type == "king" & black_king_percentage > 50)
            {
                Skill2(Figure_type);
            }
        }
        if (Figure_type == "queen" & black_queen_percentage != 0 & is_it_black == true)
        {
            Skill(Figure_type);
            if (Figure_type == "queen" & black_queen_percentage > 50)
            {
                Skill2(Figure_type);
            }
        }
        if (Figure_type == "horse" & black_horse_percentage != 0 & is_it_black == true)
        {
            Skill(Figure_type);
            if (Figure_type == "horse" & black_horse_percentage > 50)
            {
                Skill2(Figure_type);
            }
        }
        if (Figure_type == "rook" & black_rook_percentage != 0 & is_it_black == true)
        {
            Skill(Figure_type);
            if (Figure_type == "rook" & black_rook_percentage > 50)
            {
                Skill2(Figure_type);
            }
        }
        if (Figure_type == "bispo" & black_bispo_percentage != 0 & is_it_black == true)
        {
            Skill(Figure_type);
            if (Figure_type == "bispo" & black_bispo_percentage > 50)
            {
                Skill2(Figure_type);
            }
        }
        if (white_rook_percentage > 100)
        {
            point = white_rook_percentage - 100;
            white_rook_percentage = 100;
        }
        if (white_bispo_percentage > 100)
        {
            point = white_bispo_percentage - 100;
            white_bispo_percentage = 100;
        }
        if (white_horse_percentage > 100)
        {
            point = white_horse_percentage - 100;
            white_horse_percentage = 100;
        }
        if (white_queen_percentage > 100)
        {
            point = white_queen_percentage - 100;
            white_queen_percentage = 100;
        }
        if (white_pawn_percentage > 100)
        {
            point = white_pawn_percentage - 100;
            white_pawn_percentage = 100;
        }
        if (white_king_percentage > 100)
        {
            point = white_king_percentage - 100;
            white_king_percentage = 100;
        }
        if (black_pawn_percentage > 100)
        {
            point = black_pawn_percentage - 100;
            black_pawn_percentage = 100;
        }
        if (black_bispo_percentage > 100)
        {
            point = black_bispo_percentage - 100;
            black_bispo_percentage = 100;
        }
        if (black_horse_percentage > 100)
        {
            point = black_horse_percentage - 100;
            black_horse_percentage = 100;
        }
        if (black_king_percentage > 100)
        {
            point = black_king_percentage - 100;
            black_king_percentage = 100;
        }
        if (black_queen_percentage > 100)
        {
            point = black_queen_percentage - 100;
            black_queen_percentage = 100;
        }
        if (black_rook_percentage > 100)
        {
            point = black_rook_percentage - 100;
            black_rook_percentage = 100;
        }
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
    private void Skill(string Figure_type)
    {
        if (Figure_type == "pawn")
        {
            PC.speedMultiplier = 10f;
            hp = hp + 0.00001;
            PC.jumpHeight = 200f;
            PC.moveSpeed = 10;
        }
        if (Figure_type == "king")
        {
            hp = hp + 0.0040;
        }
        if (Figure_type == "queen")
        {
            hp = hp + 0.0030;
        }
        if (Figure_type == "horse")
        {
            PC.jumpHeight = 2000f;
            PC.speedMultiplier = 2f;
            hp = hp + 0.0010;
            PC.moveSpeed = 10;
        }
        if (Figure_type == "rook")
        {
            hp = hp + 0.0020;
        }
        if (Figure_type == "bispo")
        {
            PC.speedMultiplier = 2f;
            hp = hp + 0.0010;
            PC.jumpHeight = 200f;
            PC.moveSpeed = 15;
        }
    }
    private void Skill2(string Figure_type)
    {
        if (Figure_type == "pawn")
        {
            if (is_it_white == true)
            {
                PC.speedMultiplier = 50;
                hp = hp + 0.00001 * white_pawn_percentage % 10;
            }
            if (is_it_black == true)
            {
                PC.speedMultiplier = 50;
                hp = hp + 0.00001 * black_pawn_percentage % 10;
            }
        }
        if (Figure_type == "king")
        {
            if (is_it_white == true)
            {
                hp = hp + 0.00007 * white_king_percentage % 10;
            }
            if (is_it_black == true)
            {
                hp = hp + 0.00007 * black_king_percentage % 10;
            }
        }
        if (Figure_type == "queen")
        {
            if (is_it_white == true)
            {
                hp = hp + 0.00006 * white_queen_percentage % 10;
            }
            if (is_it_black == true)
            {
                hp = hp + 0.00006 * black_queen_percentage % 10;
            }
        }
        if (Figure_type == "horse")
        {
            if (is_it_white == true)
            {
                PC.jumpHeight = 4000f;
                hp = hp + 0.00002 * white_horse_percentage % 10;
            }
            if (is_it_black == true)
            {
                PC.jumpHeight = 4000f;
                hp = hp + 0.00002 * black_horse_percentage % 10;
            }
        }
        if (Figure_type == "rook")
        {
            if (is_it_white == true)
            {
                hp = hp + 0.00005 * white_rook_percentage % 10;
            }
            if (is_it_black == true)
            {
                hp = hp + 0.00005 * black_rook_percentage % 10;
            }

        }
        if (Figure_type == "bispo")
        {
            if (is_it_white == true)
            {
                PC.moveSpeed = 25;
                hp = hp + 0.00004 * white_bispo_percentage % 10;
            }
            if (is_it_black == true)
            {
                PC.moveSpeed = 25;
                hp = hp + 0.00004 * black_bispo_percentage % 10;
            }

        }
    }
    private void ShootArrow()
    {
        if (Time.time - lastShotTime >= shotDelay)
        {
            float currentForce = Mathf.Lerp(0f, maxForce, timeSinceMouseDown);
            GameObject arro = Instantiate(arrow, arrowSpawnPoint.position, Quaternion.LookRotation(cameraTransform.forward));
            Rigidbody arrowRigidbody = arro.GetComponent<Rigidbody>();
            arrowRigidbody.AddForce(cameraTransform.forward * currentForce, ForceMode.Impulse);
            lastShotTime = Time.time;
            timeSinceMouseDown = 0f;
        }
    }
}


