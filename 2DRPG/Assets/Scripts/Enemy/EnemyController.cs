using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    LayerMask playerLayer;

    public float currentHp;
    public EnemyObject enemy;
    public float followDuration;
    public float maxRandomRadius;
    public Image hpBar;
    public GameObject hpGO;
    public GameObject floatingPoints;
    public Vector3 floatingOffset;

    private Vector3 moveDestination;
    private Vector3 moveDirection;
    private float _showHPTime = 0f;
    private float moveDuration;
    private float moveTimeCount = 0f;
    private float followTimeCount = 0f;
    private bool isFollowPlayer = false;

    [System.NonSerialized]
    public GameObject player;

    [System.NonSerialized]
    public NavMeshAgent agent;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        agent.speed = enemy.moveSpeed;

        moveDestination = transform.position;
        moveDuration = Random.Range(3f, 7f);
        currentHp = enemy.maxHp;
    }

    void Update()
    {
        RandomMove();

        if (enemy.type == EnemyTypes.Aggressive)
        {
            DetectPlayer();
        }
        else if (enemy.type == EnemyTypes.Friendly)
        {
            if (player != null)
            {
                isFollowPlayer = true;
            }
        }

        if (isFollowPlayer)
        {
            if (followTimeCount < followDuration)
            {
                followTimeCount += Time.deltaTime;
                moveDestination = player.transform.position;
            }
            else
            {
                player = null;
                isFollowPlayer = false;
            }
        }

        Move();

        HpBarShow();
    }

    void RandomMove()
    {
        if (moveTimeCount < moveDuration)
        {
            moveTimeCount += Time.deltaTime;
            return;
        }
        else
        {
            moveDuration = Random.Range(3f, 7f);
            moveTimeCount = 0f;
        }

        Vector3 randomPosition = new Vector3(Random.Range(-maxRandomRadius, maxRandomRadius), Random.Range(-maxRandomRadius, maxRandomRadius), 1f);

        moveDestination = transform.position + randomPosition;
        agent.stoppingDistance = 0f;
    }

    void Move()
    {
        agent.SetDestination(moveDestination);

        moveDirection = (moveDestination - transform.position).normalized;

        if (moveDirection.x < 0)
        {
            transform.GetChild(0).localScale = new Vector3(-1, 1, 1);
        }
        else if (moveDirection.x > 0)
        {
            transform.GetChild(0).localScale = Vector3.one;
        }
    }

    void DetectPlayer()
    {
        Collider2D hit = Physics2D.OverlapCircle(transform.position, enemy.detectionRadius, playerLayer);

        if (hit != null)
        {
            agent.stoppingDistance = enemy.attackRange;
            player = hit.gameObject;
            followTimeCount = 0f;
            isFollowPlayer = true;
        }
    }

    void Damage()
    {
        bool isCrit = false;
        float damage = Mathf.Floor(Random.Range(enemy.attackDamage * 0.92f, enemy.attackDamage * 1.08f));

        if (enemy.crit > Random.Range(0f, 100f))
        {
            isCrit = true;
            damage = Mathf.Floor(damage);
        }

        if (player != null)
        {
            player.GetComponent<PlayerCombat>().TakeDamage(damage, isCrit);
        }
    }

    public void TakeDamage(float damage, bool isCrit, GameObject player)
    {
        this.player = player;

        currentHp -= damage;

        var obj = Instantiate(floatingPoints, transform.position + floatingOffset, Quaternion.identity);
        obj.transform.GetChild(0).GetComponent<TextMesh>().text = damage.ToString();
        if (isCrit)
        {
            obj.transform.GetChild(0).GetComponent<TextMesh>().color = Color.red;
        }

        _showHPTime = 5f;

        if (currentHp <= 0)
        {
            player.GetComponent<PlayerLevel>().currentExp += enemy.expDrop;
            player.GetComponent<PlayerGold>().gold += enemy.goldDrop;

            Destroy(this.gameObject);
        }
    }

    void HpBarShow()
    {
        if (_showHPTime > 0)
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
}
