using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowSmoothing : MonoBehaviour
{
    [SerializeField]
    private GameObject followObj = null;
    public float smoothingSpd = 0;
    public string followTarTag = null;
    private PlayerControllerNew pc = null;
    [SerializeField]
    private bool canTurnFollow = false;
    public float delayTimeTurning = 0;
    private bool curPFacing = false;
    private Camera c = null;
    [SerializeField]
    private float DashCamSpeed = 0;
    // Start is called before the first frame update
    private Parameters p = null;
    private float spd = 0;
    void Start()
    {
        p = GameObject.FindGameObjectWithTag("Player").GetComponent<Parameters>();
        c = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        followObj = GameObject.FindGameObjectWithTag(followTarTag);
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllerNew>();
        curPFacing = pc.facingRight;
        canTurnFollow = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 t = transform.position;
        t.y = followObj.transform.position.y;
        transform.position = t;
        if (pc.facingRight != curPFacing)
        {
            canTurnFollow = false;
            StartCoroutine(Timer(delayTimeTurning));
        }
        if(pc.facingRight == curPFacing)
        {
            canTurnFollow = true;
            StopAllCoroutines();
        }
        if (canTurnFollow)
        {
            Smoothing();
        }
        if(p.m_isDashing || p.m_isADashing)
        {
            spd = DashCamSpeed;
        }
        if(!p.m_isDashing && !p.m_isADashing)
        {
            spd = smoothingSpd;
        }
    }
    private IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);
        canTurnFollow = true;
        curPFacing = pc.facingRight;
    }
    private void Smoothing()
    {
        if(followObj)
        {
            Vector3 tarLoc = followObj.transform.position;
            transform.position = Vector3.Slerp(transform.position, tarLoc, spd * Time.deltaTime);
        }
    }
    public void SnapToWarpedArea() //in case of a warp
    {
        transform.position = followObj.transform.position;
        c.ChangeMode(2);
        c.ChangeMode(1);
    }
}
