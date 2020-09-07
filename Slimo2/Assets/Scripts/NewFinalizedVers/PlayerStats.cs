using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public InventoryObject playerInventory;
    public InventoryObject equipmentInventory;

    public Stats[] stats;

    public void StatModified(Stats stats)
    {
        Debug.Log(string.Concat(stats.type, " was updated! Value is now ", stats.value.ModifiedValue));
    }

}

[System.Serializable]
public class Stats
{
    [System.NonSerialized]
    public PlayerStats parent;
    public StatBonus type;
    public ModifiableInt value;

    public void SetParent(PlayerStats _parent)
    {
        parent = _parent;
        value = new ModifiableInt(StatModified);
    }
    public void StatModified()
    {
        parent.StatModified(this);
    }
}