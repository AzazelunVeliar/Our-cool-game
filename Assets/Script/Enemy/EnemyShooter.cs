using UnityEngine;
using System.Collections;

public class EnemyShooter : MonoBehaviour
{
    public float detectionRadius = 50f;
    public float moveSpeed = 15f;

    public float rangedAttackMinDistance = 10f;
    public float rangedAttackMaxDistance = 15f;
    public GameObject projectilePrefab;
    public float projectileSpeed = 20f;
    public float rangedAttackCooldown = 3f;

    public Transform player;
    public Transform spawnPoint;
    public Animator animator;

    private bool canAttack = true;

    private void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRadius)
        {
            if (distanceToPlayer >= rangedAttackMinDistance && distanceToPlayer <= rangedAttackMaxDistance)
            {
                PerformRangedAttack();
            }
            else
            {
                MoveTowardsPlayer();
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

    private void PerformRangedAttack()
    {
        if (canAttack)
        {
            canAttack = false;
            animator.SetTrigger("Punching");

            GameObject projectile = Instantiate(projectilePrefab, spawnPoint.position, spawnPoint.rotation);
            Vector3 direction = (player.position - spawnPoint.position).normalized;

            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = direction * projectileSpeed;
            }

            StartCoroutine(ResetAttackCooldown());
        }
    }

    private IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(rangedAttackCooldown);
        canAttack = true;
    }
}
