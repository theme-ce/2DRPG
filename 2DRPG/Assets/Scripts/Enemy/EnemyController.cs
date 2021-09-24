using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    LayerMask playerLayer;

    [SerializeField]
    Vector3 startPosition;

    public GameObject player;
    public EnemyObject enemy;
    public NavMeshAgent agent;
    public float currentHp;
    public Image hpBar;
    public GameObject hpGO;
    public Vector3 moveDirection;

    private float _showHPTime = 0f;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        startPosition = transform.position;
        agent.speed = enemy.moveSpeed;

        currentHp = enemy.maxHp;
    }

    void Update()
    {
        DetectPlayer();

        if(_showHPTime > 0)
        {
            hpGO.SetActive(true);
            _showHPTime -= Time.deltaTime;
        }
        else
        {
            hpGO.SetActive(false);
        }

        hpBar.fillAmount = currentHp / enemy.maxHp;
    }

    void DetectPlayer()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, enemy.detectionRadius, playerLayer);

        if(hit != null)
        {
            agent.SetDestination(hit.gameObject.transform.position);
            moveDirection = (hit.gameObject.transform.position - transform.position).normalized;
            agent.stoppingDistance = enemy.attackRange;
            player = hit.gameObject;
        }
        else
        {
            agent.SetDestination(startPosition);
            moveDirection = (startPosition - transform.position).normalized;
            agent.stoppingDistance = 0f;
            player = null;
        }

        if(moveDirection.x < 0)
        {
            transform.GetChild(0).localScale = new Vector3(-1, 1, 1);
        }
        else if(moveDirection.x > 0)
        {
            transform.GetChild(0).localScale = Vector3.one;
        }
    }

    public void TakeDamage(float damage, PlayerManager player)
    {
        currentHp -= damage;

        _showHPTime = 5f;

        if(currentHp <= 0)
        {
            player.Level.currentExp += enemy.expDrop;
            player.Gold.gold += enemy.goldDrop;

            Destroy(this.gameObject);
        }
    }
}
