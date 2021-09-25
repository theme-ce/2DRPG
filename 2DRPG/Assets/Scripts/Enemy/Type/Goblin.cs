using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour
{
    private Animator animator;
    private EnemyController controller;

    void Awake()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<EnemyController>();
    }

    void Update()
    {
        if (controller.player)
        {
            if ((controller.player.transform.position - transform.position).magnitude > controller.enemy.attackRange)
            {
                if (controller.agent.velocity.magnitude > 0)
                {
                    animator.SetBool("Attack", false);
                    animator.SetFloat("Speed", 1);
                }
                else
                {
                    animator.SetFloat("Speed", 0);
                }
            }
            else
            {
                animator.SetFloat("Speed", 0);

                if (controller.player != null)
                {
                    animator.SetBool("Attack", true);
                }
                else
                {
                    animator.SetBool("Attack", false);
                }
            }
        }
        else
        {
            if (controller.agent.velocity.magnitude > 0)
            {
                animator.SetBool("Attack", false);
                animator.SetFloat("Speed", 1);
            }
            else
            {
                animator.SetFloat("Speed", 0);
            }
        }
    }
}
