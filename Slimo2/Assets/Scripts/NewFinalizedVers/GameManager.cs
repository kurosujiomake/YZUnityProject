using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject EquipUI;
    public GameObject Player;
    public PlayerControlManager pCM;
    public HitStop hStop = null;
    public int cType = 0; //default keyboard, 1 is controller
    private bool anyKeyPressed = false;
    public int titleStage = -1;
    public enum GameState
    {
        Gameplay,
        EquipUIOpen,
        Cutscene
    }
    public enum SceneState
    {
        Title,
        Levels,
        Special,
        Others,
        ETC
    }
    public GameState gState;
    public SceneState sState;
    // Start is called before the first frame update
    void Awake()
    {
        if(sState == SceneState.Levels)
        {
            EquipUI.GetComponent<EquipUINew>().UpdateCursor();
            EquipUI.GetComponent<EquipUINew>().SlotIDSet();
            EquipUI.SetActive(false);
            hStop = GetComponent<HitStop>();
            if(pCM != null) //sets up the control type based on user selection
            {
                switch(cType)
                {
                    case 0:
                        pCM.curControlType = PlayerControlManager.ControlType.keyboard;
                        break;
                    case 1:
                        pCM.curControlType = PlayerControlManager.ControlType.controller;
                        break;
                }
            }
        }
        titleStage = -1;
        
    }

    // Update is called once per frame
    void Update()
    {
        switch(sState)
        {
            case SceneState.Title:
                AdvanceTitle();
                if(!pCM.GetAnyKey())
                {
                    anyKeyPressed = false;
                }

                break;
            case SceneState.Levels:
                switch (gState)
                {
                    case GameState.Gameplay:
                        if (!hStop.gTimeStop)
                        {
                            Time.timeScale = 1;
                        }
                        Player.GetComponent<Animator>().SetBool("IsGamePlay", true);
                        if (!hStop.uTimeStop)
                        {
                            Player.GetComponent<Animator>().speed = 1;
                        }
                        if (pCM.GetButtonDown("Inv"))
                        {
                            ToggleEquipUI();
                            gState = GameState.EquipUIOpen;
                            print("toggling On");
                        }
                        break;
                    case GameState.EquipUIOpen:
                        Time.timeScale = 0;
                        SelectionChangeInput();
                        Player.GetComponent<Animator>().SetBool("IsGamePlay", false);
                        Player.GetComponent<Animator>().speed = 0;
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
                break;
        }
        
    }

    void AdvanceTitle()
    {
        switch(titleStage)
        {
            case -1:
                if(pCM.GetAnyKey() && !anyKeyPressed)
                {
                    titleStage++;
                }
                break;
            case 0: //add control type prompt here
                if(pCM.GetButtonDown("UIRight") || pCM.GetButtonDown("UILeft"))
                {
                    ToggleControlSelection();
                }
                break;
            case 1:

                break;
        }
        
    }
    public void ToggleControlSelection()
    {
        switch(cType)
        {
            case 0:
                cType = 1;
                break;
            case 1:
                cType = 0;
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
                EquipUI.GetComponent<EquipUINew>().ResetSelection();
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

    void SelectionChangeInput()
    {
        EquipUINew e = EquipUI.GetComponent<EquipUINew>();
        if (pCM.GetButtonDown("UIUp"))
            e.ChangeSelection("u");
        if (pCM.GetButtonDown("UIDown"))
            e.ChangeSelection("d");
        if (pCM.GetButtonDown("UIRight"))
            e.ChangeSelection("r");
        if (pCM.GetButtonDown("UILeft"))
            e.ChangeSelection("l");
        

    }
}
