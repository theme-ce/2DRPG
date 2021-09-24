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
        if(controller.agent.velocity.magnitude > 0)
        {
            animator.SetBool("Attack", false);
            animator.SetFloat("Speed", 1);
        }
        else
        {
            animator.SetFloat("Speed", 0);

            if(controller.player != null)
            {
                animator.SetBool("Attack", true);
            }
            else
            {
                animator.SetBool("Attack", false);
            }
        }
    }

    void Damage()
    {
        if(controller.player != null)
        {
            controller.player.GetComponent<PlayerCombat>().TakeDamage(controller.enemy.attackDamage);
        }
    }
}
