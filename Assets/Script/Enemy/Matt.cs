using UnityEngine;

public class Matt : MonoBehaviour
{
    private Player player;
    private Enemy enemy;
    
    void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemy = other.GetComponent<Enemy>();
            if (enemy != null && player != null)
            {
                enemy.Enemy_hp=enemy.Enemy_hp-player.matt;
                Destroy(gameObject);
            }
        }
    }
}