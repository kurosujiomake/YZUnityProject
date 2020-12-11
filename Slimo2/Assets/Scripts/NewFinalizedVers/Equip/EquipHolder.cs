using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipHolder : MonoBehaviour
{
    public PlayerControlManager pCM = null;
    public BaseObj[] inv;

    public WepObject mainWep;
    public WepObject subWep;



    public void PickUpEquip(BaseObj equip)
    {
        for (int i= 0; i< inv.Length; i++)
        {
            if(inv[i] == null)
            {
                inv[i] = equip;
                break;
            }
            else //if it gets to this point, aka inv is full
            {
                print("inv is full"); //add a ui display later
            }
        }

    }

}
