using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject EquipUI;
    public GameObject Player;
    public PlayerControlManager pCM;
    public enum GameState
    {
        Gameplay,
        EquipUIOpen,
        Cutscene
    }
    public GameState gState;
    // Start is called before the first frame update
    void Start()
    {
        EquipUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        switch(gState)
        {
            case GameState.Gameplay:
                Time.timeScale = 1;
                Player.GetComponent<Animator>().SetBool("IsGamePlay", true);
                Player.GetComponent<Animator>().speed = 1;
                Player.GetComponent<PlayerControllerNew>().SetPState(1);
                if(pCM.GetButtonDown("Inv"))
                {
                    ToggleEquipUI();
                    gState = GameState.EquipUIOpen;
                    print("toggling On");
                }
                break;
            case GameState.EquipUIOpen:
                Time.timeScale = 0;
                Player.GetComponent<Animator>().SetBool("IsGamePlay", false);
                Player.GetComponent<Animator>().speed = 0;
                Player.GetComponent<PlayerControllerNew>().SetPState(0);
                if (pCM.GetButtonDown("Inv"))
                {
                    ToggleEquipUI();
                    gState = GameState.Gameplay;
                    print("toggling off");
                    ResetAllTriggers();
                }
                break;
            case GameState.Cutscene:

                break;
        }
    }

    void ResetAllTriggers() // remember to add any new triggers here
    {
        Animator anim = Player.GetComponent<Animator>();
        anim.ResetTrigger("Jump");
        anim.ResetTrigger("pushAtt");
        anim.ResetTrigger("KnockupAtt");
        anim.ResetTrigger("Blink");
        anim.ResetTrigger("Spike");
        anim.ResetTrigger("GAtt_a");
        anim.ResetTrigger("AAtt_a");
        anim.ResetTrigger("BowAtt");
        anim.ResetTrigger("SpAtk");
        anim.ResetTrigger("GDashAtk");
        anim.ResetTrigger("SwKnockUp");
        anim.ResetTrigger("DownAtk");
        anim.ResetTrigger("SwordUlt");
        anim.ResetTrigger("SwordUltFinish");

    }
    void ToggleEquipUI ()
    {
        if(gState != GameState.EquipUIOpen && !EquipUI.gameObject.activeSelf)
        {
            if(!EquipUI.gameObject.activeSelf)
            {
                EquipUI.SetActive(true);
            }
        }
        if(gState == GameState.EquipUIOpen)
        {
            if(EquipUI.gameObject.activeSelf)
            {
                EquipUI.SetActive(false);
            }
        }
    }
    public void SendEquipUItoggle() //when player presses decline at main equip screen, call this function
    {
        ToggleEquipUI();
    }
}
