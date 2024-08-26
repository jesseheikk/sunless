using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    Text coinsText;
    int coins;

    void Awake()
    {
        GameObject coinsTextObject = GameObject.Find("CoinsText");
        if (coinsTextObject != null)
        {
            coinsText = coinsTextObject.GetComponent<Text>();
        }
        else
        {
            Debug.LogWarning("CoinText GameObject not found.");
        }

        coins = PlayerInfo.coins;
        UpdateCoinText();
    }

    public void AddCoins(int value)
    {
        PlayerInfo.coins += value;
        UpdateCoinText();
    }

    void UpdateCoinText()
    {
        if (coinsText)
        {
            coinsText.text = PlayerInfo.coins.ToString();
        }
    }
}
