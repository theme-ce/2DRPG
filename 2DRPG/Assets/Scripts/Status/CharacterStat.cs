using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

[Serializable]
public class CharacterStat
{
    public float BaseValue;

    public float ModifiedValue;

    public virtual float Value
    {
        get
        {
            if (_isDirty || BaseValue != lastBaseValue)
            {
                lastBaseValue = BaseValue;
                _value = CalculateFinalValue();
                ModifiedValue = _value;
                _isDirty = false;
            }
            return _value;
        }
    }

    public Attributes type;

    protected readonly List<StatModifier> statModifiers;

    public readonly ReadOnlyCollection<StatModifier> StatModifiers_;

    protected bool _isDirty = true;

    protected float _value;

    protected float lastBaseValue = float.MinValue;

    public CharacterStat()
    {
        statModifiers = new List<StatModifier>();
        StatModifiers_ = statModifiers.AsReadOnly();
    }

    public CharacterStat(float baseValue) :
        this()
    {
        BaseValue = baseValue;
    }

    public virtual void AddModifier(StatModifier mod)
    {
        _isDirty = true;
        statModifiers.Add (mod);
        statModifiers.Sort (CompareModifierOrder);
    }

    protected virtual int CompareModifierOrder(StatModifier a, StatModifier b)
    {
        if (a.Order < b.Order)
        {
            return -1;
        }
        else if (a.Order > b.Order)
        {
            return 1;
        }
        return 0;
    }

    public virtual bool RemoveModifier(StatModifier mod)
    {
        if (statModifiers.Remove(mod))
        {
            _isDirty = true;
            return true;
        }
        return false;
    }

    public virtual bool RemoveAllModifiersFromSource(object source)
    {
        bool didRemove = false;

        for (int i = statModifiers.Count - 1; i >= 0; i--)
        {
            if (statModifiers[i].Source == source)
            {
                _isDirty = true;
                didRemove = true;
                statModifiers.RemoveAt (i);
            }
        }

        return didRemove;
    }

    protected virtual float CalculateFinalValue()
    {
        float finalValue = BaseValue;
        float sumPercentAdd = 0;

        for (int i = 0; i < statModifiers.Count; i++)
        {
            StatModifier mod = statModifiers[i];

            if (mod.Type == StatModType.Flat)
            {
                finalValue += mod.Value;
            }
            else if (mod.Type == StatModType.PercentAdd)
            {
                float valueToPercent = mod.Value / 100f;

                sumPercentAdd += valueToPercent;

                if (
                    i + 1 >= statModifiers.Count ||
                    statModifiers[i + 1].Type != StatModType.PercentAdd
                )
                {
                    finalValue *= 1 + sumPercentAdd;
                    sumPercentAdd = 0;
                }
            }
            else if (mod.Type == StatModType.PercentMult)
            {
                float valueToPercent = mod.Value / 100f;

                finalValue *= 1 + valueToPercent;
            }
        }

        return (float) Math.Round(finalValue, 4);
    }
}
