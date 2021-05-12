using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashDisplay : MonoBehaviour
{
    public icons[] dashIcons;
    public int curDashAmt = 5;
    // Start is called before the first frame update
    void Start()
    {
        DashDisplayUpdate();
    }

    public void UpdateDashAmt(int howMuch)
    {
        curDashAmt = howMuch;
        DashDisplayUpdate();
    }

    void DashDisplayUpdate()
    {
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
    }
}
[System.Serializable]
public struct icons
{
    public int iconID;
    public GameObject icon;
}
