using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyLock : MonoBehaviour
{
    public int targetID = 34;
    public Inventory player;
    public bool check;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindObjectOfType<Canvas>().GetComponentInChildren<Inventory>(true);
        
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && Input.GetKeyUp(KeyCode.E))
        {
            CheckForKey();
        }
    }

    public void CheckForKey()
    {

        player.updateItemList();

        foreach (Item item in player.ItemsInInventory)
        {
            if (item.itemID == targetID)
            {
                if (this.gameObject.GetComponent<PuzzleObject>())
                {
                    this.gameObject.GetComponent<PuzzleObject>().confirm = true;
                    Debug.Log("Unlocked");

                    item.itemValue--;
                    if (item.itemValue <= 0)
                    {
                        player.deleteItemFromInventoryWithGameObject(item);
                    }
                    player.updateItemList();

                }
            }
        }
    }
}
