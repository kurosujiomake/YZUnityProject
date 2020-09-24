using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public float CameraSize = 5;
    public enum CMode { Fixed, follow, Snap};
    public CMode Camera_Mode;
    [SerializeField] private float FollowSpeed = 5;
    [SerializeField] private Transform Target;
    [Header ("Check if using GameObject Tag instead of Name, then input the target Name/Tag in Tar Name")]
    [SerializeField] private bool useTag = false;
    public string tarName;
    public Transform fixedTarget = null;
    public float fixedCamDistance = 0;
    // Start is called before the first frame update
    void Awake()
    {
        FindTarget(tarName);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch(Camera_Mode)
        {
            case CMode.Fixed:
                if(fixedTarget)
                {
                    Vector3 t = fixedTarget.position;
                    t.z = -fixedCamDistance;
                    transform.position = t;
                }
                break;
            case CMode.follow:
                if (Target != null)
                {
                    Vector3 tarLoc = Target.position;
                    tarLoc.z = -10f;
                    transform.position = Vector3.Slerp(transform.position, tarLoc, FollowSpeed * Time.deltaTime);
                }
                break;
            case CMode.Snap:
                if(Target)
                {
                    Vector3 t = Target.position;
                    t.z = -10f;
                    transform.position = t;
                }
                break;
        }
        
    }
    public void ChangeMode(int i)
    {
        switch(i)
        {
            case 0:
                Camera_Mode = CMode.Fixed;
                break;
            case 1:
                Camera_Mode = CMode.follow;
                break;
            case 2:
                Camera_Mode = CMode.Snap;
                break;
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
