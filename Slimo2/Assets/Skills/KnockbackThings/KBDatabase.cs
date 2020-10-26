using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "KnockBack Database", menuName = "KnockBack/Database")]
public class KBDatabase : ScriptableObject, ISerializationCallbackReceiver
{
    public KBBase[] knockBacks;
    public void OnAfterDeserialize()
    {
        for (int i = 0; i < knockBacks.Length; i++)
        {
            if(knockBacks[i].ID != i)
            {
                knockBacks[i].ID = i;
            }
        }
    }
    public float Dir(int _ID)
    {
        return knockBacks[_ID].Dir;
    }
    public float Vel(int _ID)
    {
        return knockBacks[_ID].Velocity;
    }
    public float KBDur(int _ID)
    {
        return knockBacks[_ID].KBDuration;
    }
    public bool Aerial(int _ID)
    {
        return knockBacks[_ID].FloatsTarget;
    }
    public float FloatDur(int _ID)
    {
        return knockBacks[_ID].FloatDuration;
    }
    public void OnBeforeSerialize()
    {

    }
}
