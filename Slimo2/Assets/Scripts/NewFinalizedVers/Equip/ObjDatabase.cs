using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ObjDatabase", menuName = "Objects/Database")]
public class ObjDatabase : ScriptableObject, ISerializationCallbackReceiver
{
    public BaseObj[] Database;


    public void OnAfterDeserialize() //auto set object ID here
    {
        for(int i = 0; i < Database.Length; i++)
        {
            if(Database[i].ObjID != i)
            {
                Database[i].ObjID = i;
            }
        }
    }

    public int ReturnID(int which) //just in case something doesnt match up
    {
        return Database[which].ObjID;
    }
    public bool ReturnPlayerHasItem(int which) //returns if the player has picked up item
    {
        return Database[which].isInPlayerInv;
    }
    public Sprite ReturnUIImage(int which) //returns the ui display image
    {
        return Database[which].UIImage;
    }
    public GameObject ReturnSpawnObj(int which) //returns the prefab to spawn for the obj
    {
        return Database[which].InGamePrefab;
    }
    public WepType ReturnWeaponType(int which)
    {
        WepType w = WepType.NoType;
        if(Database[which] is WepObject) //need to recast as wepobject if database item is a wep object, otherwise return notype
        {
            var d = Database[which] as WepObject;
            w = d.wType;
        }
        return w;
    }
    public float ReturnWepDmg(int which)
    {
        float f = 0;
        if(Database[which] is WepObject) //need to recast as wepobject if database item is a wep object, otherwise return 0
        {
            var d = Database[which] as WepObject;
            d.DamageCalc(); //recalcs final damage to prevent errors
            f = d.FinalDamage;
        }
        return f;
    }
    public int ReturnSpAtkID(int which)
    {
        int i = -1;
        if(Database[which] is WepObject) //returns -1, which is used as a default for null if the obj is not a weapon
        {
            var d = Database[which] as WepObject;
            i = d.SpAtkID;
        }
        return i;
    }
    public int ReturnUltAtkID(int which)
    {
        int i = -1;
        if(Database[which] is WepObject) //same as return spAtkID
        {
            var d = Database[which] as WepObject;
            i = d.UltAtkID;
        }
        return i;
    }
    public void OnBeforeSerialize() //not used but needed to prevent errors
    {
    }
}
