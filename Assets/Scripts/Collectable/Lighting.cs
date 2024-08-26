using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lighting : Collectable
{
    protected override void PickUp(GameObject player)
    {
        base.PickUp(player);
        PlayerInfo.attack += 10f;
        PlayerAttack playerAttack = player.GetComponent<PlayerAttack>();
        playerAttack.UpdateAttack();
    }
}