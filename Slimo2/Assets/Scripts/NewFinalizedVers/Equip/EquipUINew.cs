using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipUINew : MonoBehaviour
{
    public EquipSlots[] _eSlot;
    public bool UIActive = true;
    // Start is called before the first frame update
    void Start()
    {
        UpdateCursor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void UpdateCursor()
    {
        foreach (EquipSlots s in _eSlot)
        {
            if(s.UIPrefab != null)
            {
                s.selectCursor = s.UIPrefab.transform.Find("SelectionCursor").GetComponent<Image>();
                s.selectCursor.enabled = false;
            }
            
        }
    }

}
[System.Serializable]
public class EquipSlots
{
    public bool isSelected = false;
    public GameObject UIPrefab;
    public Image selectCursor;

}

