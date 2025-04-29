using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    public float detectionRadius = 50f;
    public float attackRange = 5f;
    public float moveSpeed = 15f;
    public float attackRadius = 5f;
    public int Enemy_attack = 10;
    public Transform player;
    public Animator animator;
    public bool canAttack = true;
    private float attackCooldown = 3f;

    private void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= detectionRadius)
            {
                if (distanceToPlayer <= attackRange)
                {
                    PerformAttack();
                }
                else
                {
                    MoveTowardsPlayer();
                }
            }
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * moveSpeed);

        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    private void PerformAttack()
    {
        if (canAttack && Vector3.Distance(transform.position, player.position) <= attackRadius)
        {
            canAttack = false;
            animator.SetTrigger("Punching");
            player.GetComponent<Player>().hp -= Enemy_attack;

            StartCoroutine(ResetAttackCooldown());
        }
    }

    private IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }
}
