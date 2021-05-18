using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject EquipUI;
    public GameObject Player;
    public InputSystemShell pCM;
    public HitStop hStop = null;
    public int cType = 0; //default keyboard, 1 is controller
    private bool anyKeyPressed = false;
    public int titleStage = -1;
    public float transitionDelay;
    public GameObject fadeEffect;
    public float pDeathTime = 0;
    private bool canAdvBackToTitle = false;
    public GameObject DeathUI;
    public enum GameState
    {
        Gameplay,
        EquipUIOpen,
        Cutscene,
        PDeath
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
        switch(GetComponent<SceneManage>().ReturnSceneStateType())
        {
            case 0: //this scene is a title type scene
                sState = SceneState.Title;
                break;
            case 1:
                sState = SceneState.Levels;
                canAdvBackToTitle = false;
                if(DeathUI != null)
                {
                    DeathUI.SetActive(false);
                }
                break;
        }
        
        if (sState == SceneState.Levels)
        {
            EquipUI.GetComponent<EquipUINew>().UpdateCursor();
            EquipUI.GetComponent<EquipUINew>().SlotIDSet();
            EquipUI.SetActive(false);
            hStop = GetComponent<HitStop>();
            pCM = GameObject.FindGameObjectWithTag("pControlManager").GetComponent<InputSystemShell>();
            if(pCM != null && GameObject.FindGameObjectWithTag("cTypeHolder").GetComponent<ControlTypeScriptHolder>() != null) //sets up the control type based on user selection
            {
                switch(GameObject.FindGameObjectWithTag("cTypeHolder").GetComponent<ControlTypeScriptHolder>().cType)
                {
                    case 0:
                        pCM.SetControls(1);
                        break;
                    case 1:
                        pCM.SetControls(2);
                        break;
                }
            }
        }
        titleStage = -1;
        if(fadeEffect != null)
        {
            fadeEffect.SetActive(false);
        }
        
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
                        print("setting player state to no control");
                        Player.GetComponent<PlayerControllerNew>().SetPState(0);
                        Player.GetComponent<Animator>().SetBool("IsGamePlay", false);
                        //Player.GetComponent<PlayerControllerNew>().SetPState(2);
                        if(canAdvBackToTitle && pCM.GetAnyKey())
                        {
                            GetComponent<SceneManage>().ChangeLevel(0);
                        }
                        break;
                    case GameState.PDeath:
                        if(DeathUI != null)
                        {
                            DeathUI.SetActive(true);
                        }
                        if(pCM.GetAnyKey() && canAdvBackToTitle)
                        {
                            GetComponent<SceneManage>().ChangeLevel(0);
                        }
                        break;
                }
                break;
        }
        
    }
    public void StartDeathTimer()
    {
        StartCoroutine(PlayerDeathTimer());
    }
    IEnumerator PlayerDeathTimer()
    {
        yield return new WaitForSeconds(pDeathTime);
        canAdvBackToTitle = true;
    }
    public void SetReturnToTitle(bool b)
    {
        canAdvBackToTitle = b;
    }
    IEnumerator TitleScreenTransitionTimer()
    {
        yield return new WaitForSeconds(transitionDelay);
        GetComponent<SceneManage>().ChangeLevel(1);
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
                if (pCM.GetButtonDown("UIAccept"))
                {
                    if (GameObject.FindGameObjectWithTag("cTypeHolder").GetComponent<ControlTypeScriptHolder>() != null)
                    {
                        GameObject.FindGameObjectWithTag("cTypeHolder").GetComponent<ControlTypeScriptHolder>().cType = cType;
                    }
                    if(fadeEffect != null)
                    {
                        fadeEffect.SetActive(true);
                    }
                    titleStage++;
                    
                    
                }
                break;
            case 1:
                StartCoroutine(TitleScreenTransitionTimer());
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
    public void SwapGameState(int which)
    {
        switch(which)
        {
            case 0:
                gState = GameState.Gameplay;
                break;
            case 1:
                gState = GameState.Cutscene;
                break;
            case 2:
                gState = GameState.PDeath;
                break;
        }
    }
}
