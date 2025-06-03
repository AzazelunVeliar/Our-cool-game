using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage;
    public float speed = 20f;
    public float lifetime = 5f;

    private Transform target;
    private EnemyShooter enemy;

    private void Start()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            target = playerObject.transform;
        }

        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        if (target == null) return;
        Vector3 targetPosition = target.position;

        Vector3 direction = (targetPosition - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;
        transform.forward = direction;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) return;

        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.hp -= damage;
            }
        }

        Destroy(gameObject);
    }
}
