using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipHolder : MonoBehaviour
{
    public PlayerControlManager pCM = null;
    public ObjDatabase ItemDB;

    public int MainWp;
    public int SubWp;

    public int spID1;
    public int spID2;
    public int ultID;

    // Start is called before the first frame update
    void Start()
    {
        GetIDs();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void GetIDs()
    {
        spID1 = ItemDB.ReturnSpAtkID(MainWp);
        spID2 = ItemDB.ReturnSpAtkID(SubWp);
        ultID = ItemDB.ReturnUltAtkID(MainWp);
    }

}
