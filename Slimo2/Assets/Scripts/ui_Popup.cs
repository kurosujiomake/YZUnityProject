using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ui_Popup : MonoBehaviour
{
    public GameObject popupThisObject;
    public float timeActive = 3;
    bool timerIsRunning = false;

    void Start()
    {
        popupThisObject.SetActive(false);
        timerIsRunning = false;
    }

    void OnTriggerEnter2D(UnityEngine.Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            timerIsRunning = true;
            popupThisObject.SetActive(true);
        }
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeActive > 0)
            {
                timeActive -= Time.deltaTime;
            }
            else
            {
                timeActive = 0;
                timerIsRunning = false;
                popupThisObject.SetActive(false);
                //timeActive = 10;
                Destroy(this.gameObject);
            }
        }
    }

}
