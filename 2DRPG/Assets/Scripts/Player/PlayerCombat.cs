using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    private PlayerController playerController;
    private PlayerStatus status;
    private DamageSkillControl damageSkill;
    private Animator animator;

    public GameObject floatingPoints;
    public Vector3 floatingOffset;

    void Awake()
    {
        status = GetComponent<PlayerStatus>();
        animator = GetComponent<Animator>();
        damageSkill = GetComponent<DamageSkillControl>();
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        AutoAttack();
    }

    void Damage()
    {
        bool isCrit = false;

        float dmg = Mathf.Floor(Random.Range(status.GetATK * 0.92f, status.GetATK * 1.08f));

        if(status.GetCRIT > Random.Range(0f, 100f))
        {
            isCrit = true;
            dmg = Mathf.Floor(dmg * ((150 + status.GetCRITDMG) / 100f));
        }

        if(playerController.currentTarget != null)
        {
            playerController.currentTarget.GetComponent<EnemyController>().TakeDamage(dmg, isCrit, this.gameObject);
        }
    }

    void AutoAttack()
    {
        float attackSpeed = 1 + (status.GetATKSPEED / 100f);
        animator.SetFloat("AttackSpeed", attackSpeed);

        if(playerController.isAttack)
        {
            animator.SetBool("Attack", true);
        }
        else
        {
            animator.SetBool("Attack", false);
        }
    }

    public void TakeDamage(float damage, bool isCrit)
    {
        var obj = Instantiate(floatingPoints, transform.position + floatingOffset, Quaternion.identity);
        obj.transform.GetChild(0).GetComponent<TextMesh>().text = damage.ToString();

        if(isCrit)
        {
            status.currentHp -= damage;
            obj.transform.GetChild(0).GetComponent<TextMesh>().color = Color.red;
            return;
        }

        if(status.GetFLEE > Random.Range(0f, 100f))
        {
            if(status.GetHIT < Random.Range(0f, 100f))
            {
                obj.transform.GetChild(0).GetComponent<TextMesh>().color = Color.yellow;
                obj.transform.GetChild(0).GetComponent<TextMesh>().text = "MISS";
                return;
            }
        }

        status.currentHp -= damage;
    }
}
