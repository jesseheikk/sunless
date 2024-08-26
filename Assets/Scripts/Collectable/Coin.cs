using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Collectable
{
    [SerializeField] int value = 1;

    protected override void PickUp(GameObject player)
    {
        base.PickUp(player);
        PlayerInventory playerInventory = player.GetComponent<PlayerInventory>();
        playerInventory.AddCoins(value);
    }
}