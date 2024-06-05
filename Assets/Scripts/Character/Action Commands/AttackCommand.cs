using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCommand : InputInterface
{

    private PlayerController player;

    public AttackCommand(PlayerController player){
        this.player = player;
    }

    public void ExecuteInputRequest(){
        player.Attack();
    }
}
