using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ModifiedEvent();

[System.Serializable]
public class StatModifier
{
    [SerializeField]
    private int baseValue;

    public int BaseValue
    {
        get
        {
            return baseValue;
        }
        set
        {
            baseValue = value; 
            UpdateModifedValue();
        }
    }

    [SerializeField]
    private int modifiedValue;

    public int ModifiedValue
    {
        get
        {
            return modifiedValue;
        }
        private
        set
        {
            modifiedValue = value;
        }
    }

    public List<IModifier> modifiers = new List<IModifier>();

    public event ModifiedEvent ValueModified;

    public StatModifier(ModifiedEvent method = null)
    {
        ModifiedValue = BaseValue;
        if(method != null)
        {
            ValueModified += method;
        }
    }

    public void RegisterModEvent(ModifiedEvent method)
    {
        ValueModified += method;
    }

    public void UnregisterModEvent(ModifiedEvent method)
    {
        ValueModified -= method;
    }

    public void UpdateModifedValue()
    {
        var valueToAdd = 0;
        for (int i = 0; i < modifiers.Count; i++)
        {
            modifiers[i].AddValue(ref valueToAdd);
        }
        ModifiedValue = baseValue + valueToAdd;
        if(ValueModified != null)
        {
            ValueModified.Invoke();
        }
    }

    public void AddModifier(IModifier _modifier)
    {
        modifiers.Add(_modifier);
        UpdateModifedValue();
    }

    public void RemoveModifier(IModifier _modifier)
    {
        modifiers.Remove(_modifier);
        UpdateModifedValue();
    }
}
