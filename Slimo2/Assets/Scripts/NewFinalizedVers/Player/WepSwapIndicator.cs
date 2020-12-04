using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WepSwapIndicator : MonoBehaviour
{
    public Sprite[] wepImages;
    public SpriteRenderer curDisplay;
    public float iconDuration = 0;
    // Start is called before the first frame update
    void Start()
    {
        curDisplay = GetComponent<SpriteRenderer>();
        curDisplay.enabled = false;
    }


    public void SwapWeps(int which)
    {
        WepImageSwap(which);
        curDisplay.enabled = true;
        StopAllCoroutines();
        StartCoroutine(timer());
    }
    IEnumerator timer()
    {
        yield return new WaitForSeconds(iconDuration);
        curDisplay.enabled = false;
    }

    void WepImageSwap(int whichWep)
    {
        switch(whichWep)
        {
            case 0:
                curDisplay.sprite = wepImages[0];
                break;
            case 1:
                curDisplay.sprite = wepImages[1];
                break;
                //add other weps later
        }
    }
}
