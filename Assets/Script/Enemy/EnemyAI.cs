
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    private UnityEngine.AI.NavMeshAgent agent;
    public Transform player;
    float y = 0f;
    float z = 0f;
    private Enemy enemy;
    float detectionRadius = 10f;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        CapsuleCollider capsuleCollider = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        agent.destination = player.position;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);
        foreach (var hitCollider in hitColliders)
        {
            Enemy enemy = hitCollider.GetComponent<Enemy>();
            if (enemy != null && (enemy.Figure_type_enemy == "wp" || enemy.Figure_type_enemy == "bp" || enemy.Figure_type_enemy == "white_pawn" || enemy.Figure_type_enemy == "black_pawn"))
            {
                Vector3 newPosition = enemy.transform.position;
                newPosition.y = 0;
                enemy.transform.position = newPosition;
            }
        }
        //transform.rotation = Quaternion.Euler(-90, y, z);
        agent.updateRotation = false;
    }
}