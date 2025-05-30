using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;     // Singleton instance
    public static UIManager Instance
    {
        get
        {
            if(_instance == null)
            {
                Debug.Log("UI Manager is Null!");  // Warn if instance accessed before Awake sets it
            }
            return _instance;
        }
    }

    public Text playerGemCountText;          // Text showing player's gems in shop UI
    public Image selectionImg;                // Image used for highlighting selected item in shop
    public Text gemCountText;                 // Text showing gem count on main UI
    public Image[] healthBars;                // Array of health bar UI images

    // Updates the Y position of the shop item selection highlight
    public void UpdateShopSelection(int yPos)
    {
        selectionImg.rectTransform.anchoredPosition = new Vector2(selectionImg.rectTransform.anchoredPosition.x, yPos);
    }

    // Opens the shop UI and displays current gem count
    public void OpenShop(int gemCount)
    {
        playerGemCountText.text = "" + gemCount + "G";
    }

    // Awake is called when the script instance is being loaded
    private void Awake()
    {
        _instance = this;    // Set singleton instance
    }

    // Updates the main UI gem count text
    public void UpdateGemCount(int count)
    {
        gemCountText.text = "" + count;
    }

    // Updates the health bar UI based on remaining lives
    public void UpdateLives(int livesRemaining)
    {
        for(int i = 0; i <= livesRemaining; i++)
        {
            // Disable the health bar image corresponding to the current lives count
            if(i == livesRemaining)
            {
                healthBars[i].enabled = false;
            }
        }
    }
}
