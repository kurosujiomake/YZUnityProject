using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public float CameraSize = 5;
    public enum CMode { Fixed, follow};
    public CMode Camera_Mode;
    [SerializeField] private float FollowSpeed = 5;
    [SerializeField] private Transform Target;
    [Header ("Check if using GameObject Tag instead of Name, then input the target Name/Tag in Tar Name")]
    [SerializeField] private bool useTag = false;
    public string tarName;
    // Start is called before the first frame update
    void Awake()
    {
        FindTarget(tarName);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Camera_Mode == CMode.Fixed)
        {
            //do the fixed camera thing here
        }
        if(Camera_Mode == CMode.follow)
        {
            if(Target != null)
            {
                Vector3 tarLoc = Target.position;
                tarLoc.z = -10f;
                transform.position = Vector3.Slerp(transform.position, tarLoc, FollowSpeed * Time.deltaTime);
            }
        }
    }
    public void FindTarget(string tar) // Use this to set which target to follow, use object name
    {
        if(!useTag)
        {
            Target = GameObject.Find(tar).GetComponent<Transform>();
        }
        else
        {
            Target = GameObject.FindGameObjectWithTag(tar).GetComponent<Transform>();
        }
        
    }
}
