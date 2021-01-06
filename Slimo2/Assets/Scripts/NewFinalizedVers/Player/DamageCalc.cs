using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCalc : MonoBehaviour
{
    [SerializeField]
    float baseDmg, dmgMulti, dmgRange;
    [SerializeField]
    int eleType;
    [SerializeField]
    float baseDef, defMulti;
    [SerializeField]
    int eleRes;
    public float TotalOutput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetDamageParams(float _baseDmg, float _dmgMulti, float _dmgRange, int _eleType)
    {

    }
    public void GetDefenseParams(float _baseDef, float _defMulti, int _eleRes)
    {

    }
}
