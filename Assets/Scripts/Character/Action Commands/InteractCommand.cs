using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractCommand : InputInterface
{
    
    private PlayerController player;

    public InteractCommand(PlayerController player){
        this.player = player;
    }

    public void ExecuteInputRequest(){
        player.Interact();
    }
}
