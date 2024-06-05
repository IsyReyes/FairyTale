using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCommand : InputInterface
{
    private PlayerController player;
    private Vector2 direction;

    public MoveCommand(PlayerController player, Vector2 direction){
        this.player = player;
        this.direction = direction;
    }

    public void ExecuteInputRequest(){
        player.Move(direction);
    }
    

}