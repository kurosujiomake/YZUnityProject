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
    private float InputDmg;
    [SerializeField]
    private int Ele, hitCount;
    [SerializeField]
    private bool isACrit;



    public float ReturnFloatValues(string which)
    {
        float a = 0;
        switch(which)
        {
            case "dmg":
                a = InputDmg;
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
            case "hitCount":
                a = hitCount;
                break;
        }
        return a;
    }
    public bool ReturnBools(string which)
    {
        bool a = false;
        switch(which)
        {
            case "crit":
                a = isACrit;
                break;
        }
        return a;
    }
    public void SetValues( float dmg, bool crit, int ele, int _hitCount)
    {
        InputDmg = dmg;
        isACrit = crit;
        Ele = ele;
        hitCount = _hitCount;
    }
}