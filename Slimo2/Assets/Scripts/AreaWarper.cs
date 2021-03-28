using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaWarper : MonoBehaviour
{
    public Transform[] IDList;
    [Header("Can use either ID to warp or name, depending on the bool checked")]
    //[SerializeField] private int WarpID = 0;
    //[SerializeField] private string WarpName = null;
    public bool useInt = false;
    [Header("Input Destination in String Format here")]
    public string WarpDest = null;
    [Header("Input warp destination in int format here")]
    public int WarpDestInt = 0;
    public float WarpCD = 0;
    [SerializeField] private bool canWarp = false;
    private float timer = 0;
    [Header("Screen Transition Effect")]
    public float screenTransitionTimeToReset = 0;
    public GameObject screenTransitionEffect;

    // Start is called before the first frame update
    void Start()
    {
        canWarp = true;
        screenTransitionEffect.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(!canWarp)
        {
            timer -= Time.deltaTime;
        }
        if(timer <= 0)
        {
            canWarp = true;
        }
    }

    public void StartCD()
    {
        canWarp = false;
        timer = WarpCD;
    }
    void TriggerCD()
    {
        Invoke("DeactivateThisObject", screenTransitionTimeToReset);
        GameObject[] n = GameObject.FindGameObjectsWithTag("Warp");
        foreach(GameObject p in n)
        {
            p.GetComponent<AreaWarper>().StartCD();
        }
    }
    Transform TeleTo(int ID)
    {
        return IDList[ID];
    }
    Transform TeleTo(string Name)
    {
        return GameObject.FindGameObjectWithTag(Name).GetComponent<Transform>();
    }

    private void DeactivateThisObject()
    {
        screenTransitionEffect.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.GetComponent<Transform>().tag == "Player")
        {
            if(canWarp)
            {
                screenTransitionEffect.SetActive(true);
                Transform tempPlayer = col.GetComponent<Transform>();
                Transform cFS = GameObject.FindGameObjectWithTag("CamSmoothingTar").transform;
                if (useInt)
                {
                    tempPlayer.transform.position = TeleTo(WarpDestInt).position;
                    cFS.GetComponent<CameraFollowSmoothing>().SnapToWarpedArea();
                }
                else
                {
                    tempPlayer.transform.position = TeleTo(WarpDest).position;
                    cFS.GetComponent<CameraFollowSmoothing>().SnapToWarpedArea();
                }
                TriggerCD();
            }
        }
    }
}
