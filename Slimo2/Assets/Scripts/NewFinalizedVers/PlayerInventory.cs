using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    //public MouseItem mouseItem = new MouseItem();
    public Canvas invCanvas;
    public InventoryObject inventory;
    public InventoryObject equipment;
    public PlayerControlManager pCM;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var item = collision.GetComponent<GroundItem>();
        if(item)
        {
            Item _item = new Item(item.item);
            if(inventory.AddItem(_item, 1))
            {
                Destroy(collision.gameObject);
            }
        }
    }
    private void Start()
    {
        StartCoroutine(ToggleTimer());
        pCM = GameObject.FindGameObjectWithTag("pControlManager").GetComponent<PlayerControlManager>();

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            inventory.Save();
            equipment.Save();
        }
        if(Input.GetKeyDown(KeyCode.L))
        {
            inventory.Load();
            equipment.Load();
        }
        if(pCM.GetButtonDown("Inv"))
        {
            ToggleInventory();
        }
    }

    private void OnApplicationQuit()
    {
        inventory.Clear();
        equipment.Clear();
    }
    public void ToggleInventory()
    {
        if(invCanvas.gameObject.activeSelf)
        {
            invCanvas.gameObject.SetActive(false);
            return;
        }
        invCanvas.gameObject.SetActive(true);
    }
    private IEnumerator ToggleTimer()
    {
        yield return new WaitForSeconds(0.05f);
        ToggleInventory();
    }

}
