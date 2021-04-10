using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : MonoBehaviour
{
    [SerializeField]
    private float MaxHP;
    [SerializeField]
    private float CurHP;
    [SerializeField]
    private bool isDead;
    public HPBar hpBar;
    // Start is called before the first frame update
    void Start()
    {
        hpBar = GetComponentInChildren<HPBar>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool ReturnIsDead()
    {
        return isDead;
    }
    public void SetDeathState(bool value)
    {
        isDead = value;
    }
    public float ReturnHpValues(int which)
    {
        float v = 0;
        switch(which)
        {
            case 0: //return max hp;
                v = MaxHP;
                break;
            case 1: //return current hp
                v = CurHP;
                break;
        }
        return v;
    }
    public void TakeDamage(int type, float amt)
    {
        switch(type)
        {
            case 0: //sets hp
                CurHP = amt;
                break;
            case 1: //subtracts hp
                CurHP -= amt;
                break;
        }
        hpBar.SetHPCur(CurHP);
        DeathCheck();
    }
    public void HealDamage(int type, float amt)
    {
        DeathCheck(); //dont heal dead things
        if(!isDead)
        {
            switch (type)
            {
                case 0: //sets cur hp

                    break;
                case 1: //adds hp

                    break;
            }
        }
    }
    public void SetStartHP(float value)
    {
        MaxHP = value;
        CurHP = value;
        isDead = false;
        hpBar.SetHP(MaxHP, CurHP); 
    }
    private void DeathCheck()
    {
        if(CurHP <= 0)
        {
            isDead = true;
        }
    }
}
