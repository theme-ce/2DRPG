using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    LayerMask playerLayer;

    [SerializeField]
    Vector3 startPosition;

    public EnemyObject enemy;

    public float currentHp;

    private NavMeshAgent agent;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        startPosition = transform.position;

        currentHp = enemy.maxHp;
    }

    void Update()
    {
        DetectPlayer();
    }

    void DetectPlayer()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, enemy.detectionRadius, playerLayer);

        if(hit != null)
        {
            agent.SetDestination(hit.gameObject.transform.position);
            agent.stoppingDistance = enemy.attackRange;
        }
        else
        {
            agent.SetDestination(startPosition);
            agent.stoppingDistance = 0f;
        }
    }

    public void TakeDamage(float damage)
    {
        currentHp -= damage;

        if(currentHp <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
