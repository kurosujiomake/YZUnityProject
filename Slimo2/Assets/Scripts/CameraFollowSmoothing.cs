using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowSmoothing : MonoBehaviour
{
    [SerializeField]
    private GameObject followObj = null;
    public float smoothingSpd = 0;
    public string followTarTag = null;
    private pStates pst = null;
    private PlayerConsolidatedControl pc = null;
    [SerializeField]
    private bool canTurnFollow = false;
    public float delayTimeTurning = 0;
    private bool curPFacing = false;
    private Camera c = null;
    // Start is called before the first frame update
    void Start()
    {
        c = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        followObj = GameObject.FindGameObjectWithTag(followTarTag);
        pst = GameObject.FindGameObjectWithTag("Player").GetComponent<pStates>();
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConsolidatedControl>();
        curPFacing = pc.ReturnFacingDir();
        canTurnFollow = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 t = transform.position;
        t.y = followObj.transform.position.y;
        transform.position = t;
        if (pc.ReturnFacingDir() != curPFacing)
        {
            canTurnFollow = false;
            StartCoroutine(Timer(delayTimeTurning));
        }
        if(pc.ReturnFacingDir() == curPFacing)
        {
            canTurnFollow = true;
            StopAllCoroutines();
        }
        if (canTurnFollow)
        {
            Smoothing();
        }
    }
    private IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);
        canTurnFollow = true;
        curPFacing = pc.ReturnFacingDir();
    }
    private void Smoothing()
    {
        if(followObj)
        {
            Vector3 tarLoc = followObj.transform.position;
            transform.position = Vector3.Slerp(transform.position, tarLoc, smoothingSpd * Time.deltaTime);
        }
    }
    public void SnapToWarpedArea() //in case of a warp
    {
        transform.position = followObj.transform.position;
        c.ChangeMode(2);
        c.ChangeMode(1);
    }
}
