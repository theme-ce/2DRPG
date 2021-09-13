using UnityEngine;

[System.Serializable]
public class Attribute
{
    [System.NonSerialized]
    public PlayerStatus parent;
    public Attributes type;
    public StatModifier value;
    public void SetParent(PlayerStatus _parent)
    {
        parent = _parent;
        value = new StatModifier(AttributeModified);
    }

    public void AttributeModified()
    {
        parent.AttributeModified(this);
    }
}
