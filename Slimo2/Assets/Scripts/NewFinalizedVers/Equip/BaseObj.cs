using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjType
{
    Weapon,
    Armor,
    Relic,
    Accessory
}
public class BaseObj : ScriptableObject
{
    public int ObjID;
    public ObjType ObjectType;
    public GameObject InGamePrefab;
    public Sprite UIImage;
    public bool isInPlayerInv = false;
    [TextArea(15,5)]
    public string Desc;

}
