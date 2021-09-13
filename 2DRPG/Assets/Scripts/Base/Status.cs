using UnityEngine;

public class Status : MonoBehaviour
{
    [Header("Health & Mana: ")]
    public float currentHp;

    public float maxHp;

    public float currentMana;

    public float maxMana;

    public float hpRegen;

    public float manaRegen;

    [Header("Combat: ")]
    public float attackDamage;

    public float attackRange;

    public float defense;

    public float evasion;

    public float hit;

    [Header("Status: ")]
    public float strength;

    public float dexterity;

    public float vitality;

    public float intellect;

    public float lucky;

    void Start()
    {
        CalculateAtkDamage();
    }

    public void TakeDamage(float dmg)
    {
        this.currentHp -= dmg;

        if (this.currentHp <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void CalculateAtkDamage()
    {
        attackDamage = (strength * 2);
    }
}
