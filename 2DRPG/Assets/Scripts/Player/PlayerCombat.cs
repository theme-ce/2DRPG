using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private PlayerManager playerManager;
    private PlayerController playerController;
    private PlayerStatus status;
    private DamageSkillControl damageSkill;
    private Animator animator;

    void Awake()
    {
        status = GetComponent<PlayerStatus>();
        animator = GetComponent<Animator>();
        damageSkill = GetComponent<DamageSkillControl>();
        playerController = GetComponent<PlayerController>();
        playerManager = GetComponent<PlayerManager>();
    }

    void Update()
    {
        AutoAttack();
    }

    void Damage()
    {
        if(playerController.currentTarget != null)
        {
            playerController.currentTarget.GetComponent<EnemyController>().TakeDamage(status.GetATK, playerManager);
        }
    }

    void AutoAttack()
    {
        if(playerController.isAttack)
        {
            animator.SetBool("Attack", true);
        }
        else
        {
            animator.SetBool("Attack", false);
        }
    }

    public void TakeDamage(float damage)
    {
        status.currentHp -= damage;
    }
}
