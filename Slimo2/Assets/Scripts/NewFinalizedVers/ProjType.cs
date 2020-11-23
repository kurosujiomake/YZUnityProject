using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjType : MonoBehaviour //attach this script to all objects that are projs
{
    public enum Type
    {
        Bullet,
        Area,
        Other
    }
    public Type Proj_Type;
}
