using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashDisplay : MonoBehaviour
{
    public icons[] dashIcons;
    public int curDashAmt = 5;
    public float displayTimer = 0;
    public bool allDisplay = false;
    private bool hasReset = false;
    // Start is called before the first frame update
    void Start()
    {
        DashDisplayUpdate();
        HideAll();
    }

    public void UpdateDashAmt(int howMuch)
    {
        curDashAmt = howMuch;
        DashDisplayUpdate();
        hasReset = false;
    }
    public void ResetDashDisplay(int amt)
    {
        curDashAmt = amt;
        if(!hasReset)
        {
            DashDisplayUpdate();
            hasReset = true;
        }
    }

    void DashDisplayUpdate()
    {
        allDisplay = true;
        foreach (icons i in dashIcons)
        {
            if(i.iconID <= (curDashAmt-1))
            {
                i.icon.SetActive(true);
            }
            if(i.iconID > (curDashAmt-1))
            {
                i.icon.SetActive(false);
            }
        }
        StopAllCoroutines();
        StartCoroutine(DisplayTimer());
    }
    IEnumerator DisplayTimer()
    {
        yield return new WaitForSeconds(displayTimer);
        allDisplay = false;
        HideAll();
    }
    void HideAll()
    {
        foreach (icons i in dashIcons)
        {
            i.icon.SetActive(false);
        }
    }
}
[System.Serializable]
public struct icons
{
    public int iconID;
    public GameObject icon;
}
