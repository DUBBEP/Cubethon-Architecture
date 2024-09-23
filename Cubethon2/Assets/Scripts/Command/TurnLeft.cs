using UnityEngine;

public class TurnLeft : Command
{
    
    public TurnLeft(PlayerMovement controller)
    {
        this.controller = controller;
    }

    public override void Execute()
    {
        controller.Move(PlayerMovement.Direction.Left);
    }
}
