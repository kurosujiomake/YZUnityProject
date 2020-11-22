using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWepProjSpawn : MonoBehaviour
{
    public bool FireOne = false;
    public bool FireCont = false;
    public int MaxProj = 0;
    public float projDelay = 0;
    public GameObject[] Proj;
    public int ProjID;
    public Transform[] originPoints;
    public int OriginPID;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator FireOnce(int _projID)
    {
        GameObject clone = Instantiate(Proj[_projID], originPoints[OriginPID].position, Quaternion.identity);
        yield return new WaitForSeconds(0);
    }
}
