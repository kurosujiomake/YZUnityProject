using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTransfer : MonoBehaviour
{
    public DataBundle dmgData;
}
[System.Serializable]
public class DataBundle
{
    [SerializeField]
    private float InputDmg, critChance;
    [SerializeField]
    private int Ele;

    public float ReturnFloatValues(string which)
    {
        float a = 0;
        switch(which)
        {
            case "dmg":
                a = InputDmg;
                break;
            case "crit":
                a = critChance;
                break;
        }
        return a;
    }
    public int ReturnIntValues(string which)
    {
        int a = 0;
        switch(which)
        {
            case "ele":
                a = Ele;
                break;
        }
        return a;
    }
    public void SetValues(float dmg, float crit, int ele)
    {
        InputDmg = dmg;
        critChance = crit;
        Ele = ele;
    }
}