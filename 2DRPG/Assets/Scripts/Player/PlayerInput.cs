using UnityEditor;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public GameObject getCurrentTarget
    {
        get
        {
            return _currentTarget;
        }
    }

    private GameObject _currentTarget;
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        SelectTarget();
    }

    void SelectTarget()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.zero, Mathf.Infinity, 64);

            if(hit.collider != null)
            {
                _currentTarget = hit.collider.gameObject;
            }
            else
            {
                _currentTarget = null;
            }
        }
    }
}
