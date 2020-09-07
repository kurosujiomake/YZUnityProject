using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void ModifiedEvent();
[System.Serializable]
public class ModifiableInt 
{
    [SerializeField]
    private int baseValue;
    public int BaseValue { get { return baseValue; } set { baseValue = value; UpdateModifiedValue();} }

    [SerializeField]
    private int modifiedValue;
    public int ModifiedValue { get { return modifiedValue; } private set { modifiedValue = value; } }

    public List<iModifiers> modifiers = new List<iModifiers>();

    public event ModifiedEvent ValueModified;
    public ModifiableInt (ModifiedEvent method = null)
    {
        modifiedValue = BaseValue;
        if(method != null)
        {
            ValueModified += method;
        }
    }

    public void RegisterModEven(ModifiedEvent method)
    {
        ValueModified += method;
    }
    public void UnRegisterModEven(ModifiedEvent method)
    {
        ValueModified -= method;
    }

    public void UpdateModifiedValue()
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

    public void AddModifier(iModifiers _modifier)
    {
        modifiers.Add(_modifier);
        UpdateModifiedValue();
    }
    public void RemoveModifier(iModifiers _modifier)
    {
        modifiers.Remove(_modifier);
        UpdateModifiedValue();
    }
}
