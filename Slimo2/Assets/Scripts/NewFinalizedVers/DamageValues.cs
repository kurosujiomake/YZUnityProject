using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageValues : MonoBehaviour
{
    [SerializeField]
    private int _hitCount = 0;
    [HideInInspector]
    public int hitCount
    {
        get { return _hitCount; }
    }
    [SerializeField]
    private float _hitDelay = 0;
    [HideInInspector]
    public float hitDelay
    {
        get { return _hitDelay; }
    }
    [SerializeField]
    private float _dir = 0;
    [HideInInspector]
    public float dir
    {
        get { return _dir; }
    }
    [SerializeField]
    private float _force = 0;
    [HideInInspector]
    public float force
    {
        get { return _force; }
    }
    [SerializeField]
    private float _dmg = 0;
    [HideInInspector]
    public float dmg
    {
        get { return _dmg; }
    }

    [SerializeField]
    private int _dmgID = 0; //this should be set by the att script once per hitbox activation, and never repeat the value two times in a row
    [HideInInspector]
    public int dmgID
    {
        get { return _dmgID; }
    }

    public void SetHitCount(int m_hitCount)
    {
        _hitCount = m_hitCount;
    }
    public void SetDirection(float m_dir) //this should be adjusted based on which direction the player is facing
    {
        _dir = m_dir;
    }
    public void SetDamage(float m_dmg)
    {
        _dmg = m_dmg;
    }
    public void SetRandomHitID(int m_dmgID) //this should be called independently always
    {
        _dmgID = m_dmgID;
    }
    public void SetValues(int m_hitCount, float m_hitDelay, float m_dir, float m_force, float m_dmg) //global set almost all parameters, shouldnt be used that often
    {
        _hitCount = m_hitCount;
        _hitDelay = m_hitDelay;
        _dir = m_dir;
        _force = m_force;
        _dmg = m_dmg;
    }
}
