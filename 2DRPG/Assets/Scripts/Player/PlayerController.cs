using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 5f;

    public InventoryObject inventory;
    public GameObject currentTarget;
    public bool isAttack = false;

    private NavMeshAgent agent;
    private float _stopDistance = 0.2f;
    private Vector2 _moveDestination;
    private Rigidbody2D rb;
    private Animator animator;
    private PlayerStatus status;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        status = GetComponent<PlayerStatus>();

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
        if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.zero, Mathf.Infinity, 64);

            isAttack = false;

            if (hit.collider != null)
            {
                currentTarget = hit.collider.gameObject;
                _moveDestination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                agent.stoppingDistance = status.attackRange;
                agent.SetDestination (_moveDestination);
            }
            else
            {
                currentTarget = null;
                _moveDestination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                agent.stoppingDistance = _stopDistance;
                agent.SetDestination (_moveDestination);
            }
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

            if (currentTarget != null)
            {
                isAttack = true;
            }
            else if (currentTarget == null)
            {
                isAttack = false;
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
