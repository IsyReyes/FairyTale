using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCommand : InputInterface
{
    // This is a private field named 'player' of type 'PlayerController'.
    // It stores a reference to a 'PlayerController' instance, which the command will act upon.
    private PlayerController player;

    // This is the constructor (a method used to initialize an instance of a class when it is created).
    // The constructor takes a 'PlayerController' instance as a parameter and assigns it to the private field 'player'.
    // This allows the command to act on a specific instance of 'PlayerController'.
    public AttackCommand(PlayerController player)
    {
        this.player = player;
    }

    // This method executes the action associated with the command.
    // In this case, it calls the 'Attack' method on the 'PlayerController' instance referred to by 'player'.
    // This means that when 'ExecuteInputRequest' is called, it triggers the 'Attack' action on the specified 'PlayerController' instance.
    public void ExecuteInputRequest()
    {
        player.Attack();
    }
}

