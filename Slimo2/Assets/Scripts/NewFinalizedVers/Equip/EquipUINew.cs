using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipUINew : MonoBehaviour
{
    public EquipSlots[] _eSlot;
    public bool UIActive = true;
    public int currentSelection = 0;
    // Start is called before the first frame update
    void Awake()
    {
        //UpdateCursor();
        //SlotIDSet();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateCursor()
    {
        foreach (EquipSlots s in _eSlot)
        {
            if(s.UIPrefab != null)
            {
                s.selectCursor = s.UIPrefab.transform.Find("SelectionCursor").GetComponent<Image>();
            }
            s.ShowCursor();
            
        }
    }
    public void SlotIDSet()
    {
        for(int i = 0; i < _eSlot.Length; i++)
        {
            if (_eSlot[i].slotID != i)
                _eSlot[i].slotID = i;
        }
        ResetSelection();
    }
    public void ResetSelection()
    {
        if(currentSelection > _eSlot.Length)
        {
            currentSelection = 0;
        }
        _eSlot[0].ShowCursor();
    }
    void SelectionUpdate() //call this every time the selection gets updated
    {
        for(int i = 0; i < _eSlot.Length; i++)
        {
            if(_eSlot[i].slotID != currentSelection)
            {
                _eSlot[i].isSelected = false;
            }
            if(_eSlot[i].slotID == currentSelection)
            {
                _eSlot[i].isSelected = true;
            }
            _eSlot[i].ShowCursor();
        }
    }
    public void ChangeSelection(string dir)
    {
        switch(dir)
        {
            case "u":
                if (currentSelection > 5)
                    currentSelection -= 6;
                break;
            case "d":
                if (currentSelection <= 5)
                    currentSelection += 6;
                break;
            case "r":
                if (currentSelection != 11)
                    currentSelection++;
                break;
            case "l":
                if (currentSelection != 0)
                    currentSelection--;
                break;
        }
        SelectionUpdate();
    }
}
[System.Serializable]
public class EquipSlots
{
    public bool isSelected = false;
    public GameObject UIPrefab;
    public Image selectCursor;
    public int slotID;


    public void ShowCursor()
    {
        if(isSelected)
        {
            selectCursor.enabled = true;
        }
        else
        {
            selectCursor.enabled = false;
        }
    }
}

