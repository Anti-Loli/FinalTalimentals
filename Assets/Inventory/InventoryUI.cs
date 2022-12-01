using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject partyPanel;

    // public Player player;

   // public List<SlotsUI> slots = new List<Slots UI>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory();
        }else if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            ToggleParty();
        }
    }

    public void ToggleParty()
    {
        if (!partyPanel.activeSelf)
        {
            partyPanel.SetActive(true);
        }
        else
        {
            partyPanel.SetActive(false);
        }
    }
    public void ToggleInventory()
    {
        if (!inventoryPanel.activeSelf)
        {
            inventoryPanel.SetActive(true);
           // Setup();
        }
        else
        {
            inventoryPanel.SetActive(false);
        }
    }
/*
    void Setup()
    {
        if(slots.Count == player.inventory.slots.Count)
        {
            for(int i =0; i<slots.Count; i++)
            {
                if(player.inventory.slots[i].type != CollectableType.NONE)
                {

                }
            }
        }
    }*/
}
