using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    GameObject inventoryUI;

    [SerializeField]
    float moveSpeed = 5f;

    [SerializeField]
    GameObject currentTarget;

    private NavMeshAgent agent;

    private float _stopDistance = 0.2f;

    private Vector2 _moveDestination;

    private Rigidbody2D rb;

    private Animator animator;

    private float attackDuration = 1.5f;

    public bool haveEnemy;

    public float attackRange = 1.5f;

    public InventoryObject inventory;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        _moveDestination = rb.position;
    }

    void Start()
    {
        agent.speed = moveSpeed;
    }

    void Update()
    {
        ProcessInput();
    }

    void FixedUpdate()
    {
        MovementController();
    }

    void ProcessInput()
    {
        if (
            Input.GetMouseButton(0) &&
            !EventSystem.current.IsPointerOverGameObject()
        )
        {
            RaycastHit2D hit =
                Physics2D
                    .Raycast(Camera
                        .main
                        .ScreenToWorldPoint(Input.mousePosition),
                    Vector3.zero,
                    Mathf.Infinity,
                    64);

            if (hit.collider != null)
            {
                currentTarget = hit.collider.gameObject;
                _moveDestination =
                    Camera.main.ScreenToWorldPoint(Input.mousePosition);

                agent.stoppingDistance = attackRange;

                agent.SetDestination (_moveDestination);
            }
            else
            {
                currentTarget = null;
                _moveDestination =
                    Camera.main.ScreenToWorldPoint(Input.mousePosition);

                agent.stoppingDistance = _stopDistance;

                agent.SetDestination (_moveDestination);
            }
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }

    void Attack()
    {
        currentTarget.GetComponent<Status>().TakeDamage(2);
    }

    IEnumerator AttackAnim()
    {
        while (true)
        {
            Debug.Log("Attack");

            if (currentTarget != null)
            {
                animator.SetTrigger("BasicAttack");
            }
            else
            {
                break;
            }

            yield return new WaitForSeconds(attackDuration);
        }
    }

    void MovementController()
    {
        Vector2 _moveDirection = (_moveDestination - rb.position).normalized;

        if (_moveDirection.x < 0)
        {
            transform.GetChild(0).localScale = new Vector3(-1, 1, 1);
        }
        else if (_moveDirection.x > 0)
        {
            transform.GetChild(0).localScale = Vector3.one;
        }

        if (agent.velocity.magnitude <= 0)
        {
            animator.SetFloat("Speed", 0);

            if (currentTarget != null && !haveEnemy)
            {
                haveEnemy = true;
                StartCoroutine(AttackAnim());
            }
            else if (currentTarget == null && haveEnemy)
            {
                haveEnemy = false;
            }
        }
        else
        {
            animator.SetFloat("Speed", 1);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        var item = other.GetComponent<GroundItem>();

        if (item)
        {
            Item _item = new Item(item.item);
            inventory.AddItem (_item);
            Destroy(other.gameObject);
        }
    }
}
