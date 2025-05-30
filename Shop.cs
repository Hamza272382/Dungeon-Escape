using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject shopPanel;          // UI panel for the shop
    public int currentSelectedItem;       // Currently selected item index
    public int currentItemCost;           // Cost of the selected item
    private Player player;                // Reference to the Player script

    // Called when a collider enters the trigger zone
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider belongs to the player
        if(other.tag == "Player")
        {
            player = other.GetComponent<Player>();

            if(player != null)
            {
                // Open the shop UI and pass current player's diamonds
                UIManager.Instance.OpenShop(player.Diamonds);
            }
            // Show the shop panel
            shopPanel.SetActive(true);
        }
    }

    // Called when a collider exits the trigger zone
    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the collider belongs to the player
        if (other.tag == "Player")
        {
            // Hide the shop panel when player leaves
            shopPanel.SetActive(false);
        }
    }

    // Called when the player selects an item in the shop UI
    public void SelectItem(int item)
    {
        Debug.Log("Select Item!" + item);

        switch (item)
        {
            case 0:
                // Update shop selection UI to position 70 and set cost to 200
                UIManager.Instance.UpdateShopSelection(70);
                currentSelectedItem = 0;
                currentItemCost = 200;
                break;
            case 1:
                // Update shop selection UI to position -45 and set cost to 400
                UIManager.Instance.UpdateShopSelection(-45);
                currentSelectedItem = 1;
                currentItemCost = 400;
                break;
            case 2:
                // Update shop selection UI to position -140 and set cost to 100
                UIManager.Instance.UpdateShopSelection(-140);
                currentSelectedItem = 2;
                currentItemCost = 100;
                break;
        }
    }

    // Called when the player confirms purchase
    public void BuyItem()
    {
        // Check if player has enough diamonds to buy the selected item
        if(player.Diamonds >= currentItemCost)
        {
            // Special effect for item 2: grant key to castle
            if(currentSelectedItem == 2)
            {
                GameManager.Instance.HasKeyToCastle = true;
            }

            // Deduct cost from player's diamonds
            player.Diamonds -= currentItemCost;

            Debug.Log("Purchased " + currentSelectedItem);
            Debug.Log("Remaining Gems " + player.Diamonds);

            // Close the shop panel after purchase
            shopPanel.SetActive(false);
        }
        else
        {
            // Inform player they don't have enough gems and close the panel
            Debug.Log("You Dont Have Enough Gems!!!");
            shopPanel.SetActive(false);
        }
    }
}
