using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Collectable
{
    protected override void PickUp(GameObject player)
    {
        base.PickUp(player);
        PlayerInfo.maxHealth += 20f;
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        playerHealth.UpdateMaxHealth();
    }
}