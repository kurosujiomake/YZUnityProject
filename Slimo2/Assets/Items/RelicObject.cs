using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ModType
{
    AdditionalEffect,
    MultiHit,
    HomingProj,
    None

}
[CreateAssetMenu(fileName = "New Relic Object", menuName = "Inventory System/Items/Relic")]
public class RelicObject : ItemObject
{

    public AttModifier[] attMods;
    private void Awake()
    {
        type = ItemType.Relic;

    }
}

[System.Serializable]
public class AttModifier
{
    public ModType mod;
    public GameObject modPrefab;

}